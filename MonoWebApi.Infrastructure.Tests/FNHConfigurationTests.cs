using System;
using System.Threading;
using System.Collections.Generic;
using System.Configuration;

using MonoWebApi.Domain.Entities;
using MonoWebApi.Infrastructure.DataAccess;

using FluentNHibernate.Testing;
using NHibernate;
using NHibernate.Collection.Generic;
using NUnit.Framework;
using System.Collections;
using NHibernate.Tool.hbm2ddl;

namespace MonoWebApi.Infrastructure.Tests
{
	[TestFixture]
	public class FNHConfigurationTests
	{
		ISession _session;
		SchemaExport _schema;

		[TestFixtureSetUp]
		public void FixtureSetUp ()
		{
			//ConfigurationManager.ConnectionStrings.Add ();
			foreach (ConnectionStringSettings str in ConfigurationManager.ConnectionStrings) {
				Console.WriteLine ("{0}: {1}", str.Name, str.ConnectionString);
			}
			ConfigurationManager.ConnectionStrings.Add (new ConnectionStringSettings ("DefaultConnection", "Server=localhost;Database=koshiyam;Uid=uniuser;Pwd=unipass;"));
			_session = NHibernateConfiguration.OpenSession ();
			_schema = new SchemaExport (NHibernateConfiguration.NHConfiguration);
		}

		[SetUp]
		public void SetUp ()
		{
			_schema.Drop (true, true);
			_schema.Create (true, true);
		}

		[Test]
		[Category ("Database")]
		public void Ingredient_PersistenceSpecification ()
		{
			var recipes = new List<Recipe> () {
				new Recipe() {Name="Recipe 1"},
				new Recipe() {Name="Recipe 2"}
			};
			new PersistenceSpecification<Ingredient> (_session, new MonoWebApiComparer())
				.CheckProperty (x => x.Name, "Ingredient Test 1")
				.CheckProperty (x => x.Created, DateTime.UtcNow)
				.CheckProperty (x => x.Updated, DateTime.UtcNow)
				.CheckProperty (x => x.Recipes, recipes)
				.VerifyTheMappings ();
		}

		[Test]
		[Category ("Database")]
		public void Recipe_PersistenceSpecification ()
		{
			var ingredients = new List<Ingredient> () {
				new Ingredient () { Name = "Ingredient 1" },
				new Ingredient () { Name = "Ingredient 2" },
			};
			new PersistenceSpecification<Recipe> (_session, new MonoWebApiComparer ())
				.CheckProperty (x => x.Name, "Recipe Test 1")
				.CheckProperty (x => x.Created, DateTime.UtcNow)
				.CheckProperty (x => x.Updated, DateTime.UtcNow)
				.CheckProperty (x => x.Ingredients, ingredients)
				.VerifyTheMappings ();
		}
	}

	class MonoWebApiComparer : IEqualityComparer
	{
		// x value is the one specified in the test
		bool IEqualityComparer.Equals (object x, object y)
		{
			if (x is DateTime && y is DateTime) {
				return CompareAsDates (x, y);
			}


			if (x is IList<Ingredient>) {
				return CompareAsIngredientLists (x, y);
			}

			if (x is IList<Recipe>) {
				return CompareAsRecipeLists (x, y);
			}

			return x.Equals (y);
		}

		bool CompareAsDates(object x, object y)
		{
			DateTime xDate = (DateTime)x;
			DateTime yDate = (DateTime)y;
			return xDate.ToString () == yDate.ToString ();
		}

		bool CompareAsIngredientLists(object x, object y)
		{
			var xList = x as List<Ingredient>;
			var yIList = y as PersistentGenericBag<Ingredient>;
			for (int i = 0; i < xList.Count; i++) {
				var xName = xList [i].Name;
				var yName = yIList [i].Name;
				if (xName != yName) {
					return false;
				}
			}
			return true;
		}

		bool CompareAsRecipeLists (object x, object y)
		{
			var xList = x as List<Recipe>;
			var yIList = y as PersistentGenericBag<Recipe>;
			for (int i = 0; i < xList.Count; i++) {
				var xName = xList [i].Name;
				var yName = yIList [i].Name;
				if (xName != yName) {
					return false;
				}
			}
			return true;
		}

		public int GetHashCode (object obj)
		{
			throw new NotImplementedException ();
		}
	}
}
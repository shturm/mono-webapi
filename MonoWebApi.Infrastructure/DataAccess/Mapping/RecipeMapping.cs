using System;
using MonoWebApi.Domain.Entities;
using FluentNHibernate.Mapping;

namespace MonoWebApi.Infrastructure.DataAccess
{
	public class RecipeMapping : SubclassMap<Recipe>
	{
		public RecipeMapping ()
		{
			//Abstract ();
			//Table ("Recipe");
			//KeyColumn ("Id");
			Map (x => x.Name);
			HasManyToMany <Ingredient>(x => x.Ingredients)
				.Table ("IngredientsRecipes")
				//.Inverse ()
				.Cascade.All ();
		}
	}
}
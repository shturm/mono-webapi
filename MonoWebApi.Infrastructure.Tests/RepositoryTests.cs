using NUnit.Framework;
using System.Configuration;
using System.Linq;
using MonoWebApi.Domain.Entities;
using Moq;
using NHibernate;
using System.Collections.Generic;

namespace MonoWebApi.Infrastructure.Tests
{
	[TestFixture]
	public class RepositoryTests
	{
		Mock<ISession> SessionMock;
		Mock<ITransaction> TxMock;
		Repository<Ingredient> sut;

		[SetUp]
		public void Setup()
		{
			//ConfigurationManager.ConnectionStrings.Add (new ConnectionStringSettings ("DefaultConnection", "Server=localhost;Database=koshiyam;Uid=uniuser;Pwd=unipass;"));
			SessionMock = new Mock<ISession> ();
			TxMock = new Mock<ITransaction> ();
			sut = new Repository<Ingredient> (SessionMock.Object);

			SessionMock.Setup (s => s.BeginTransaction ()).Returns (TxMock.Object);
		}

		[Test]
		[Category ("Unit")]
		public void Insert ()
		{
			var entity = new Ingredient ();

			Assert.DoesNotThrow (() => {
				sut.Insert (entity);
			});

			SessionMock.Verify (x => x.Save (entity), Times.Exactly (1));
			TxMock.Verify (t => t.Commit (), Times.Exactly (1));
		}

		[Test]
		[Category ("Unit")]
		public void Update ()
		{
			var entity = new Ingredient ();

			Assert.DoesNotThrow (() => {
				sut.Update(entity);
			});

			SessionMock.Verify (x => x.SaveOrUpdate (entity), Times.Exactly (1));
			TxMock.Verify (t => t.Commit (), Times.Exactly (1));
		}

		[Test]
		[Category ("Unit")]
		public void GetAll ()
		{
			var entity = new Ingredient ();
			int actualCount;
			var resultIngredients = new List<Ingredient> () { new Ingredient (), new Ingredient () };
			var criteriaMock = new Mock<ICriteria> ();

			criteriaMock.Setup (c => c.List <Ingredient>()).Returns (resultIngredients);
			SessionMock.Setup (s => s.CreateCriteria <Ingredient>()).Returns (criteriaMock.Object);

			actualCount = sut.GetAll ().Count ();

			Assert.AreEqual (resultIngredients.Count(), actualCount);
			SessionMock.Verify (x => x.SaveOrUpdate (entity), Times.Never ());
			SessionMock.Verify (x => x.Save (entity), Times.Never ());
			SessionMock.Verify (x => x.Delete (entity), Times.Never ());
		}

	}
}
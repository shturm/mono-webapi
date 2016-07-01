using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using MonoWebApi.Domain.Infrastructure;
using MonoWebApi.Infrastructure.DataAccess;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace MonoWebApi.Infrastructure
{
	public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
	{
		readonly ISession _session;

		public Repository (ISession session)
		{
			_session = session;
		}


		public void Delete (TEntity entity)
		{
			using (_session.BeginTransaction ())
			{
				_session.Delete (entity);
			}
		}

		public IEnumerable<TEntity> Get (Expression<Func<TEntity, bool>> predicate)
		{
			using(var tx = _session.BeginTransaction ())
			{
				return _session.Query<TEntity> ().Where (predicate).ToList();
			}
		}

		public IEnumerable<TEntity> GetAll ()
		{
			using (var tx = _session.BeginTransaction ()) {
				return _session.CreateCriteria <TEntity>().List <TEntity>();
			}
		}

		public void Insert (TEntity entity)
		{
			using(var tx = _session.BeginTransaction ())
			{
				_session.Save (entity);
				tx.Commit ();
			}
		}

		public void Update (TEntity entity)
		{
			using (var tx = _session.BeginTransaction ()) {
				_session.SaveOrUpdate(entity);
				tx.Commit ();
			}
		}

		public void Dispose ()
		{
			_session.Dispose ();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace MonoWebApi.Domain.Infrastructure
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll ();
		IEnumerable<T> Get (Expression<Func<T, bool>> expression);
		void Insert (T entity);
		void Delete (T entity);
		void Update (T entity);
	}
}
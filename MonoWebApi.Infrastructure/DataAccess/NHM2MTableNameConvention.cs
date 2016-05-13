using System;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Inspections;

namespace MonoWebApi.Infrastructure.DataAccess
{
	public class NHM2MTableNameConvention : FluentNHibernate.Conventions.ManyToManyTableNameConvention
	{
		protected override string GetBiDirectionalTableName (IManyToManyCollectionInspector collection, IManyToManyCollectionInspector otherSide)
		{
			var result = collection.EntityType.Name + "_" + otherSide.EntityType.Name;
			return result;
		}

		protected override string GetUniDirectionalTableName (IManyToManyCollectionInspector collection)
		{
			var result = collection.EntityType.Name + "_" + collection.ChildType.Name;
			return result;
		}
	}
}
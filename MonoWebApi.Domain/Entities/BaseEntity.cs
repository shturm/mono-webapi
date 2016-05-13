using System;
namespace MonoWebApi.Domain.Entities
{
	public class BaseEntity
	{
		public virtual int Id { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime Updated { get; set; }
	}
}
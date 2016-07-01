using System;
using System.Collections.Generic;
namespace MonoWebApi.Domain.Entities
{
	public class Recipe : BaseEntity
	{
		public virtual string Name { get; set; }
		public virtual IList<Ingredient> Ingredients { get; set;}
	}
}


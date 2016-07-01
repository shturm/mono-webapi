using System;
using System.Collections.Generic;

namespace MonoWebApi.Domain.Entities
{
	public class Ingredient : BaseEntity
	{
		public virtual string Name { get; set; }
		public virtual IList<Recipe> Recipes { get; set; }
	}
}
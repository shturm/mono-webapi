using System;
using MonoWebApi.Domain.Entities;
using FluentNHibernate.Mapping;

namespace MonoWebApi.Infrastructure.DataAccess
{
	public class IngredientMapping : SubclassMap<Ingredient>
	{
		public IngredientMapping ()
		{
			//Abstract ();
			//Table ("Ingredient");
			//KeyColumn ("Id");
			Map (x => x.Name);
			HasManyToMany<Recipe> (x => x.Recipes)
				.Table ("IngredientsRecipes")
				.Cascade.All ();
		}
	}
}
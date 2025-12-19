using BITSEFClasses.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BITSTests
{
    public class IngredientTests
    {
        BITSContext dbContext;
        Ingredient? i;
        List<Ingredient>? ingredients;

        [SetUp]
        public void Setup()
        {
            dbContext = new BITSContext();
            dbContext.Database.ExecuteSqlRaw("call usp_testingResetData()");
        }

        [Test]
        public void GetAllTest()
        {
            ingredients = dbContext.Ingredients.OrderBy(i => i.IngredientId).ToList();
            Assert.AreEqual(1149, ingredients.Count);
            Assert.AreEqual("Acid Malt", ingredients[0].Name);
            PrintAll(ingredients);
        }

        public void PrintAll(List<Ingredient> ingredients)
        {
            foreach (Ingredient i in ingredients)
            {
                Console.WriteLine(i);
            }
        }
    }
}

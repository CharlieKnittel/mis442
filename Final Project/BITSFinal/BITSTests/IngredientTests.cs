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

        [Test]
        public void GetByPrimaryKeyTest()
        {
            i = dbContext.Ingredients.Find(2);
            Assert.IsNotNull(i);
            Assert.AreEqual("Lager Malt", i.Name);
            Console.WriteLine(i);
        }

        [Test]
        public void GetUsingWhere()
        {
            // get a list of all of the ingredients measured per each (not weight or volume)
            ingredients = dbContext.Ingredients.Where(i => i.UnitType.Name == "each").OrderBy(i => i.IngredientId).ToList();
            Assert.AreEqual(12, ingredients.Count);
            Assert.AreEqual("Accumash", ingredients[0].Name);
            PrintAll(ingredients);
        }

        [Test]
        public void GetWithInventoryAdditionsTest()
        {
            // get the with an if of 1036 and all of the inventory additions for that ingredient
            i = dbContext.Ingredients.Include("IngredientInventoryAdditions").Where(i => i.IngredientId == 1036).SingleOrDefault();
            Assert.IsNotNull(i);
            Assert.AreEqual("Citra", i.Name);
            Assert.AreEqual(2, i.IngredientInventoryAdditions.Count);
            Console.WriteLine(i);
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

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

        [Test]
        public void GetWithJoinTest()
        {
            // get a list of objects that include the customer id, name, statecode and statename
            var ingredients = dbContext.Ingredients.Join(
               dbContext.IngredientTypes,
               i => i.IngredientTypeId,
               t => t.IngredientTypeId,
               (i, t) => new { i.IngredientId, i.Name, i.Version, IngredientTypeName = t.Name }).OrderBy(t => t.Name).ToList();
            Assert.AreEqual(1149, ingredients.Count);
            // I wouldn't normally print here but this lets you see what each object looks like
            foreach (var i in ingredients)
            {
                Console.WriteLine(i);
            }
        }

        /* Ingredients are not deleted in this use case, and testing such a function would require rewriting the DB config to complete a delete cascade, so I might work on
         * this later if I have time.
         
         If implemented, it would look like
        [Test]
        public void DeleteTest()
        {
            i = dbContext.Ingredients.Find(3);
            dbContext.Ingredients.Remove(i);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Ingredients.Find(3));
        }

         */

        [Test]
        public void CreateTest()
        {
            i = new Ingredient();
            i.Name = "Test Ingredient";
            i.Version = 1;
            i.IngredientTypeId = 1;
            i.UnitCost = 1.02m;
            i.ReorderPoint = 0.5;
            i.OnHandQuantity = 3;
            i.UnitTypeId = 2;
            dbContext.Ingredients.Add(i);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Ingredients.Find(i.IngredientId));
        }

        [Test]
        public void UpdateTest()
        {
            i = dbContext.Ingredients.Find(4);
            i.Name = "Chocolate Sauce";
            dbContext.Ingredients.Update(i);
            dbContext.SaveChanges();
            Assert.AreEqual("Chocolate Sauce", dbContext.Ingredients.Find(4).Name);
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

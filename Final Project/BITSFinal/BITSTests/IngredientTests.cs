using BITSEFClasses.Models;
using Microsoft.EntityFrameworkCore;

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
            dbContext.Database.ExecuteSqlRaw("call ")
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Core.Models.QueryModels;
using WaiterApplication.Core.Models.ViewModel;
using WaiterApplication.Core.Services;
using WaiterApplication.Infrastructure.Data;
using WaiterApplication.Infrastructure.Data.Common;
using WaiterApplication.Infrastructure.Data.Models;

namespace WaiterApplication.UnitTests
{
    [TestFixture]
    internal class MenuServiceUnitTests
    {
        private WaiterApplicationDbContext dbContext;
        private IRepository repository;
        private IMenuService menuService;

        private IEnumerable<Dish> dishes;

        private Dish dishOne;
        private Dish dishTwo;
        private Dish dishThree;
        private Dish dishFour;

        [SetUp]
        public void Setup()
        {
            dishOne = new Dish
            {
                Id = 1,
                Name = "Test Dish One",
                Description = "This is the description for test dish one.",
                Image = "image1.jpg",
                Price = 9.99m,
                Ingredients = "Ingredient 1, Ingredient 2, Ingredient 3"
            };

            dishTwo = new Dish
            {
                Id = 2,
                Name = "Test Dish Two",
                Description = "This is the description for test dish two.",
                Image = "image2.jpg",
                Price = 14.99m,
                Ingredients = "Ingredient A, Ingredient B, Ingredient C"
            };

            dishThree = new Dish
            {
                Id = 3,
                Name = "Test Dish Three",
                Description = "This is the description for test dish three.",
                Image = "image3.jpg",
                Price = 7.49m,
                Ingredients = "Ingredient X, Ingredient Y, Ingredient Z"
            };

            dishFour = new Dish
            {
                Id = 4,
                Name = "Test Dish Four",
                Description = "This is the description for test dish four.",
                Image = "image4.jpg",
                Price = 12.99m,
                Ingredients = "Ingredient Alpha, Ingredient Beta, Ingredient Gamma"
            };

            dishes = new List<Dish>() { dishOne, dishTwo, dishThree, dishFour };

            var options = new DbContextOptionsBuilder<WaiterApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            dbContext = new WaiterApplicationDbContext(options);
            dbContext.AddRangeAsync(dishes);
            dbContext.SaveChanges();

            repository = new Repository(dbContext);
            menuService = new MenuService(repository);
        }


        [Test]
        public async Task AddDishAsync_AddNewDish_ShouldSucceed()
        {
            var newDish = new Dish
            {
                Name = "New Test Dish",
                Description = "This is the description for the new test dish.",
                Image = "newimage.jpg",
                Price = 19.99m,
                Ingredients = "New Ingredient 1, New Ingredient 2"
            };

            await menuService.AddDishAsync(newDish.Name, newDish.Description, newDish.Image, newDish.Price, newDish.Ingredients);

            var allDishes = await menuService.AllAsync();
            Assert.IsTrue(allDishes.Dishes.Any(d => d.Name == newDish.Name));
        }


        [Test]
        public async Task DeleteAsync_ExistingDishId_DishDeletedSuccessfully()
        {
            int dishId = 1; 

            await menuService.DeleteAsync(dishId);

            Assert.IsFalse(await menuService.DishExistsAsync(dishId.ToString()));
        }
        [Test]
        public async Task DishExistsAsync_ExistingDishId_ShouldReturnTrue()
        {
            var existingDishId = dishOne.Id.ToString();

            var exists = await menuService.DishExistsAsync(existingDishId);

            Assert.IsTrue(exists);
        }

        [Test]
        public async Task DishExistsAsync_NonExistingDishId_ShouldReturnFalse()
        {
            var nonExistingDishId = "9999";

            var exists = await menuService.DishExistsAsync(nonExistingDishId);

            Assert.IsFalse(exists);
        }

        [Test]
        public async Task DishDetailsByIdAsync_ExistingDishId_ShouldReturnDishDetails()
        {
            var existingDishId = dishTwo.Id;

            var dishDetails = await menuService.DishDetailsByIdAsync(existingDishId);

            Assert.IsNotNull(dishDetails);
            Assert.AreEqual(dishTwo.Id, dishDetails.Id);
            Assert.AreEqual(dishTwo.Name, dishDetails.Name);
            Assert.AreEqual(dishTwo.Description, dishDetails.Description);
            Assert.AreEqual(dishTwo.Image, dishDetails.Image);
            Assert.AreEqual(dishTwo.Price, dishDetails.Price);
            Assert.AreEqual(dishTwo.Ingredients, dishDetails.Ingredients);
        }
        [Test]
        public async Task EditAsync_ExistingDishId_ShouldUpdateDishDetails()
        {
            var existingDishId = dishThree.Id;
            var updatedName = "Updated Name";
            var updatedDescription = "Updated Description";
            var updatedImage = "updated_image.jpg";
            var updatedPrice = 19.99m;
            var updatedIngredients = "Updated Ingredient 1, Updated Ingredient 2";

            var model = new DishFormModel
            {
                Name = updatedName,
                Description = updatedDescription,
                Image = updatedImage,
                Price = updatedPrice,
                Ingredients = updatedIngredients
            };

            await menuService.EditAsync(existingDishId, model);
            var updatedDish = await menuService.DishDetailsByIdAsync(existingDishId);

            Assert.IsNotNull(updatedDish);
            Assert.AreEqual(updatedName, updatedDish.Name);
            Assert.AreEqual(updatedDescription, updatedDish.Description);
            Assert.AreEqual(updatedImage, updatedDish.Image);
            Assert.AreEqual(updatedPrice, updatedDish.Price);
            Assert.AreEqual(updatedIngredients, updatedDish.Ingredients);
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.DisposeAsync();
        }
    }
}
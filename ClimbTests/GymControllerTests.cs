using Assignment1.Controllers;
using Assignment1.Data;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymTesting
{
    [TestClass]
    public class GymControllerTests
    {
        ApplicationDbContext _context;
        GymsController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
            _context = new ApplicationDbContext(options);
            
            var gym = new Gym { GymId = 1, Name = "Alt Rock", City = "Barrie",  GradingStyle = "Vermin" };
            _context.Gyms.Add(gym);

            gym = new Gym { GymId = 2, Name = "Gravity", City = "Hamilton", GradingStyle = "Vermin" };
            _context.Gyms.Add(gym);

            gym = new Gym { GymId = 3, Name = "Climbing Hangar", City = "Leeds", GradingStyle = "French" };
            _context.Gyms.Add(gym);

            _context.SaveChanges();

            controller = new GymsController(_context);
        }

        [TestMethod]
        public void DeleteNullReturnsErrorView() {
            var result = (ViewResult)controller.Delete(null).Result;

            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvlaidIdReturnsErrorView()
        {
            var result = (ViewResult)controller.Delete(999).Result;

            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdReturnsDeleteView()
        {
            var result = (ViewResult)controller.Delete(1).Result;

            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdReturnsGym()
        {
            var result = (ViewResult)controller.Delete(1).Result;
            var model = (Gym)result.Model;

            Assert.AreEqual(_context.Gyms.Find(1), model);
        }
    }
}
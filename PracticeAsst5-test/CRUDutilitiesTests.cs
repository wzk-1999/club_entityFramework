using Microsoft.EntityFrameworkCore;
using zhankui_wang_Practice_Asst_5.Models;
using zhankui_wang_Practice_Asst_5.Utilities;

namespace PracticeAsst5_test
{
    public class CRUDutilitiesTests
    {
        private readonly PracticeAsst5DbContext _context;

        public CRUDutilitiesTests()
        {
            var options = new DbContextOptionsBuilder<PracticeAsst5DbContext>()
                .UseSqlServer("Server=VOCBook15\\SQLEXPRESS;Database=PracticeAsst5DB;Trusted_Connection=True;MultipleActiveResultSets=true") // 
                .Options;

            _context = new PracticeAsst5DbContext(options);
        }

        [Fact]
        public void UserExists_ReturnsTrue_WhenUserExists()
        {
            // Act
            bool userExists = CRUDutilities.UserExists("Bill Anderson");

            // Assert
            Assert.True(userExists);
        }

        [Fact]
        public void UserExists_ReturnsFalse_WhenUserDoesNotExist()
        {
            // Act
            bool userExists = CRUDutilities.UserExists("Nonexistent User");

            // Assert
            Assert.False(userExists);
        }

        [Fact]
        public void ProvinceOfCity_ReturnsCorrectProvince_WhenCityExists()
        {
            // Act
            string province = CRUDutilities.ProvinceOfCity("Toronto");

            // Assert
            Assert.Equal("Ontario", province);
        }

        [Fact]
        public void ProvinceOfCity_ReturnsNotFound_WhenCityDoesNotExist()
        {
            // Act
            string province = CRUDutilities.ProvinceOfCity("Nonexistent City");

            // Assert
            Assert.Equal("Province not found", province);
        }

        [Fact]
        public void LivesInProv_ReturnsTrue_WhenUserLivesInProvince()
        {
            // Act
            bool livesInProv = CRUDutilities.LivesInProv("Bill Anderson", "Ontario");

            // Assert
            Assert.True(livesInProv);
        }

        [Fact]
        public void LivesInProv_ReturnsFalse_WhenUserDoesNotLiveInProvince()
        {
            // Act
            bool livesInProv = CRUDutilities.LivesInProv("Bill Anderson", "British Columbia");

            // Assert
            Assert.False(livesInProv);
        }
    }
}

using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Tests
{
    [TestClass]
    public class DriverDataServiceTests
    {
        DriverDataService _driverDataService = new DriverDataService();

        [TestMethod]
        public void CheckDriverInFile()
        {
            // Arrange
           
            
            // Act
            string[] result = _driverDataService.ListAllDriversService();

            // Assert
            var testArray = new string[] { "DID#SENVIV,4,Second,Victim,Old Rusty Bicycle,string,string" };
            Assert.AreEqual(testArray[0], result[4]);

        }
    }
}
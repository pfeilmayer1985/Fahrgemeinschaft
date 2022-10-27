using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data.Tests
{
    [TestClass]
    public class PassengerDataServiceTests
    {
        PassengerDataService _passengerDataService = new PassengerDataService();

        [TestMethod]
        public void CheckPassengerInFile()
        {
            // Arrange


            // Act
            string[] result = _passengerDataService.ListAllPassengersService();

            // Assert
            var testArray = new string[] { "PID#NICSAN,Nicusor,Sandu,Start,End" };
            Assert.AreEqual(testArray[0], result[0]);

        }
    }
}
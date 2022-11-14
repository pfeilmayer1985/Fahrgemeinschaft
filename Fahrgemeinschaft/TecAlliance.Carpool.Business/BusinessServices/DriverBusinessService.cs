using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{

    public class DriverBusinessService : IDriverBusinessService
    {
        private IDriverDataService _driverDataService;
        const string driverID = "DID#";
        string[] driver;
        public DriverBusinessService(IDriverDataService driverDataService)
        {
            _driverDataService = driverDataService;
            driver = _driverDataService.ListAllDriversService();
        }

        public DriverBusinessService()
        {

        }

        /// <summary>
        /// This method will return a detailed list with the driver IDs and infos
        /// </summary>
        public DriverModelData[] ListAllDriverData()
        {
            DriverModelData[] resultNew = new DriverModelData[driver.Length];
            int i = 0;
            foreach (string element in driver)
            {
                DriverModelData newDriverModelDto = new DriverModelData();
                var subElement = element.Split(',');
                newDriverModelDto.ID = subElement[0];
                newDriverModelDto.FreePlaces = Convert.ToInt32(subElement[1]);
                newDriverModelDto.FirstName = subElement[2];
                newDriverModelDto.LastName = subElement[3];
                newDriverModelDto.CarTypeMake = subElement[4];
                newDriverModelDto.StartingCity = subElement[5];
                newDriverModelDto.Destination = subElement[6];
                resultNew[i] = newDriverModelDto;
                i++;
            }
            return resultNew;
        }

        /// <summary>
        /// This method will return one detailed driver info based on a search after his IDs
        /// </summary>
        public DriverModelData ListDriverById(string id)
        {
            var findDriver = driver.FirstOrDefault(e => e.Contains(driverID + id));
            if (findDriver != null)
            {
                DriverModelData resultNew = new DriverModelData();
                var subElement = findDriver.Split(',');
                resultNew.ID = subElement[0];
                resultNew.FreePlaces = Convert.ToInt32(subElement[1]);
                resultNew.FirstName = subElement[2];
                resultNew.LastName = subElement[3];
                resultNew.CarTypeMake = subElement[4];
                resultNew.StartingCity = subElement[5];
                resultNew.Destination = subElement[6];
                return resultNew;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// This method will add a new driver in the file
        /// </summary>
        public DriverModelData AddDriverBuService(DriverModelData driver)
        {
            DriverModelData result = new DriverModelData()
            {
                ID = "",
                FreePlaces = driver.FreePlaces,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                CarTypeMake = driver.CarTypeMake,
                StartingCity = driver.StartingCity,
                Destination = driver.Destination
            };
            result.ID = SMGenerateAndCheckDriver(driverID + driver.FirstName.Substring(0, 3).ToUpper() + driver.LastName.Substring(0, 3).ToUpper());
            _driverDataService.AddDriverDaService(result);
            return result;
        }

        /// <summary>
        /// This method will "edit" the infos of a driver based on his ID. ID and Free places are not going to be edited
        /// </summary>
        public DriverModelDto EditDriverBuService(string id, DriverModelDto driverModelDto)
        {
            var findDriver = driver.FirstOrDefault(e => e.Contains(driverID + id));
            if (findDriver != null)
            {
                var subElement = findDriver.Split(',');
                DriverModelData result = new DriverModelData()
                {
                    ID = subElement[0],
                    FreePlaces = Convert.ToInt32(subElement[1]),
                    FirstName = driverModelDto.FirstName,
                    LastName = driverModelDto.LastName,
                    CarTypeMake = driverModelDto.CarTypeMake,
                    StartingCity = driverModelDto.StartingCity,
                    Destination = driverModelDto.Destination
                };
                this._driverDataService.EditDriverDaService(result);
                return MapToModelDtoDriver(result);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will delete a driver based on his ID
        /// </summary>
        public DriverModelData DeleteDriverBuService(string id)
        {
            var findDriver = driver.FirstOrDefault(e => e.Contains(driverID + id));
            if (findDriver != null)
            {
                DriverModelData resultNew = new DriverModelData();
                var subElement = findDriver.Split(',');
                resultNew.ID = subElement[0];
                _driverDataService.DeleteDriverDaService(resultNew);
                return resultNew;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Remapping a driver obj. to a DTO obj.
        /// </summary>
        private DriverModelDto MapToModelDtoDriver(DriverModelData driver)
        {
            DriverModelDto remappedDriver = new DriverModelDto()
            {
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                CarTypeMake = driver.CarTypeMake,
                StartingCity = driver.StartingCity,
                Destination = driver.Destination
            };
            return remappedDriver;
        }

        /// <summary>
        /// This submethod will generate a driver ID based on the first 3 letters in the First Name and first 3 letters in the Last Name
        /// If the ID allready exists in the file, a new ID will be generated, where the last character of the first 3 letters both in First Name and Last Name
        /// are replaced by a random character.
        /// </summary>
        public string SMGenerateAndCheckDriver(string id)
        {
            foreach (var driver in driver)
            {
                var splittedDriver = driver.Split(',');
                if (splittedDriver[0] == id)
                {
                    string partOne = id.Substring(0, 6);
                    string partTwo = id.Substring(7, 2);
                    id = partOne + SMGetaRandomChar() + partTwo + SMGetaRandomChar();
                }
            }
            return id;
        }

        /// <summary>
        /// This submethod will return random character to be used in the ID generator
        /// </summary>
        public static string SMGetaRandomChar()
        {
            string chars = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            var splittedChars = chars.Split(',').ToArray();
            Random rand = new Random();
            int num = rand.Next(0, splittedChars.Count() - 1);
            return splittedChars[num].ToString();
        }
    }
}
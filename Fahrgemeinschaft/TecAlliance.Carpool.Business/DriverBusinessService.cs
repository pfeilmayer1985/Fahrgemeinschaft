﻿using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{

    public class DriverBusinessService
    {
        private DriverDataService driverDataService;

        public DriverBusinessService()
        {
            driverDataService = new DriverDataService();

        }

        public Driver[] ListAllDriverData()
        {

            var drivers = driverDataService.ListAllDriversService();

            //var result = from x in passengerDataService.ListAllPassengersService()
            //             where x.Any()
            //             select x;
            Driver[] resultNew = new Driver[drivers.Length];
            int i = 0;
            foreach (string element in drivers)
            {
                Driver newDriverModelDto = new Driver();
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

        public Driver ListDriverById(string id)
        {

            var drivers = driverDataService.ListAllDriversService();

            var findDriver = drivers.First(e => e.Contains("DID#" + id));
            Driver resultNew = new Driver();
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


        public Driver AddDriverBuService(Driver driver)
        {
            Driver result = new Driver()
            {
                ID = "",
                FreePlaces = driver.FreePlaces,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                CarTypeMake = driver.CarTypeMake,
                StartingCity = driver.StartingCity,
                Destination = driver.Destination
            };
            result.ID = CheckIdByGenerateId("DID#" + driver.FirstName.Substring(0, 3).ToUpper() + driver.LastName.Substring(0, 3).ToUpper());

            driverDataService.AddDriverDaService(result);

            return result;


        }


        public DriverModelDto EditDriverBuService(string id, DriverModelDto driverModelDto)
        {

            var drivers = driverDataService.ListAllDriversService();
            var findDriver = drivers.First(e => e.Contains("DID#" + id));
            var subElement = findDriver.Split(',');

            Driver result = new Driver()
            {
                ID = subElement[0],
                FreePlaces = Convert.ToInt32(subElement[1]),
                FirstName = driverModelDto.FirstName,
                LastName = driverModelDto.LastName,
                CarTypeMake = driverModelDto.CarTypeMake,
                StartingCity = driverModelDto.StartingCity,
                Destination = driverModelDto.Destination
            };

            driverDataService.EditDriverDaService(result);

            return MapToModelDtoDriver(result);
        }


        public Driver DeleteDriverBuService(string id)
        {

            var drivers = driverDataService.ListAllDriversService();
            var findDriver = drivers.First(e => e.Contains("DID#" + id));
           
            Driver resultNew = new Driver();
            var subElement = findDriver.Split(',');
            resultNew.ID = subElement[0];

            driverDataService.DeleteDriverDaService(resultNew);

            return resultNew;
        }


        private DriverModelDto MapToModelDtoDriver(Driver driver)
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


        private Driver MapToDriver(DriverModelDto driverModelDto)
        {
            Driver remappedDriverDto = new Driver()
            {
                ID = driverModelDto.ID,
                FreePlaces = Convert.ToInt32(driverModelDto.FreePlaces),
                FirstName = driverModelDto.FirstName,
                LastName = driverModelDto.LastName,
                CarTypeMake = driverModelDto.CarTypeMake,
                StartingCity = driverModelDto.StartingCity,
                Destination = driverModelDto.Destination

            };

            return remappedDriverDto;

        }



        public string CheckIdByGenerateId(string id)
        {
            var driverID = driverDataService.ListAllDriversService();

            foreach (var driver in driverID)
            {
                var splittedDriver = driver.Split(',');
                if (splittedDriver[0] == id)
                {
                    string partOne = id.Substring(0, 6);
                    string partTwo = id.Substring(7, 2);
                    id = partOne + GetaRandomChar() + partTwo + GetaRandomChar();
                }
            }
            return id;
        }

        public static string GetaRandomChar()
        {
            string chars = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            var splittedChars = chars.Split(',').ToArray();
            Random rand = new Random();
            int num = rand.Next(0, splittedChars.Count() - 1);
            return splittedChars[num].ToString();
        }

    }
}
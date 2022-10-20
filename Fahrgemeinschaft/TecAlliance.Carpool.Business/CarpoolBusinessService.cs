using System.Xml.Linq;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{

    public class CarpoolBusinessService
    {
        private CarpoolDataService carpoolDataService;
        private DriverDataService driverDataService;
        private PassengerDataService passengerDataService;

        public CarpoolBusinessService()
        {
            carpoolDataService = new CarpoolDataService();
            driverDataService = new DriverDataService();
            passengerDataService = new PassengerDataService();
        }

        public CarpoolModelDto[] ListAllCarpoolsData()
        {

            var carpools = carpoolDataService.ListAllCarpoolsDataService();
            var passenger = passengerDataService.ListAllPassengersService();
            var driverX = driverDataService.ListAllDriversService();

            CarpoolModelDto[] resultNew = new CarpoolModelDto[carpools.Length];
            //List<string> carpoolDetailed = new List<string>();

            int i = 0;
            foreach (string element in carpools)
            {
                CarpoolModelDto newCarpoolModelDto = new CarpoolModelDto();
                newCarpoolModelDto.Passengers = new List<string>();
                var subElement = element.Split(',');
                newCarpoolModelDto.Driver = subElement[0];


                var findDriver = driverX.First(e => e.Contains(subElement[0]));
                Driver resultedDriver = new Driver();
                var subElementofDriver = findDriver.Split(',');
                resultedDriver.ID = subElementofDriver[0];
                resultedDriver.FreePlaces = Convert.ToInt32(subElementofDriver[1]);
                resultedDriver.FirstName = subElementofDriver[2];
                resultedDriver.LastName = subElementofDriver[3];
                resultedDriver.CarTypeMake = subElementofDriver[4];
                resultedDriver.StartingCity = subElementofDriver[5];
                resultedDriver.Destination = subElementofDriver[6];
                newCarpoolModelDto.DriverDetails = resultedDriver;


                Passenger resultedPassenger = new Passenger();
                newCarpoolModelDto.PassengersDetails = new List<Passenger>();

                for (int j = 1; j < subElement.Length; j++)
                {
                    newCarpoolModelDto.Passengers.Add(subElement[j]);

                    var findPassenger = passenger.First(e => e.Contains(subElement[j]));
                    var subElementofPassenger = findPassenger.Split(',');
                    resultedPassenger.ID = subElementofPassenger[0];
                    resultedPassenger.FirstName = subElementofPassenger[1];
                    resultedPassenger.LastName = subElementofPassenger[2];
                    resultedPassenger.StartingCity = subElementofPassenger[3];
                    resultedPassenger.Destination = subElementofPassenger[4];
                    newCarpoolModelDto.PassengersDetails.Add(resultedPassenger);

                }

                resultNew[i] = newCarpoolModelDto;
                i++;
            }
            return resultNew;
        }

        public CarpoolModelDto ListCarpoolById(string id)
        {

            var carpool = carpoolDataService.ListAllCarpoolsDataService();
            var passenger = passengerDataService.ListAllPassengersService();
            var driverX = driverDataService.ListAllDriversService();

            var findCarpool = carpool.First(e => e.Contains("DID#" + id));
            CarpoolModelDto resultNew = new CarpoolModelDto();
            var subElement = findCarpool.Split(',');
            resultNew.Driver = subElement[0];

            var findDriver = driverX.First(e => e.Contains(subElement[0]));
            Driver resultedDriver = new Driver();
            var subElementofDriver = findDriver.Split(',');
            resultedDriver.ID = subElementofDriver[0];
            resultedDriver.FreePlaces = Convert.ToInt32(subElementofDriver[1]);
            resultedDriver.FirstName = subElementofDriver[2];
            resultedDriver.LastName = subElementofDriver[3];
            resultedDriver.CarTypeMake = subElementofDriver[4];
            resultedDriver.StartingCity = subElementofDriver[5];
            resultedDriver.Destination = subElementofDriver[6];
            resultNew.DriverDetails = resultedDriver;

            Passenger resultedPassenger = new Passenger();
            resultNew.PassengersDetails = new List<Passenger>();


            resultNew.Passengers = new List<string>();
            for (int j = 1; j < subElement.Length; j++)
            {

                resultNew.Passengers.Add(subElement[j]);


                var findPassenger = passenger.First(e => e.Contains(subElement[j]));
                var subElementofPassenger = findPassenger.Split(',');
                resultedPassenger.ID = subElementofPassenger[0];
                resultedPassenger.FirstName = subElementofPassenger[1];
                resultedPassenger.LastName = subElementofPassenger[2];
                resultedPassenger.StartingCity = subElementofPassenger[3];
                resultedPassenger.Destination = subElementofPassenger[4];
                resultNew.PassengersDetails.Add(resultedPassenger);

            }



            return resultNew;

        }

    }
}
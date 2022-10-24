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

        public CarpoolModel? AddCarpoolBuByPassengerAndDriverId(string inputDriverID, string inputPassengerID)
        {
            var carpool = carpoolDataService.ListAllCarpoolsDataService();
            var passengerFile = passengerDataService.ListAllPassengersService();
            var driversFile = driverDataService.ListAllDriversService();

            var findPassengerInPassengers = passengerFile.FirstOrDefault(e => e.Contains("PID#" + inputPassengerID));
            var findDriverInDrivers = driversFile.FirstOrDefault(e => e.Contains("DID#" + inputDriverID));
           

            if (findPassengerInPassengers != null && findDriverInDrivers != null)
            {

                var findDriverInCarpool = carpool.FirstOrDefault(e => e.Contains("DID#" + inputDriverID));
                var findExistingCarpool = carpool.FirstOrDefault(e => e.Contains("DID#" + inputDriverID) && e.Contains("PID#" + inputPassengerID));
                //if the user is allready in the carpool throw error
                if (findExistingCarpool != null)
                {
                    throw new Exception();
                }
                //check if the driver has a carpool, if yes, add user to carpool
                else if (findDriverInCarpool != null)
                {
                    var elementsOfCurrentCarpool = findDriverInCarpool.Split(',');

                    Driver currentDriverX = new Driver();
                    CarpoolModelDto toDeleteCarpoolEntry = new CarpoolModelDto();
                    CarpoolModel toAddEditedCarpoolEntry = new CarpoolModel();
                    var newListOfRemainingPassengers = new List<string>();

                    toDeleteCarpoolEntry.Driver = elementsOfCurrentCarpool[0];
                    toAddEditedCarpoolEntry.Driver = elementsOfCurrentCarpool[0];

                    for (int i = 1; i < elementsOfCurrentCarpool.Length; i++)
                    {
                        // if (elementsOfCurrentCarpool[i] != "PID#" + inputPassengerID)
                        newListOfRemainingPassengers.Add(elementsOfCurrentCarpool[i]);
                    }
                    newListOfRemainingPassengers.Add("PID#" + inputPassengerID);
                    //var result = string.Join(",", newListOfRemainingPassengers.ToArray());
                    toAddEditedCarpoolEntry.Passengers = newListOfRemainingPassengers;

                    //delete old string
                    carpoolDataService.DeleteCarpoolDaService(MapToCarpool(toDeleteCarpoolEntry));

                    // add new string
                    carpoolDataService.AddCarpoolDaService(toAddEditedCarpoolEntry);

                    //change free places for the driver erased from the carpool
                    var subElementDriverX = findDriverInDrivers.Split(',');
                    currentDriverX.ID = subElementDriverX[0];
                    currentDriverX.FreePlaces = Convert.ToInt32(subElementDriverX[1]) - 1;
                    currentDriverX.FirstName = subElementDriverX[2];
                    currentDriverX.LastName = subElementDriverX[3];
                    currentDriverX.CarTypeMake = subElementDriverX[4];
                    currentDriverX.StartingCity = subElementDriverX[5];
                    currentDriverX.Destination = subElementDriverX[6];
                    driverDataService.EditDriverDaService(currentDriverX);
                    return toAddEditedCarpoolEntry;
                }
                //if the driver is not in the carpool lists, make new carpool
                else if (findDriverInCarpool == null)
                {
                    CarpoolModel newCarpool = new CarpoolModel();
                    newCarpool.Driver = "\nDID#" + inputDriverID;
                    var newListOPassengers = new List<string>();
                    newListOPassengers.Add("PID#" + inputPassengerID);
                    newCarpool.Passengers = newListOPassengers;
                    carpoolDataService.AddCarpoolDaService(newCarpool);

                    return newCarpool;
                }
            }
            else
            {
                throw new Exception();
            }

            return null;

        }


        public CarpoolModelDto DeleteCarpoolByDriverId(string id)
        {
            //delete carpool based on driverID
            var carpool = carpoolDataService.ListAllCarpoolsDataService();
            var findCarpool = carpool.First(e => e.Contains("DID#" + id));
            CarpoolModelDto resultNew = new CarpoolModelDto();
            var subElement = findCarpool.Split(',');
            var numberOfCarpoolPassengers = subElement.Length - 1; //take length of the carpool IDs and substract 1 that is the driver. the rest are passengers
            resultNew.Driver = subElement[0];


            carpoolDataService.DeleteCarpoolDaService(MapToCarpool(resultNew));

            //change free places for the driver erased from the carpool
            var driver = driverDataService.ListAllDriversService();
            var findDriver = driver.First(e => e.Contains("DID#" + id));
            Driver currentDriver = new Driver();
            var subElementDriver = findDriver.Split(',');
            currentDriver.ID = subElementDriver[0];
            currentDriver.FreePlaces = Convert.ToInt32(subElementDriver[1]) + numberOfCarpoolPassengers;
            currentDriver.FirstName = subElementDriver[2];
            currentDriver.LastName = subElementDriver[3];
            currentDriver.CarTypeMake = subElementDriver[4];
            currentDriver.StartingCity = subElementDriver[5];
            currentDriver.Destination = subElementDriver[6];


            driverDataService.EditDriverDaService(currentDriver);

            return resultNew;

        }

        public CarpoolModelDto DeleteCarpoolByPassengerAndDriverId(string inputDriverID, string inputPassengerID)
        {
            var carpool = carpoolDataService.ListAllCarpoolsDataService();
            var findDriverInCarpool = carpool.FirstOrDefault(e => e.Contains("DID#" + inputDriverID) && e.Contains("PID#" + inputPassengerID));
            var elementsOfCurrentCarpool = findDriverInCarpool.Split(',');
            var newListOfRemainingPassengers = new List<string>();

            var driverX = driverDataService.ListAllDriversService();
            var findDriverX = driverX.First(e => e.Contains("DID#" + inputDriverID));
            Driver currentDriverX = new Driver();

            CarpoolModelDto toDeleteCarpoolEntry = new CarpoolModelDto();
            CarpoolModel toAddEditedCarpoolEntry = new CarpoolModel();

            var subElement = findDriverInCarpool.Split(',');
            toDeleteCarpoolEntry.Driver = subElement[0];
            toAddEditedCarpoolEntry.Driver = subElement[0];

            if (elementsOfCurrentCarpool.Length != 2)
            {
                for (int i = 1; i < elementsOfCurrentCarpool.Length; i++)
                {
                    if (elementsOfCurrentCarpool[i] != "PID#" + inputPassengerID)
                        newListOfRemainingPassengers.Add(elementsOfCurrentCarpool[i]);
                }
                var result = string.Join(",", newListOfRemainingPassengers.ToArray());
                toAddEditedCarpoolEntry.Passengers = newListOfRemainingPassengers;

                //delete old string
                carpoolDataService.DeleteCarpoolDaService(MapToCarpool(toDeleteCarpoolEntry));

                // add new string
                carpoolDataService.AddCarpoolDaService(toAddEditedCarpoolEntry);

                //change free places for the driver erased from the carpool
                var subElementDriverX = findDriverX.Split(',');
                currentDriverX.ID = subElementDriverX[0];
                currentDriverX.FreePlaces = Convert.ToInt32(subElementDriverX[1]) + 1;
                currentDriverX.FirstName = subElementDriverX[2];
                currentDriverX.LastName = subElementDriverX[3];
                currentDriverX.CarTypeMake = subElementDriverX[4];
                currentDriverX.StartingCity = subElementDriverX[5];
                currentDriverX.Destination = subElementDriverX[6];
                driverDataService.EditDriverDaService(currentDriverX);
            }
            else
            {
                carpoolDataService.DeleteCarpoolDaService(MapToCarpool(toDeleteCarpoolEntry));

                //change free places for the driver erased from the carpool
                var subElementDriverX = findDriverX.Split(',');
                currentDriverX.ID = subElementDriverX[0];
                currentDriverX.FreePlaces = Convert.ToInt32(subElementDriverX[1]) + 1;
                currentDriverX.FirstName = subElementDriverX[2];
                currentDriverX.LastName = subElementDriverX[3];
                currentDriverX.CarTypeMake = subElementDriverX[4];
                currentDriverX.StartingCity = subElementDriverX[5];
                currentDriverX.Destination = subElementDriverX[6];
                driverDataService.EditDriverDaService(currentDriverX);
            }
            return toDeleteCarpoolEntry;
        }

        private CarpoolModelDto MapToModelDtoCarpool(CarpoolModel carpool)
        {
            CarpoolModelDto remappedCarpool = new CarpoolModelDto()
            {
                Driver = carpool.Driver,
                Passengers = carpool.Passengers,

            };

            return remappedCarpool;

        }

        private CarpoolModel MapToCarpool(CarpoolModelDto dtoCarpool)
        {
            CarpoolModel remappedCarpoolDto = new CarpoolModel()
            {
                Driver = dtoCarpool.Driver,
                Passengers = dtoCarpool.Passengers,

            };

            return remappedCarpoolDto;

        }


    }
}
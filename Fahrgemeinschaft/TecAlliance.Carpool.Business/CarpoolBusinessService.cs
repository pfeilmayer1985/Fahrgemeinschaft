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

        /// <summary>
        /// This method will return a detailed carpool list with drivers and passengers IDs and infos
        /// </summary>
        public CarpoolModelDto[] ListAllCarpoolsDataBu()
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

        /// <summary>
        /// This method will return one detailed carpool info based on a search after driver ID and passenger IDs
        /// </summary>
        public CarpoolModelDto ListCarpoolByIdBu(string id)
        {

            var carpool = carpoolDataService.ListAllCarpoolsDataService();
            var passenger = passengerDataService.ListAllPassengersService();
            var driverX = driverDataService.ListAllDriversService();

            var findCarpool = carpool.FirstOrDefault(e => e.Contains("DID#" + id));

            if (findCarpool != null)
            {
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
            else
            {
                return null;
            }

        }

        /// <summary>
        /// This method will either build a new carpool based on Driver ID and Passenger ID or add to an existing Driver ID carpool the Passenger ID
        /// </summary>
        public CarpoolModel? AddCarpoolByPassengerAndDriverIdBu(string inputDriverID, string inputPassengerID)
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
                //if the driver and passenger is allready in the carpool throw error
                Driver currentDriverX = new Driver();

                if (findExistingCarpool != null)
                {
                    return null;
                }
                //check if the driver has a carpool, if yes, add user to carpool
                else if (findDriverInCarpool != null)
                {
                    var elementsOfCurrentCarpool = findDriverInCarpool.Split(',');

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
                    toAddEditedCarpoolEntry.Passengers = newListOfRemainingPassengers;

                    //delete old string
                    carpoolDataService.DeleteCarpoolDaService(MapToCarpoolBu(toDeleteCarpoolEntry));

                    // add new string
                    carpoolDataService.AddCarpoolDaService(toAddEditedCarpoolEntry);

                    //reduce the amount of free places from the current driver with 1
                    SMReduceFreePlacesWithOne(findDriverInDrivers, currentDriverX);
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

                    //reduce the amount of free places from the current driver with 1
                    SMReduceFreePlacesWithOne(findDriverInDrivers, currentDriverX);

                    return newCarpool;
                }
            }
            else
            {
                return null;
            }

            return null;

        }

        /// <summary>
        /// This method will delete an existing carpool based on Driver ID
        /// </summary>
        public CarpoolModelDto DeleteCarpoolByDriverIdBu(string id)
        {
            //delete carpool based on driverID
            var carpool = carpoolDataService.ListAllCarpoolsDataService();
            var findCarpool = carpool.FirstOrDefault(e => e.Contains("DID#" + id));

            if (findCarpool != null)
            {
                CarpoolModelDto resultNew = new CarpoolModelDto();
                var subElement = findCarpool.Split(',');
                var numberOfCarpoolPassengers = subElement.Length - 1; //take length of the carpool IDs and substract 1 that is the driver. the rest are passengers
                resultNew.Driver = subElement[0];


                carpoolDataService.DeleteCarpoolDaService(MapToCarpoolBu(resultNew));

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
            else
            {
                return null;
            }

        }

        /// <summary>
        /// This method will either remove an passenger from an existing carpool/Driver ID or if he is the only passenger 
        /// then will dissolve/delete the existing carpool based on Driver ID
        /// </summary>
        public CarpoolModelDto RemovePassengerFromCarpoolByPassengerIdAndDriverIdBu(string inputDriverID, string inputPassengerID)
        {
            var carpool = carpoolDataService.ListAllCarpoolsDataService();
            var driverX = driverDataService.ListAllDriversService();
            var passengerX = passengerDataService.ListAllPassengersService();
            var foundDriverX = driverX.FirstOrDefault(e => e.Contains("DID#" + inputDriverID.ToUpper()));
            var foundPassengerX = passengerX.FirstOrDefault(e => e.Contains("PID#" + inputPassengerID.ToUpper()));
            var bothDriverAndPassengerAreInACarpool = carpool.FirstOrDefault(e => e.Contains("DID#" + inputDriverID) && e.Contains("PID#" + inputPassengerID));

            if (foundDriverX != null && foundPassengerX != null && bothDriverAndPassengerAreInACarpool != null)
            {

                var elementsOfCurrentCarpool = bothDriverAndPassengerAreInACarpool.Split(',');
                var newListOfRemainingPassengers = new List<string>();

                Driver currentDriverX = new Driver();

                CarpoolModelDto toDeleteCarpoolEntry = new CarpoolModelDto();
                CarpoolModel toAddEditedCarpoolEntry = new CarpoolModel();

                var subElement = bothDriverAndPassengerAreInACarpool.Split(',');
                toDeleteCarpoolEntry.Driver = subElement[0];
                toAddEditedCarpoolEntry.Driver = subElement[0];

                //delete old string
                carpoolDataService.DeleteCarpoolDaService(MapToCarpoolBu(toDeleteCarpoolEntry));

                if (elementsOfCurrentCarpool.Length != 2)
                {
                    for (int i = 1; i < elementsOfCurrentCarpool.Length; i++)
                    {
                        if (elementsOfCurrentCarpool[i] != "PID#" + inputPassengerID)
                            newListOfRemainingPassengers.Add(elementsOfCurrentCarpool[i]);
                    }
                    var result = string.Join(",", newListOfRemainingPassengers.ToArray());
                    toAddEditedCarpoolEntry.Passengers = newListOfRemainingPassengers;
                    // add new string
                    carpoolDataService.AddCarpoolDaService(toAddEditedCarpoolEntry);

                }


                //change free places for the driver erased from the carpool
                SMRecoverOneFreePlace(foundDriverX, currentDriverX);

                driverDataService.EditDriverDaService(currentDriverX);
                return toDeleteCarpoolEntry;
            }
            else
            {
                return null;
            }
        }

        /* private CarpoolModelDto MapToModelDtoCarpool(CarpoolModel carpool)
         {
             CarpoolModelDto remappedCarpool = new CarpoolModelDto()
             {
                 Driver = carpool.Driver,
                 Passengers = carpool.Passengers,

             };

             return remappedCarpool;

         }*/

        /// <summary>
        /// This method is remapping a Carpool model to a Carpool model DTO
        /// </summary>
        private CarpoolModel MapToCarpoolBu(CarpoolModelDto dtoCarpool)
        {
            CarpoolModel remappedCarpoolDto = new CarpoolModel()
            {
                Driver = dtoCarpool.Driver,
                Passengers = dtoCarpool.Passengers,

            };

            return remappedCarpoolDto;

        }

        /// <summary>
        /// Submethod that reduces the number of free places of a driver with 1
        /// </summary>
        private void SMReduceFreePlacesWithOne(string? findDriverInDrivers, Driver currentDriverX)
        {
            var subElementDriverX = findDriverInDrivers.Split(',');
            currentDriverX.ID = subElementDriverX[0];
            currentDriverX.FreePlaces = Convert.ToInt32(subElementDriverX[1]) - 1;
            currentDriverX.FirstName = subElementDriverX[2];
            currentDriverX.LastName = subElementDriverX[3];
            currentDriverX.CarTypeMake = subElementDriverX[4];
            currentDriverX.StartingCity = subElementDriverX[5];
            currentDriverX.Destination = subElementDriverX[6];
            driverDataService.EditDriverDaService(currentDriverX);
        }

        /// <summary>
        /// Submethod that adds back one free place to the current number of free places of a driver
        /// </summary>
        private static void SMRecoverOneFreePlace(string findDriverX, Driver currentDriverX)
        {
            var subElementDriverX = findDriverX.Split(',');
            currentDriverX.ID = subElementDriverX[0];
            currentDriverX.FreePlaces = Convert.ToInt32(subElementDriverX[1]) + 1;
            currentDriverX.FirstName = subElementDriverX[2];
            currentDriverX.LastName = subElementDriverX[3];
            currentDriverX.CarTypeMake = subElementDriverX[4];
            currentDriverX.StartingCity = subElementDriverX[5];
            currentDriverX.Destination = subElementDriverX[6];
        }


    }
}
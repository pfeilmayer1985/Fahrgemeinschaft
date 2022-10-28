using System.Reflection.Metadata;
using System.Xml.Linq;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public class CarpoolBusinessService : ICarpoolBusinessService
    {
        private ICarpoolDataService _carpoolDataService;
        private IDriverDataService _driverDataService;
        private IPassengerDataService _passengerDataService;
        const string driverID = "DID#";
        const string passengerID = "PID#";
        string[] carpools;
        string[] passenger;
        string[] driver;
        public CarpoolBusinessService(ICarpoolDataService carpoolDataService, IDriverDataService driverDataService, IPassengerDataService passengerDataService)
        {
            _carpoolDataService = carpoolDataService;
            _driverDataService = driverDataService;
            _passengerDataService = passengerDataService;
            carpools = _carpoolDataService.ListAllCarpoolsDataService();
            passenger = _passengerDataService.ListAllPassengersService();
            driver = _driverDataService.ListAllDriversService();
        }

        public CarpoolBusinessService()
        {

        }

        /// <summary>
        /// This method will return a detailed carpool list with drivers and passengers IDs and infos
        /// </summary>
        public CarpoolModelDto[] ListAllCarpoolsDataBu()
        {
            CarpoolModelDto[] resultNew = new CarpoolModelDto[carpools.Length];
            int i = 0;
            foreach (string element in carpools)
            {
                CarpoolModelDto newCarpoolModelDto = new CarpoolModelDto();
                newCarpoolModelDto.Passengers = new List<string>();
                var subElement = element.Split(',');
                newCarpoolModelDto.Driver = subElement[0];

                var findDriver = driver.First(e => e.Contains(subElement[0]));
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
                newCarpoolModelDto.PassengersDetails = new List<Passenger>();

                for (int j = 1; j < subElement.Length; j++)
                {
                    Passenger resultedPassenger = new Passenger();
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
            var findCarpool = carpools.FirstOrDefault(e => e.Contains(driverID + id));
            if (findCarpool != null)
            {
                CarpoolModelDto resultNew = new CarpoolModelDto();
                var subElement = findCarpool.Split(',');
                resultNew.Driver = subElement[0];
                var findDriver = driver.First(e => e.Contains(subElement[0]));
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

                resultNew.PassengersDetails = new List<Passenger>();
                resultNew.Passengers = new List<string>();
                for (int j = 1; j < subElement.Length; j++)
                {
                    Passenger resultedPassenger = new Passenger();
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
            var findPassengerInPassengers = passenger.FirstOrDefault(e => e.Contains(passengerID + inputPassengerID));
            var findDriverInDrivers = driver.FirstOrDefault(e => e.Contains(driverID + inputDriverID));
            if (findPassengerInPassengers != null && findDriverInDrivers != null)
            {
                var findDriverInCarpool = carpools.FirstOrDefault(e => e.Contains(driverID + inputDriverID));
                var findExistingCarpool = carpools.FirstOrDefault(e => e.Contains(driverID + inputDriverID) && e.Contains(passengerID + inputPassengerID));
                //if the driver and passenger is allready in the carpool throw error
                Driver currentDriver = new Driver();
                CarpoolModelDto toDeleteCarpoolEntry = new CarpoolModelDto();
                CarpoolModel toAddEditedCarpoolEntry = new CarpoolModel();
                var newListOfRemainingPassengers = new List<string>();

                if (findExistingCarpool != null)
                {
                    return null;
                }
                //check if the driver has a carpool, if yes, add user to carpool
                else if (findDriverInCarpool != null)
                {
                    var elementsOfCurrentCarpool = findDriverInCarpool.Split(',');
                    toDeleteCarpoolEntry.Driver = elementsOfCurrentCarpool[0];
                    toAddEditedCarpoolEntry.Driver = elementsOfCurrentCarpool[0];
                    for (int i = 1; i < elementsOfCurrentCarpool.Length; i++)
                    {
                        newListOfRemainingPassengers.Add(elementsOfCurrentCarpool[i]);
                    }
                    newListOfRemainingPassengers.Add(passengerID + inputPassengerID);
                    toAddEditedCarpoolEntry.Passengers = newListOfRemainingPassengers;
                    //delete old string
                    this._carpoolDataService.DeleteCarpoolDaService(MapToCarpoolBu(toDeleteCarpoolEntry));
                    // add new string
                    this._carpoolDataService.AddCarpoolDaService(toAddEditedCarpoolEntry);
                    //reduce the amount of free places from the current driver with 1
                    SMReduceFreePlacesWithOne(findDriverInDrivers, currentDriver);
                    return toAddEditedCarpoolEntry;
                }
                //if the driver is not in the carpool lists, make new carpool
                else if (findDriverInCarpool == null)
                {
                    toAddEditedCarpoolEntry.Driver = "\n" + driverID + inputDriverID;
                    newListOfRemainingPassengers.Add(passengerID + inputPassengerID);
                    toAddEditedCarpoolEntry.Passengers = newListOfRemainingPassengers;
                    this._carpoolDataService.AddCarpoolDaService(toAddEditedCarpoolEntry);
                    //reduce the amount of free places from the current driver with 1
                    SMReduceFreePlacesWithOne(findDriverInDrivers, currentDriver);
                    return toAddEditedCarpoolEntry;
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
            var findCarpool = carpools.FirstOrDefault(e => e.Contains(driverID + id));
            if (findCarpool != null)
            {
                CarpoolModelDto resultNew = new CarpoolModelDto();
                var subElement = findCarpool.Split(',');
                var numberOfCarpoolPassengers = subElement.Length - 1; //take length of the carpool IDs and substract 1 that is the driver. the rest are passengers
                resultNew.Driver = subElement[0];
                this._carpoolDataService.DeleteCarpoolDaService(MapToCarpoolBu(resultNew));
                //change free places for the driver erased from the carpool
                var findDriver = driver.First(e => e.Contains(driverID + id));
                Driver currentDriver = new Driver();
                SMRecoverAllFreePlacesFromPassengers(numberOfCarpoolPassengers, findDriver, currentDriver);
                _driverDataService.EditDriverDaService(currentDriver);
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
            var foundDriver = driver.FirstOrDefault(e => e.Contains(driverID + inputDriverID.ToUpper()));
            var foundPassenger = passenger.FirstOrDefault(e => e.Contains(passengerID + inputPassengerID.ToUpper()));
            var bothDriverAndPassengerAreInACarpool = carpools.FirstOrDefault(e => e.Contains(driverID + inputDriverID) && e.Contains(passengerID + inputPassengerID));
            if (foundDriver != null && foundPassenger != null && bothDriverAndPassengerAreInACarpool != null)
            {
                var elementsOfCurrentCarpool = bothDriverAndPassengerAreInACarpool.Split(',');
                var newListOfRemainingPassengers = new List<string>();
                Driver currentDriver = new Driver();
                CarpoolModelDto toDeleteCarpoolEntry = new CarpoolModelDto();
                CarpoolModel toAddEditedCarpoolEntry = new CarpoolModel();
                var subElement = bothDriverAndPassengerAreInACarpool.Split(',');
                toDeleteCarpoolEntry.Driver = subElement[0];
                toAddEditedCarpoolEntry.Driver = subElement[0];
                //delete old string
                this._carpoolDataService.DeleteCarpoolDaService(MapToCarpoolBu(toDeleteCarpoolEntry));
                if (elementsOfCurrentCarpool.Length != 2)
                {
                    for (int i = 1; i < elementsOfCurrentCarpool.Length; i++)
                    {
                        if (elementsOfCurrentCarpool[i] != passengerID + inputPassengerID)
                            newListOfRemainingPassengers.Add(elementsOfCurrentCarpool[i]);
                    }
                    var result = string.Join(",", newListOfRemainingPassengers.ToArray());
                    toAddEditedCarpoolEntry.Passengers = newListOfRemainingPassengers;
                    // add new string
                    this._carpoolDataService.AddCarpoolDaService(toAddEditedCarpoolEntry);
                }
                //change free places for the driver erased from the carpool
                SMRecoverOneFreePlace(foundDriver, currentDriver);
                _driverDataService.EditDriverDaService(currentDriver);
                return toDeleteCarpoolEntry;
            }
            else
            {
                return null;
            }
        }

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
        private void SMReduceFreePlacesWithOne(string? findDriverInDrivers, Driver currentDriver)
        {
            var subElementDriver = findDriverInDrivers.Split(',');
            currentDriver.FreePlaces = Convert.ToInt32(subElementDriver[1]) - 1;
            SMCurrentDriver(currentDriver, subElementDriver);
            _driverDataService.EditDriverDaService(currentDriver);
        }

        private static void SMCurrentDriver(Driver currentDriver, string[] subElementDriver)
        {
            currentDriver.ID = subElementDriver[0];
            currentDriver.FirstName = subElementDriver[2];
            currentDriver.LastName = subElementDriver[3];
            currentDriver.CarTypeMake = subElementDriver[4];
            currentDriver.StartingCity = subElementDriver[5];
            currentDriver.Destination = subElementDriver[6];
        }

        /// <summary>
        /// Submethod that adds back one free place to the current number of free places of a driver
        /// </summary>
        private void SMRecoverOneFreePlace(string findDriver, Driver currentDriver)
        {
            var subElementDriver = findDriver.Split(',');
            currentDriver.FreePlaces = Convert.ToInt32(subElementDriver[1]) + 1;
            SMCurrentDriver(currentDriver, subElementDriver);
        }

        /// <summary>
        /// Submethod that adds back all the free places (occupied by passengers) to the driver after removing all passengers
        /// </summary>
        private void SMRecoverAllFreePlacesFromPassengers(int numberOfCarpoolPassengers, string findDriver, Driver currentDriver)
        {
            var subElementDriver = findDriver.Split(',');
            currentDriver.FreePlaces = Convert.ToInt32(subElementDriver[1]) + numberOfCarpoolPassengers;
            SMCurrentDriver(currentDriver, subElementDriver);
        }
    }
}
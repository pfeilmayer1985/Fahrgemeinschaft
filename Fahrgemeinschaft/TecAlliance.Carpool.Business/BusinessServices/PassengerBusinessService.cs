using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public class PassengerBusinessService : IPassengerBusinessService
    {
        private IPassengerDataService _passengerDataService;
        const string passengerID = "PID#";
        string[] passenger;
        public PassengerBusinessService(IPassengerDataService passengerDataService)
        {
            _passengerDataService = passengerDataService;
            passenger = _passengerDataService.ListAllPassengersService();
        }

        public PassengerBusinessService()
        {

        }

        /// <summary>
        /// This method will return a detailed list with the passenger IDs and infos
        /// </summary>
        public Passenger[] ListAllPassengersData()
        {
            Passenger[] resultNew = new Passenger[passenger.Length];
            int i = 0;
            foreach (string element in passenger)
            {
                Passenger result = new Passenger();
                var subElement = element.Split(',');
                result.ID = subElement[0];
                result.FirstName = subElement[1];
                result.LastName = subElement[2];
                result.StartingCity = subElement[3];
                result.Destination = subElement[4];
                resultNew[i] = result;
                i++;
            }
            return resultNew;
        }

        /// <summary>
        /// This method will return one detailed passenger info based on a search after his IDs
        /// </summary>
        public Passenger ListPassengerDataById(string id)
        {
            var findPassenger = passenger.FirstOrDefault(e => e.Contains(passengerID + id));
            if (findPassenger != null)
            {
                Passenger result = new Passenger();
                var subElement = findPassenger.Split(',');
                result.ID = subElement[0];
                result.FirstName = subElement[1];
                result.LastName = subElement[2];
                result.StartingCity = subElement[3];
                result.Destination = subElement[4];
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will add a new passenger in the file
        /// </summary>
        public Passenger AddPassengerBuService(PassengerModelDto passengerModelDto)
        {
            Passenger result = new Passenger()
            {
                ID = "",
                FirstName = passengerModelDto.FirstName,
                LastName = passengerModelDto.LastName,
                StartingCity = passengerModelDto.StartingCity,
                Destination = passengerModelDto.Destination
            };
            result.ID = SMGenerateAndCheckPassenger(passengerID + passengerModelDto.FirstName.Substring(0, 3).ToUpper() + passengerModelDto.LastName.Substring(0, 3).ToUpper());
            this._passengerDataService.AddPassengerDaService(result);
            return result;
        }

        /// <summary>
        /// This method will "edit" the infos of a passenger based on his ID
        /// </summary>
        public PassengerModelDto EditPassengerBuService(string id, PassengerModelDto dtoPassenger)
        {
            var findPassenger = passenger.FirstOrDefault(e => e.Contains(passengerID + id));
            if (findPassenger != null)
            {
                var subElement = findPassenger.Split(',');
                Passenger result = new Passenger()
                {
                    ID = subElement[0],
                    FirstName = dtoPassenger.FirstName,
                    LastName = dtoPassenger.LastName,
                    StartingCity = dtoPassenger.StartingCity,
                    Destination = dtoPassenger.Destination
                };
                this._passengerDataService.EditPassengerDaService(result);
                return MapToModelDtoPassenger(result);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will delete a passenger based on his ID
        /// </summary>
        public Passenger DeletePassengerBuService(string id)
        {
            var findPassenger = passenger.FirstOrDefault(e => e.Contains(passengerID + id));
            if (findPassenger != null)
            {
                Passenger resultNew = new Passenger();
                var subElement = findPassenger.Split(',');
                resultNew.ID = subElement[0];
                this._passengerDataService.DeletePassengerDaService(resultNew);
                return resultNew;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Remapping a Passenger obj to a DTO obj
        /// </summary>
        public PassengerModelDto MapToModelDtoPassenger(Passenger passenger)
        {
            PassengerModelDto remappedPassenger = new PassengerModelDto()
            {
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                StartingCity = passenger.StartingCity,
                Destination = passenger.Destination
            };
            return remappedPassenger;
        }

        /// <summary>
        /// This submethod will generate a Passenger ID based on the first 3 letters in the First Name and first 3 letters in the Last Name
        /// If the ID allready exists in the file, a new ID will be generated, where the last character of the first 3 letters both in First Name and Last Name
        /// are replaced by a random character.
        /// </summary>
        public string SMGenerateAndCheckPassenger(string id)
        {
            foreach (var passenger in passenger)
            {
                var splittedPassenger = passenger.Split(',');
                if (splittedPassenger[0] == id)
                {
                    string partOne = id.Substring(0, 6);
                    string partTwo = id.Substring(7, 2);
                    id = partOne + SMGetaRandomChar() + partTwo + SMGetaRandomChar();
                    //id = id.Substring(0, 8) + GetaRandomChar() + GetaRandomChar();
                }
            }
            return id;
        }

        /// <summary>
        /// This submethod will return random character to be used in the ID generator
        /// </summary>
        public string SMGetaRandomChar()
        {
            string chars = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            var splittedChars = chars.Split(',').ToArray();
            Random rand = new Random();
            int num = rand.Next(0, splittedChars.Count() - 1);
            return splittedChars[num].ToString();
        }
    }
}
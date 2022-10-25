using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{

    public class PassengerBusinessService
    {
        private PassengerDataService passengerDataService;
        private CarpoolDataService carpoolDataService;

        public PassengerBusinessService()
        {
            passengerDataService = new PassengerDataService();

        }

        /// <summary>
        /// This method will return a detailed list with the passenger IDs and infos
        /// </summary>
        public Passenger[] ListAllPassengersData()
        {

            var passengers = passengerDataService.ListAllPassengersService();

            //var result = from x in passengerDataService.ListAllPassengersService()
            //             where x.Any()
            //             select x;
            Passenger[] resultNew = new Passenger[passengers.Length];
            int i = 0;
            foreach (string element in passengers)
            {
                Passenger newPassengerModelDto = new Passenger();
                // Element is like: PID#FELFAR,Felician,Farcas,Schrozberg,Wien
                var subElement = element.Split(',');
                newPassengerModelDto.ID = subElement[0];
                newPassengerModelDto.FirstName = subElement[1];
                newPassengerModelDto.LastName = subElement[2];
                newPassengerModelDto.StartingCity = subElement[3];
                newPassengerModelDto.Destination = subElement[4];
                resultNew[i] = newPassengerModelDto;
                i++;
            }
            return resultNew;
        }

        /// <summary>
        /// This method will return one detailed passenger info based on a search after his IDs
        /// </summary>
        public Passenger ListPassengerDataById(string id)
        {

            var passenger = passengerDataService.ListAllPassengersService();
            var findPassenger = passenger.FirstOrDefault(e => e.Contains("PID#" + id));

            if (findPassenger != null)
            {
                Passenger result = new Passenger();
                var subElementofPassenger = findPassenger.Split(',');
                result.ID = subElementofPassenger[0];
                result.FirstName = subElementofPassenger[1];
                result.LastName = subElementofPassenger[2];
                result.StartingCity = subElementofPassenger[3];
                result.Destination = subElementofPassenger[4];
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
            result.ID = SMGenerateAndCheckPassenger("PID#" + passengerModelDto.FirstName.Substring(0, 3).ToUpper() + passengerModelDto.LastName.Substring(0, 3).ToUpper());

            passengerDataService.AddPassengerDaService(result);

            return result;


        }

        /// <summary>
        /// This method will "edit" the infos of a passenger based on his ID
        /// </summary>
        public PassengerModelDto EditPassengerBuService(string id, PassengerModelDto dtoPassenger)
        {

            var passengers = passengerDataService.ListAllPassengersService();
            var findPassenger = passengers.FirstOrDefault(e => e.Contains("PID#" + id));

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

                passengerDataService.EditPassengerDaService(result);

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

            var passengers = passengerDataService.ListAllPassengersService();
            var findPassenger = passengers.FirstOrDefault(e => e.Contains("PID#" + id));

            if (findPassenger != null)
            {
                Passenger resultNew = new Passenger();
                var subElement = findPassenger.Split(',');
                resultNew.ID = subElement[0];

                passengerDataService.DeletePassengerDaService(resultNew);

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
        private PassengerModelDto MapToModelDtoPassenger(Passenger passenger)
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

        /* private Passenger MapToDriver(PassengerModelDto dtoPassenger)
         {
             Passenger remappedPassengerDto = new Passenger()
             {
                 ID = dtoPassenger.ID,
                 FirstName = dtoPassenger.FirstName,
                 LastName = dtoPassenger.LastName,
                 StartingCity = dtoPassenger.StartingCity,
                 Destination = dtoPassenger.Destination

             };

             return remappedPassengerDto;

         }  */

        /// <summary>
        /// This submethod will generate a Passenger ID based on the first 3 letters in the First Name and first 3 letters in the Last Name
        /// If the ID allready exists in the file, a new ID will be generated, where the last character of the first 3 letters both in First Name and Last Name
        /// are replaced by a random character.
        /// </summary>
        public string SMGenerateAndCheckPassenger(string id)
        {
            var passengerID = passengerDataService.ListAllPassengersService();

            foreach (var passenger in passengerID)
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
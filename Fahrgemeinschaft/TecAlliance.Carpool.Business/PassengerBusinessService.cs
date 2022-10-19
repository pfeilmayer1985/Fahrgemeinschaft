using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;

namespace TecAlliance.Carpool.Business
{

    public class PassengerBusinessService
    {
        private PassengerDataService passengerDataService;

        public PassengerBusinessService()
        {
            passengerDataService = new PassengerDataService();

        }

        public PassengerModelDto[] ListAllPassengersData()
        {

            var passengers = passengerDataService.ListAllPassengersService();

            //var result = from x in passengerDataService.ListAllPassengersService()
            //             where x.Any()
            //             select x;
            PassengerModelDto[] resultNew = new PassengerModelDto[passengers.Length];
            int i = 0;
            foreach (string element in passengers)
            {
                PassengerModelDto newPassengerModelDto = new PassengerModelDto();
                // Element is like: PID#FELFAR,Felician Farcas,Schrozberg,Wien
                var subElement = element.Split(',');
                newPassengerModelDto.ID = subElement[0];
                newPassengerModelDto.Name = subElement[1];
                newPassengerModelDto.StartingCity = subElement[2];
                newPassengerModelDto.Destination = subElement[3];
                resultNew[i] = newPassengerModelDto;
                i++;
            }
            return resultNew;
        }

        public PassengerModelDto ListPassengerDataById(string id)
        {

            var passenger = passengerDataService.ListAllPassengersService();
            var findPassenger = passenger.First(e => e.Contains("PID#" + id));
            PassengerModelDto result = new PassengerModelDto();
            var subElementofPassenger = findPassenger.Split(',');
            result.ID = subElementofPassenger[0];
            result.Name = subElementofPassenger[1];
            result.StartingCity = subElementofPassenger[2];
            result.Destination = subElementofPassenger[3];
            return result;
        }

    }
}
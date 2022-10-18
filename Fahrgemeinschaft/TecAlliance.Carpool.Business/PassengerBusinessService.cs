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

            var result = passengerDataService.ListAllPassengersService();

            //var result = from x in passengerDataService.ListAllPassengersService()
            //             where x.Any()
            //             select x;
            PassengerModelDto[] resultNew = new PassengerModelDto[result.Length];
            int i = 0;
            foreach (string element in result)
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
    }
}
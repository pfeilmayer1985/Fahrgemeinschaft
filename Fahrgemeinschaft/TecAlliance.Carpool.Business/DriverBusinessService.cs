using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;

namespace TecAlliance.Carpool.Business
{

    public class DriverBusinessService
    {
        private DriverDataService driverDataService;

        public DriverBusinessService()
        {
            driverDataService = new DriverDataService();

        }

        public DriverModelDto[] ListAllDriverData()
        {

            var result = driverDataService.ListAllDriversService();

            //var result = from x in passengerDataService.ListAllPassengersService()
            //             where x.Any()
            //             select x;
            DriverModelDto[] resultNew = new DriverModelDto[result.Length];
            int i = 0;
            foreach (string element in result)
            {
                DriverModelDto newDriverModelDto = new DriverModelDto();
                var subElement = element.Split(',');
                newDriverModelDto.ID = subElement[0];
                newDriverModelDto.FreePlaces = Convert.ToInt32(subElement[1]);
                newDriverModelDto.Name = subElement[2];
                newDriverModelDto.CarTypeMake = subElement[3];
                newDriverModelDto.StartingCity = subElement[4];
                newDriverModelDto.Destination = subElement[5];
                resultNew[i] = newDriverModelDto;
                i++;
            }
            return resultNew;



        }
    }
}
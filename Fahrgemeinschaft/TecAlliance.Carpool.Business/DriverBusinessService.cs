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

            var drivers = driverDataService.ListAllDriversService();

            //var result = from x in passengerDataService.ListAllPassengersService()
            //             where x.Any()
            //             select x;
            DriverModelDto[] resultNew = new DriverModelDto[drivers.Length];
            int i = 0;
            foreach (string element in drivers)
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

        public DriverModelDto ListDriverById(string id)
        {

            var drivers = driverDataService.ListAllDriversService();

            var findDriver = drivers.First(e => e.Contains("DID#" + id));
            DriverModelDto resultNew = new DriverModelDto();
            var subElement = findDriver.Split(',');
            resultNew.ID = subElement[0];
            resultNew.FreePlaces = Convert.ToInt32(subElement[1]);
            resultNew.Name = subElement[2];
            resultNew.CarTypeMake = subElement[3];
            resultNew.StartingCity = subElement[4];
            resultNew.Destination = subElement[5];

            return resultNew;

        }
    }
}
using System.Xml.Linq;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data;

namespace TecAlliance.Carpool.Business
{

    public class CarpoolBusinessService
    {
        private CarpoolDataService carpoolDataService;

        public CarpoolBusinessService()
        {
            carpoolDataService = new CarpoolDataService();

        }

        public CarpoolModelDto[] ListAllCarpoolsData()
        {

            var carpools = carpoolDataService.ListAllCarpoolsService();


            CarpoolModelDto[] resultNew = new CarpoolModelDto[carpools.Length];
            int i = 0;
            foreach (string element in carpools)
            {
                CarpoolModelDto newCarpoolModelDto = new CarpoolModelDto();
                newCarpoolModelDto.Passengers = new List<string>();
                var subElement = element.Split(',');
                newCarpoolModelDto.Driver = subElement[0];

                for (int j = 1; j < subElement.Length; j++)
                {
                    newCarpoolModelDto.Passengers.Add(subElement[j]);
                }

                resultNew[i] = newCarpoolModelDto;
                i++;
            }
            return resultNew;
        }

        public CarpoolModelDto ListCarpoolById(string id)
        {

            var carpool = carpoolDataService.ListAllCarpoolsService();

            var findCarpool = carpool.First(e => e.Contains("DID#" + id));
            //var findCarpool = result.Where(e => e.Contains("DID#" + id)).ToList();
            CarpoolModelDto resultNew = new CarpoolModelDto();
            var subElement = findCarpool.Split(',');
            resultNew.Driver = subElement[0];

            resultNew.Passengers = new List<string>();
            for (int j = 1; j < subElement.Length; j++)
            {

                resultNew.Passengers.Add(subElement[j]);
            }
           
            /*
            foreach (string element in findCarpool)
            {
                CarpoolModelDto newCarpoolModelDto = new CarpoolModelDto();
                newCarpoolModelDto.Passengers = new List<string>();
                var subElement = element.Split(',');
                newCarpoolModelDto.Driver = subElement[0];

                for (int j = 1; j < subElement.Length; j++)
                {
                    newCarpoolModelDto.Passengers.Add(subElement[j]);
                }

                resultNew = newCarpoolModelDto;
            }
            */
           
            return resultNew;

        }

    }
}
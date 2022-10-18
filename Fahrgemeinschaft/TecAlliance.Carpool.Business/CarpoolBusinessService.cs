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

            var result = carpoolDataService.ListAllCarpoolsService();


            CarpoolModelDto[] resultNew = new CarpoolModelDto[result.Length];
            int i = 0;
            foreach (string element in result)
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

        public CarpoolModelDto[] ListCarpoolsById(string id)
        {

            var result = carpoolDataService.ListAllCarpoolsService();


            CarpoolModelDto[] resultNew = new CarpoolModelDto[result.Length];
            int i = 0;
            foreach (string element in result)
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

    }
}
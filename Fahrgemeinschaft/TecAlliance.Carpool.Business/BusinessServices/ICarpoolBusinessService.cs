using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface ICarpoolBusinessService
    {
        CarpoolModel? AddCarpoolByPassengerAndDriverIdBu(string inputDriverID, string inputPassengerID);
        CarpoolModelDto DeleteCarpoolByDriverIdBu(string id);
        CarpoolModelDto[] ListAllCarpoolsDataBu();
        CarpoolModelDto ListCarpoolByIdBu(string id);
        CarpoolModelDto RemovePassengerFromCarpoolByPassengerIdAndDriverIdBu(string inputDriverID, string inputPassengerID);
    }
}
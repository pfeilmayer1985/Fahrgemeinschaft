using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface INewUserBusinessService
    {
        NewUserBaseModelData AddUserBusineeService(NewUserBaseModelData newUserModel);
        NewUserBaseModelDto ConvertUserToDto(NewUserBaseModelData user);
        List<NewUserBaseModelData> ListAllUserData();
        NewUserBaseModelDto ListUserDataByEmail(string email);
    }
}
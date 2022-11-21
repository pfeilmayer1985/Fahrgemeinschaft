using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface INewUserBusinessService
    {
        NewUserBaseModelData AddUserBusineeService(NewUserBaseModelData newUserModel);
        List<NewUserBaseModelData> ListAllUserData();
        List<NewUserBaseModelData> ListUserDataByEmail(string email);
        //NewUserBaseModelDto MapToModelDtoUsers(NewUserBaseModelData user);
    }
}
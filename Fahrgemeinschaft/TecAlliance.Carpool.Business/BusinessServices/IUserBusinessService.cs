using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Business
{
    public interface IUserBusinessService
    {
        UserBaseModelData AddUserBusineeService(UserBaseModelData newUserModel);
        UserBaseModelDto ConvertUserToDto(UserBaseModelData user);
        string DeleteUserBusinessService(string email, string password);
        UserBaseModelData EditUserBusinessService(string email, string password, UserBaseModelData user);
        List<UserBaseModelData> ListAllUserData();
        UserBaseModelDto ListUserDataByEmail(string email);
    }
}
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface IUsersDataServiceSQL
    {
        void AddUserDataService(UserBaseModelData user);
        void DeleteUserDataService(string email);
        void EditUserDataService(UserBaseModelData user);
        List<UserBaseModelData> ListAllUsersDataService();
        UserBaseModelData ListUserByEmailDataService(string email);
    }
}
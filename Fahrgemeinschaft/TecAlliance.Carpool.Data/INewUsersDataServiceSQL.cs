using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Data
{
    public interface INewUsersDataServiceSQL
    {
        void AddUserDataService(NewUserBaseModelData user);
        void DeleteUserDataService(NewUserBaseModelData user);
        void EditUserDataService(NewUserBaseModelData user);
        List<NewUserBaseModelData> ListAllUsersDataService();
        NewUserBaseModelData ListUserByEmailDataService(string email);
    }
}
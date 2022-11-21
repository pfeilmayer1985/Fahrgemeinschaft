using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TecAlliance.Carpool.Business
{
    public class NewUserBusinessService : INewUserBusinessService
    {
        private INewUsersDataServiceSQL _newUserDataService;
        List<NewUserBaseModelData> userList;
        public NewUserBusinessService(INewUsersDataServiceSQL newUserDataService)
        {
            _newUserDataService = newUserDataService;
            userList = _newUserDataService.ListAllUsersDataService();
        }

        /// <summary>
        /// This method will return a detailed list with the passenger IDs and infos
        /// </summary>
        public List<NewUserBaseModelData> ListAllUserData()
        {
            return userList;
        }

        /*  public NewUserBaseModelDto MapToModelDtoUsers(NewUserBaseModelData user) 
          {
              NewUserBaseModelDto remappedUser = new NewUserBaseModelDto()
              {
                  Email = user.Email,
                  PhoneNo = user.PhoneNo,
                  FirstName = user.FirstName,
                  LastName = user.LastName,
                  IsDriver = user.IsDriver
              };
              return remappedUser;
          }

          */

        /// <summary>
        /// This method will return one detailed passenger info based on a search after his IDs
        /// </summary>
        public List<NewUserBaseModelData> ListUserDataByEmail(string email)
        {
            List<NewUserBaseModelData> specificUser = _newUserDataService.ListUserByEmailDataService(email);
            return specificUser;
        }

        /// <summary>
        /// This method will add a new passenger in the file
        /// </summary>
        public NewUserBaseModelData AddUserBusineeService(NewUserBaseModelData newUserModel)
        {
            NewUserBaseModelData newUser = new NewUserBaseModelData()
            {
                Email = newUserModel.Email,
                PhoneNo = newUserModel.PhoneNo,
                Password = newUserModel.Password,
                LastName = newUserModel.LastName,
                FirstName = newUserModel.FirstName,
                IsDriver = newUserModel.IsDriver
            };
            _newUserDataService.AddUserDataService(newUser);

            return newUser;

        }

        /*
        /// <summary>
        /// This method will "edit" the infos of a passenger based on his ID
        /// </summary>
        public NewUserBaseModel EditUserBusinessService(string id, NewUserBaseModel user)
        {

        }

        /// <summary>
        /// This method will delete a passenger based on his ID
        /// </summary>
        public UserBaseModelData DeleteUserBusinessService(string id)
        {

        }
        */
    }
}
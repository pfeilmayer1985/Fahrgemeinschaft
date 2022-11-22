using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;
using TecAlliance.Carpool.Data;
using System.Linq;

namespace TecAlliance.Carpool.Business
{
    public class UserBusinessService : IUserBusinessService
    {
        private IUsersDataServiceSQL _newUserDataService;
        List<UserBaseModelData> userList;
        public UserBusinessService(IUsersDataServiceSQL newUserDataService)
        {
            _newUserDataService = newUserDataService;
        }

        /// <summary>
        /// This method will return a detailed list with the passenger IDs and infos
        /// </summary>
        public List<UserBaseModelData> ListAllUserData()
        {
            userList = _newUserDataService.ListAllUsersDataService();
            return userList;
        }

        public UserBaseModelDto ConvertUserToDto(UserBaseModelData user)
        {
            return new UserBaseModelDto(user.ID, user.Email, user.PhoneNo, user.FirstName, user.LastName, user.IsDriver);
        }

        /// <summary>
        /// This method will return one detailed passenger info based on a search after his IDs
        /// </summary>
        public UserBaseModelDto ListUserDataByEmail(string email)
        {
            userList = _newUserDataService.ListAllUsersDataService();
            var findUser = userList.FirstOrDefault(e => e.Email.Equals(email.ToLower()));
            if (findUser != null)
            {
                return ConvertUserToDto(_newUserDataService.ListUserByEmailDataService(email));
            }

            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method will add a new passenger in the file
        /// </summary>
        public UserBaseModelData AddUserBusineeService(UserBaseModelData newUserModel)
        {

            UserBaseModelData newUser = new UserBaseModelData()
            {
                Email = newUserModel.Email.ToLower(),
                PhoneNo = newUserModel.PhoneNo,
                Password = newUserModel.Password,
                LastName = newUserModel.LastName,
                FirstName = newUserModel.FirstName,
                IsDriver = newUserModel.IsDriver
            };
            _newUserDataService.AddUserDataService(newUser);

            return newUser;


        }


        /// <summary>
        /// This method will "edit" the infos of a passenger based on his ID
        /// </summary>
        public UserBaseModelData EditUserBusinessService(string email, string password, UserBaseModelData user)
        {
            userList = _newUserDataService.ListAllUsersDataService();

            var findUser = userList.FirstOrDefault(e => e.Email.Equals(email.ToLower()));

            if (findUser != null && findUser.Password == password)
            {

                UserBaseModelData editedUser = new UserBaseModelData()
                {
                    Email = user.Email.ToLower(),
                    PhoneNo = user.PhoneNo,
                    Password = user.Password,
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    IsDriver = user.IsDriver
                };
                _newUserDataService.EditUserDataService(editedUser);

                return editedUser;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// This method will delete a passenger based on his ID
        /// </summary>
        public string DeleteUserBusinessService(string email, string password)
        {
            userList = _newUserDataService.ListAllUsersDataService();

            var findUser = userList.FirstOrDefault(e => e.Email.Equals(email.ToLower()));

            if (findUser != null && findUser.Password == password)
            {
                string userToDelete = findUser.Email.ToLower();
                _newUserDataService.DeleteUserDataService(userToDelete);
                return userToDelete;
            }
            else
            {
                return null;
            }

        }

    }
}
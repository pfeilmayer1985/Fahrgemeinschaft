using Microsoft.AspNetCore.Mvc;
using System.Text;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Models;
using TecAlliance.Carpool.Data.Models;

namespace TecAlliance.Carpool.Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private IUserBusinessService _newUserBusinessService;

        public UsersController(ILogger<UsersController> logger, IUserBusinessService newUserBusinessService)
        {
            StringBuilder test = new StringBuilder();
            _newUserBusinessService = newUserBusinessService;
            _logger = logger;
        }

        [HttpGet]
        //[Route("api/CarPoolApi/GetSpecificUser")]
        public async Task<ActionResult<IEnumerable<UserBaseModelData>>> GetAllUsers()
        {
            List<UserBaseModelData> items = _newUserBusinessService.ListAllUserData();
            return items;
        }
        
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/GetPassenger/{id}")]
        public async Task<ActionResult<UserBaseModelDto>> GetUserByEmail(string email)
        {
            UserBaseModelDto item = _newUserBusinessService.ListUserDataByEmail(email);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return item;
            }
        }

        [HttpPost]
        //[Route("api/CarPoolApi/AddUser")]
        public async Task<ActionResult<UserBaseModelData>> AddUser(UserBaseModelData user)
        {
            UserBaseModelData item = _newUserBusinessService.AddUserBusineeService(user);
            return item;
        }

        
        [HttpPut("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/EditDriverById")]
        public async Task<IActionResult> UpdateUser(string email, string password, UserBaseModelData user)
        {
            var item = _newUserBusinessService.EditUserBusinessService(email.ToLower(), password, user);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _newUserBusinessService.EditUserBusinessService(email.ToLower(), password, user);
                return NoContent();
            }
        }

        
        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeletePassengerByEmail")]
        public async Task<IActionResult> DeleteUser(string email, string password)
        {
            var item = _newUserBusinessService.DeleteUserBusinessService(email.ToLower(), password);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _newUserBusinessService.DeleteUserBusinessService(email.ToLower(), password);
                return NoContent();
            }
        }
    }
}
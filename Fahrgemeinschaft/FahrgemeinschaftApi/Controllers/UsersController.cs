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
        private INewUserBusinessService _newUserBusinessService;

        public UsersController(ILogger<UsersController> logger, INewUserBusinessService newUserBusinessService)
        {
            StringBuilder test = new StringBuilder();
            _newUserBusinessService = newUserBusinessService;

            _logger = logger;
        }

        [HttpGet]
        //[Route("api/CarPoolApi/GetSpecificUser")]
        public async Task<ActionResult<IEnumerable<NewUserBaseModelData>>> GetAllUsers()
        {
            List<NewUserBaseModelData> items = _newUserBusinessService.ListAllUserData();
            return items;
        }
        
        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/GetPassenger/{id}")]
        public async Task<ActionResult<NewUserBaseModelData>> GetUserByEmail(string email)
        {
            List<NewUserBaseModelData> item = _newUserBusinessService.ListUserDataByEmail(email.ToUpper());

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
        public async Task<ActionResult<NewUserBaseModelData>> AddUser(NewUserBaseModelData user)
        {
            NewUserBaseModelData item = _newUserBusinessService.AddUserBusineeService(user);
            return item;
        }

        /*
        [HttpPut("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/EditDriverById")]
        public async Task<IActionResult> UpdateUser(string email, NewUserBaseModel user)
        {
            var item = _newUserBusinessService.EditUserBusinessService(email.ToUpper(), user);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _newUserBusinessService.EditUserBusinessService(email.ToUpper(), user);
                return NoContent();
            }
        }
        /*
        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Route("api/CarPoolApi/DeletePassengerByEmail")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var item = _newUserBusinessService.DeleteUserBusinessService(email.ToUpper());
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                _newUserBusinessService.DeleteUserBusinessService(email.ToUpper());
                return NoContent();
            }
        } */
    }
}
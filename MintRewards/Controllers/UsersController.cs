using Microsoft.AspNetCore.Mvc;
using MintRewards.ServiceResponder;
using MintRewards.Model;
 
using MintRewards.Services;

namespace MintRewards.Controllers
{
    [ApiExplorerSettings(GroupName ="User")]
    [Route("User")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUsersService _userService;

        public UsersController(IConfiguration configuration,IUsersService userservice)
        {
            _configuration = configuration;
            _userService = userservice;
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(SignIn dto) 
        {
            if (!ModelState.IsValid)
                return Ok(ResponseHelper.GetValidationFailureResponse(ModelState));
            var data = await _userService.AuthenticateUser(dto);
            if (data!=null)
            {

                if (data != null)
                {
                    Users item = new Users { Id = data.Id, RoleId = data.RoleId, UserName = data.UserName };
                    //data.Features = await _userService.GetFeatureByRoleId(data.RoleId);
                    data.Token = await _userService.Authenticate(item);
                    return Ok(ResponseHelper.GetSuccessResponse(data));
                }
            }
            return Ok();
        }
        [HttpPost("SignUp")]
        public async Task<ResponseModel> SignUp(SignUp dto)
        {
            try
            {
                var data = await _userService.CreateAccount(dto);
                return ResponseHelper.GetSuccessResponse("Success");

            }
            catch (Exception err)
            {
                return ResponseHelper.GetExceptionResponse(err);
            }

        }


    }
}

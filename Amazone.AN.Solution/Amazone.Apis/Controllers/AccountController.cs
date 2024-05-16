using Amazone.Apis.Dtos;
using Amazone.Apis.Dtos.Account;
using Amazone.Apis.Errors;
using Amazone.Apis.Extentions;
using Amazone.Core.Entities.Identity;
using Amazone.Core.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Amazone.Apis.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                  IAuthService authService,
                                  IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {

            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
                return Unauthorized(new ResponseApi(401, "Invalid Login"));
            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ResponseApi(401, "Invalid Login"));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreatTokenAsync(user, _userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {
            var user = new ApplicationUser()
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                UserName = input.Email.Split("@")[0],
                PhoneNumber = input.Phone

            };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
                return BadRequest(new ValidationErrorResponse { Errors = result.Errors.Select(e => e.Description) });

            return Ok(new UserDto()
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                Token = await _authService.CreatTokenAsync(user, _userManager)
            });

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreatTokenAsync(user, _userManager)
            });

        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetAddressForUser()
        {

            var user = await _userManager.FindUserwithaddressAsync(User);
            var mapuser = _mapper.Map<AddressDto>(user.Addresses);
            return Ok(mapuser);



        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Address>> UpdateAddressForUser(AddressDto address)
        {
            var updateAddress = _mapper.Map<Address>(address);

            var user = await _userManager.FindUserwithaddressAsync(User);

            updateAddress.Id = user.Addresses.Id;

            user.Addresses = updateAddress;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return BadRequest(new ValidationErrorResponse() { Errors = result.Errors.Select(u => u.Description) });
            return Ok(address);

        }
    }

}

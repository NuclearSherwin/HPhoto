using AutoMapper;
using HPhoto.Authorization;
using HPhoto.Dtos.UserDto;
using HPhoto.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HPhoto.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        await _userService.Register(model);
        return Ok(new { message = "Registration successful" });
    }
    
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(AuthenticationRequest model)
    {
        var response = await _userService.Authenticate(model);
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        return Ok(user);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest model)
    {
        await _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}
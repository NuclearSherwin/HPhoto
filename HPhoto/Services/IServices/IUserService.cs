using HPhoto.Dtos.UserDto;
using HPhoto.Model;

namespace HPhoto.Services.IServices;

public interface IUserService
{
    Task<AuthenticationResponse> Authenticate(AuthenticationRequest model);
    Task<IEnumerable<User>> GetAll();
    Task<User> GetById(int id);
    Task Register(RegisterRequest model);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
}
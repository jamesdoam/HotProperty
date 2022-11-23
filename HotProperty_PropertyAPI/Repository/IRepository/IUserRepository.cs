using HotProperty_PropertyAPI.Models.Dto;
using HotProperty_PropertyAPI.Models;

namespace HotProperty_PropertyAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegistrationRequestDTO registrationRequestDTO);
    }
}

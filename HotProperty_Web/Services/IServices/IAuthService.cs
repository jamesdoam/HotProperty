using HotProperty_Web.Models.Dto;

namespace HotProperty_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO loginDTO);
        Task<T> RegisterAsync<T>(RegistrationRequestDTO registerDTO);
    }
}

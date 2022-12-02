using HotProperty_Utility;
using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;
using HotProperty_Web.Services.IServices;

namespace HotProperty_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string propertyUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration config) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            propertyUrl = config.GetValue<string>("ServiceUrls:PropertyAPI");
        }
        public Task<T> LoginAsync<T>(LoginRequestDTO loginDTO)
        {
            APIRequest request = new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginDTO,
                Url = propertyUrl + "/api/v1/UserAuth/login"
            };
            return SendAsync<T>(request);
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO registerDTO)
        {
            APIRequest request = new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registerDTO,
                Url = propertyUrl + "/api/v1/UserAuth/register"
            };
            return SendAsync<T>(request);
        }
    }
}

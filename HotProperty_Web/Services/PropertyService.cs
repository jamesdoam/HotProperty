using HotProperty_Utility;
using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;
using HotProperty_Web.Services.IServices;

namespace HotProperty_Web.Services
{
    public class PropertyService : BaseService, IPropertyService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string propertyUrl;

        public PropertyService(IHttpClientFactory clientFactory,IConfiguration configuration) :base(clientFactory)
        {
            _clientFactory = clientFactory;
            propertyUrl = configuration.GetValue<string>("ServiceUrls:PropertyAPI");
            //this is the localhost link in the appsetings.json
        }
        public Task<T> CreateAsync<T>(PropertyCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = propertyUrl + "/api/PropertyAPI",
                Token = token
            });            
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = propertyUrl + "/api/PropertyAPI/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            APIRequest request = new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = propertyUrl + "/api/PropertyAPI",
                Token = token
            };

            return SendAsync<T>(request);            
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = propertyUrl + "/api/PropertyAPI/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(PropertyUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = propertyUrl + "/api/PropertyAPI/" + dto.Id,
                Token = token
            });
        }
    }
}

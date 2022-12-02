using AutoMapper;
using HotProperty_PropertyAPI.Data;
using HotProperty_PropertyAPI.Logging;
using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Models.Dto;
using HotProperty_PropertyAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Runtime.CompilerServices;

namespace HotProperty_PropertyAPI.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/PropertyNumberAPI")]    
    [ApiVersion("2.0")]
    //[Route("api/[controller]") this will use the controller name in the route
    public class PropertyNumberAPIController : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IPropertyNumberRepository _dbPropertyNumber;
        private readonly IPropertyRepository _dbProperty;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public PropertyNumberAPIController(ILogging logger, IMapper mapper, IPropertyNumberRepository dbPropertyNumber, IPropertyRepository dbProperty)
        {
            _logger = logger;
            _mapper = mapper;
            _dbPropertyNumber = dbPropertyNumber;
            _dbProperty = dbProperty;
            _response = new();
        }

        [MapToApiVersion("2.0")]
        [HttpGet("GetStrong")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Strong1", "Strong2" };
        }
        
    }
}

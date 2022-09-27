using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProtoType.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected IConfiguration Config;
        protected IUnitOfWork UnitOfWork;

        public ApiController(IMapper mapper, IConfiguration config, IUnitOfWork uow)
        {
            Mapper = mapper;
            Config = config;
            UnitOfWork = uow;
        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProtoType.Core.Dto;
using ProtoType.Core.Enums;
using ProtoType.Api.Models;

namespace ProtoType.Api.Controllers
{
    public class FixtureController : ApiController
    {
        public FixtureController(IMapper mapper, IMemoryCache cache, IConfiguration config, IUnitOfWork unitOfWork) : base(mapper, config, unitOfWork)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FixtureDto))]
        public async Task<IActionResult> GetFixtureById(int id)
        {
            try
            {
                var fixture = await UnitOfWork.FixtureService.Get(id);
                return Ok(fixture);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(new ErrorResponse
                {
                    Code = ErrorCode.UNKNOWN
                });
            }
        }
    }
}

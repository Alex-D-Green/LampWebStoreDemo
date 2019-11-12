using System;
using System.Threading.Tasks;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Interfaces;

using Microsoft.AspNetCore.Mvc;


namespace LampStore.Infrastructure.WebApi.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LampController: ControllerBase
    {
        private readonly IUnitOfWork db;


        //Actually it's not the best idea to use UoW directly it should be wrapped by a service
        public LampController(IUnitOfWork db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await db.Lamps.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Lamp ret = await db.Lamps.GetByIdAsync(id);

            if(ret == null)
                return NotFound(id);

            return Ok(ret);
        }
    }
}

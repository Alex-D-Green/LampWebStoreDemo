using System;
using System.Linq;
using System.Threading.Tasks;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Interfaces;
using LampStore.AppCore.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace LampStore.Infrastructure.WebApi.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComparisonController: ControllerBase
    {
        private readonly ILampsComparisonService service;
        private readonly ILogger<ComparisonController> logger;


        public ComparisonController(ILampsComparisonService service, ILogger<ComparisonController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await service.GetAllComparisonsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int fstLampId, int sndLampId)
        {
            //It should be done through a service but I had no enough time...
            var uow = (IUnitOfWork)HttpContext.RequestServices.GetService(typeof(IUnitOfWork));

            Lamp fst = await uow.Lamps.GetByIdAsync(fstLampId);
            if(fst is null)
                return NotFound(fstLampId);

            Lamp snd = await uow.Lamps.GetByIdAsync(sndLampId);
            if(snd is null)
                return NotFound(sndLampId);


            Comparison comp = service.DoComparison(fst, snd);
            await service.SaveComparsionAsync(comp);

            return CreatedAtAction(nameof(Post), comp);
        }
    }
}

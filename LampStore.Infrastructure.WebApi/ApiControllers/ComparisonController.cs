using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Exceptions;
using LampStore.AppCore.Core.Interfaces;
using LampStore.AppCore.Services.Interfaces;
using LampStore.Infrastructure.WebApi.ApiModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace LampStore.Infrastructure.WebApi.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComparisonController: ControllerBase
    {
        private const int pageSize = 3;


        private readonly ILampsComparisonService service;
        private readonly ILogger<ComparisonController> logger;


        public ComparisonController(ILampsComparisonService service, ILogger<ComparisonController> logger)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        public async Task<IEnumerable<Comparison>> Get(int? page = null, string sortingBy = null, bool desc = false)
        {
            int? from = null;
            int? count = null;

            if(page.HasValue)
            {
                from = page * pageSize;
                count = pageSize;
            }

            return await service.GetAllComparisonsAsync(from, count, sortingBy, 
                desc ? SortDirection.Descending : SortDirection.Ascending);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Comparison>> Get(int id)
        {
            Comparison ret = await service.GetComparisonByIdAsync(id) ;

            if(ret == null)
                return NotFound();

            return ret;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Lamp>> Post([FromBody] PairOfLampsApi pairOfLamps)
        {
            //TODO: Check pairOfLamps...

            Lamp fst = await service.Lamps.GetByIdAsync(pairOfLamps.FstLampId);
            if(fst is null)
                return NotFound(pairOfLamps.FstLampId);

            Lamp snd = await service.Lamps.GetByIdAsync(pairOfLamps.SndLampId);
            if(snd is null)
                return NotFound(pairOfLamps.SndLampId);


            Comparison comp = service.DoComparison(fst, snd);
            await service.SaveComparsionAsync(comp);

            return CreatedAtAction(nameof(Get), new { id = comp.Id }, comp);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await service.DeleteComparsionAsync(id);
                return NoContent();
            }
            catch(ItemNotFoundStorageException)
            { return NotFound(); }
        }
    }
}

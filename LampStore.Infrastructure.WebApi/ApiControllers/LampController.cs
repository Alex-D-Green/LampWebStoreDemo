using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using LampStore.AppCore.Core.Entities;
using LampStore.AppCore.Core.Interfaces;
using LampStore.AppCore.Core.Utilities;
using LampStore.Infrastructure.WebApi.ApiModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LampStore.Infrastructure.WebApi.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LampController: ControllerBase
    {
        private const int pageSize = 3;


        private readonly IUnitOfWork db;
        private readonly IMapper mapper;


        public LampController(IUnitOfWork db, IMapper mapper)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// Fetch the portion of the lamps items.
        /// </summary>
        /// <param name="page">Page number (<c>null</c> - to fetch all items).</param>
        /// <param name="sortingBy">Property name for sorting.</param>
        /// <param name="desc"><c>true</c> to sort in descending order.</param>
        /// <returns></returns>
        /// <response code="200">The portion of the lamps items.</response>
        [HttpGet]
        public async Task<IEnumerable<Lamp>> Get(int? page = null, string sortingBy = null, bool desc = false)
        {
            int? from = null;
            int? count = null;

            if(page.HasValue)
            {
                from = page * pageSize;
                count = pageSize;
            }

            return await db.Lamps.GetAsync(from, count, sortingBy, desc ? SortDirection.Descending : SortDirection.Ascending);
        }

        /// <summary>
        /// Fetch the lamp item with the given id.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns></returns>
        /// <response code="200">The lamp item with the given id.</response>
        /// <response code="404">If item with the given id was not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Lamp>> Get(int id)
        {
            Lamp ret = await db.Lamps.GetByIdAsync(id);

            if(ret == null)
                return NotFound();

            return ret;
        }

        /// <summary>
        /// Create a new lamp item.
        /// </summary>
        /// <param name="lamp">New item data.</param>
        /// <returns></returns>
        /// <response code="201">The newly created lamp item.</response>
        /// <response code="400">If item contains incorrect data.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Lamp>> Post([FromBody] LampCreationApi lamp)
        {
            var newItem = mapper.Map<Lamp>(lamp);
            IdHolder<int> idHolder = await db.Lamps.AddAsync(newItem);
            
            await db.SaveAsync();
            newItem.SetId(idHolder.Id);

            return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
        }

        /// <summary>
        /// Update the lamp item data.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <param name="lamp">Updated item data.</param>
        /// <response code="204">If data was accepted.</response>
        /// <response code="404">If item with the given id was not found.</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(int id, [FromBody] LampCreationApi lamp)
        {
            Lamp ret = await db.Lamps.GetByIdAsync(id);

            if(ret == null)
                return NotFound();

            Lamp item = mapper.Map<Lamp>(lamp);
            item.SetId(id);
            db.Lamps.Update(item);

            await db.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete item with the given id.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns></returns>
        /// <response code="204">If the item was deleted.</response>
        /// <response code="404">If item with the given id was not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            Lamp item = await db.Lamps.GetByIdAsync(id);

            if(item == null)
                return NotFound();

            db.Lamps.Delete(item);
            await db.SaveAsync();

            return NoContent();
        }
    }
}

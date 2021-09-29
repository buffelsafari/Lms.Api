using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lms.Core.Entities;
using Lms.Data.Data;
using Lms.Core.Repositories;
using AutoMapper;
using Lms.Core.Dto;

namespace Lms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        
        private readonly IUoW uow;
        private readonly IMapper mapper;

        public ModulesController(IUoW uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
            
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModule()
        {
            return Ok(mapper.Map<IEnumerable<ModuleDto>>(await uow.ModuleRepository.GetAllModules()));            
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id)
        {
            var module = await uow.ModuleRepository.FindAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            return mapper.Map<ModuleDto>(module);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, Module module)
        {
            if (id != module.Id)
            {
                return BadRequest();
            }

            uow.ModuleRepository.Update(module);
            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return NoContent();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(Module module)
        {
            uow.ModuleRepository.Add(module);
            await uow.CompleteAsync();

            return CreatedAtAction("GetModule", new { id = module.Id }, mapper.Map<ModuleDto>(module));
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var module = await uow.ModuleRepository.FindAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            uow.ModuleRepository.Remove(module);
            await uow.CompleteAsync();

            return NoContent();
        }

        private async Task<bool> ModuleExists(int id)
        {
            return await uow.ModuleRepository.AnyAsync(id);
        }
    }
}

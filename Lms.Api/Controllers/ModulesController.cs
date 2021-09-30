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
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpPatch("{moduleId}")]
        public async Task<ActionResult<ModuleDto>> PatchModule(int moduleId, JsonPatchDocument<ModuleDto> patchDocument)
        {
            var module = await uow.ModuleRepository.FindAsync(moduleId);

            if (module == null)
            {
                return NotFound();
            }

            var moduleDto = mapper.Map<ModuleDto>(module);

            patchDocument.ApplyTo(moduleDto ,ModelState);

            if (!TryValidateModel(moduleDto))
            {
                return BadRequest();
            }

            mapper.Map(moduleDto, module);


            try
            {
                await uow.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ModuleExists(moduleId))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }




            return Ok();


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

            return Ok(mapper.Map<ModuleDto>(module));
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleDto moduleDto)
        {
            
            if (!TryValidateModel(moduleDto))
            {
                return BadRequest();
            }

            Module module = mapper.Map<Module>(moduleDto);
            module.Id = id;

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
                    return StatusCode(500);                    
                }
            }
            
            return Ok();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(ModuleDto moduleDto)
        {
            if (!TryValidateModel(moduleDto))
            {
                return BadRequest();
            }

            Module module = mapper.Map<Module>(moduleDto);
            
            uow.ModuleRepository.Add(module);
            await uow.CompleteAsync();

            return Ok(CreatedAtAction("GetModule", new { id = module.Id }, mapper.Map<ModuleDto>(module)));
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

            return Ok();
        }

        private async Task<bool> ModuleExists(int id)
        {
            return await uow.ModuleRepository.AnyAsync(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CommandAPI.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        // PROPERTIES
        private readonly ICommandAPIRepository repository;
        private readonly IMapper mapper;

        // CONSTRUCTOR
        public CommandsController(ICommandAPIRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            IEnumerable<Command> commands = repository.GetAllCommands();
            return Ok(mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }

        [HttpGet("{id}", Name ="GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            Command command = repository.GetCommandById(id);
            if(command == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CommandReadDTO>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            Command command = mapper.Map<Command>(commandCreateDTO);
            repository.CreateCommand(command);
            repository.SaveChanges();

            CommandReadDTO createdCommand = mapper.Map<CommandReadDTO>(command);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = createdCommand.Id }, createdCommand);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {
            Command foundCommand = repository.GetCommandById(id);
            if(foundCommand == null)
            {
                return NotFound();
            }
            mapper.Map(commandUpdateDTO, foundCommand);
            repository.UpdateCommand(foundCommand);
            repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDTO> patchDocument)
        {
            Command foundCommand = repository.GetCommandById(id);
            if(foundCommand == null)
            {
                return NotFound();
            }

            CommandUpdateDTO commandToPatch = mapper.Map<CommandUpdateDTO>(foundCommand);
            patchDocument.ApplyTo(commandToPatch, ModelState);

            if (TryValidateModel(patchDocument))
            {
                return ValidationProblem(ModelState);
            }
            mapper.Map(commandToPatch, foundCommand);
            repository.UpdateCommand(foundCommand);
            repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            Command foundCommand = repository.GetCommandById(id);
            if (foundCommand == null)
            {
                return NotFound();
            }

            repository.DeleteCommand(foundCommand);
            repository.SaveChanges();
            return NoContent();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CommandAPI.Controllers;
using Moq;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Profiles;
using AutoMapper;
using CommandAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CommandAPI.Tests
{
    public class CommandControllerTests : IDisposable
    {
        // PROPRIETES
        private Mock<ICommandAPIRepository> mockRepository;
        private CommandsProfile realProfile;
        private MapperConfiguration configuration;
        private IMapper mapper;

        // CONSTRUCTOR
        public CommandControllerTests()
        {
            mockRepository = new Mock<ICommandAPIRepository>();
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }

        // METHODS
        public void Dispose()
        {
            mockRepository = null;
            realProfile = null;
            configuration = null;
            mapper = null;
        }

        [Fact]
        public void GetCommandItems_ReturnsZeroItem_WhenDbIsEmpty()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(0));
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandItems_ReturnsOneItem_WhenDbHasOneRessource()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDTO>;
            Assert.Single(commands);
        }

        [Fact]
        public void GetCommandItems_Returns200Ok_WhenDbHasOneRessource()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandItems_ReturnsCorrectType_WhenDbHasOneRessource()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetAllCommands()).Returns(GetCommands(1));
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDTO>>>(result);
        }

        [Fact]
        public void GetCommandById_Returns404NotFound_WhenNonExistantIdIsProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetCommandById_Returns200Ok_WhenValidIdIsProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCommandById_ReturnsCorrectType_WhenValidIdIsProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);

            // Assert
            Assert.IsType<ActionResult<CommandReadDTO>>(result);
        }

        [Fact]
        public void CreateCommand_ReturnsCorrectType_WhenValidObjectProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.CreateCommand(new CommandCreateDTO { });

            // Assert
            Assert.IsType<ActionResult<CommandReadDTO>>(result);
        }

        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.CreateCommand(new CommandCreateDTO { });

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }

        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.UpdateCommand(1, new CommandUpdateDTO());

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenInvalidObjectProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.UpdateCommand(0, new CommandUpdateDTO());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void PartialUpdateCommand_Returns404NotFound_WhenInvalidObjectProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.PartialUpdateCommand(0, new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDTO>());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidIdProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "Mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.DeleteCommand(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteCommand_Returns404NotFound_WhenInvalidIdProvided()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetCommandById(0)).Returns(() => null);
            var controller = new CommandsController(mockRepository.Object, mapper);

            // Act
            var result = controller.DeleteCommand(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private List<Command> GetCommands(int num)
        {
            List<Command> commands = new List<Command>();
            if(num > 0)
            {
                commands.Add(new Command 
                {
                    Id = 0,
                    HowTo = "List files and folder",
                    Platform = "Windows",
                    CommandLine = "dir"
                });
            }
            return commands;
        }

    }
}

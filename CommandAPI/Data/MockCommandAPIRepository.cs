using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Data
{
    public class MockCommandAPIRepository : ICommandAPIRepository
    {
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            List<Command> commands = new List<Command>
            {
                new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "add-migration <name of migration>",
                    Platform = ".NET Core EF"
                },
                new Command
                {
                    Id = 1,
                    HowTo = "How to apply a migration in database",
                    CommandLine = "update-database",
                    Platform = ".NET Core EF"
                },
                                new Command
                {
                    Id = 2,
                    HowTo = "How to list files and folder",
                    CommandLine = "ls",
                    Platform = "Linux bash"
                }
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command
            {
                Id = 0,
                HowTo = "How to generate a migration",
                CommandLine = "add-migration <name of migration>",
                Platform = ".NET Core EF"
            };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandAPI.Data
{
    public class SqlCommandAPIRepository : ICommandAPIRepository
    {
        // PROPERTIES
        private readonly CommandContext context;

        // CONSTRUCTOR
        public SqlCommandAPIRepository(CommandContext context)
        {
            this.context = context;
        }

        // METHODS
        public void CreateCommand(Command command)
        {
            if(command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            context.Commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            context.Commands.Remove(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return context.Commands.FirstOrDefault(c => c.Id == id);
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 1);
        }

        public void UpdateCommand(Command command)
        {
            // Implementation not necessary
        }
    }
}

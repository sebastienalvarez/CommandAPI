using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using CommandAPI.Models;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        private Command testCommand;

        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Test xUnit",
                Platform = "xUnit",
                CommandLine = "First xUnit test method"
            };
        }

        [Fact]
        public void CanChangeHowTo()
        {
            // Arrange

            // Act
            testCommand.HowTo = "Text change";

            // Assert
            Assert.Equal("Text change", testCommand.HowTo);
        }

        public void Dispose()
        {
            testCommand = null;
        }
    }
}

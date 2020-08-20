using DesafioSouthSystem.Domain.Commands;
using DesafioSouthSystem.Domain.Handlers.Commands;
using System;
using System.IO;
using System.Threading;
using Xunit;

namespace DesafioSouthSystem.Tests.Commands
{
    public class FileReadingCommandTests: BaseTests
    {
        public FileReadingCommandTests()
        {
        }
        [Fact]
        public void ErrorWhenFileIsEmpty()
        {
            var fileReadingCommand = GetFileReadingCommand("TestFileIsEmpty.txt");
            var commandResult = new FileReadingCommandHandler(_mockAppSettings.Object).Handle(fileReadingCommand, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(1001));
        }
        [Fact]
        public void ErrorWhenLineHasNoSeparator()
        {
            var fileReadingCommand = GetFileReadingCommand("TestLineHasNoSeparator.txt");
            var commandResult = new FileReadingCommandHandler(_mockAppSettings.Object).Handle(fileReadingCommand, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(1015));
        }
        [Theory]
        [InlineData(1004)]
        [InlineData(1005)]
        [InlineData(1006)]
        public void ErrorWhenSalesmanInvalid(int errorCode)
        {
            var fileReadingCommand = GetFileReadingCommand("TestSalesmanInvalid.txt");
            var commandResult = new FileReadingCommandHandler(_mockAppSettings.Object).Handle(fileReadingCommand, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(errorCode));
        }
        [Theory]
        [InlineData(1007)]
        [InlineData(1008)]
        [InlineData(1009)]
        public void ErrorWhenCustomerInvalid(int errorCode)
        {
            var fileReadingCommand = GetFileReadingCommand("TestCustomerInvalid.txt");
            var commandResult = new FileReadingCommandHandler(_mockAppSettings.Object).Handle(fileReadingCommand, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(errorCode));
        }
        [Theory]
        [InlineData(1010)]
        [InlineData(1011)]
        public void ErrorWhenSaleInvalid(int errorCode)
        {
            var fileReadingCommand = GetFileReadingCommand("TestSaleInvalid.txt");
            var commandResult = new FileReadingCommandHandler(_mockAppSettings.Object).Handle(fileReadingCommand, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(errorCode));
        }
        [Theory]
        [InlineData(1012)]
        [InlineData(1013)]
        [InlineData(1014)]
        public void ErrorWhenSaleItemInvalid(int errorCode)
        {
            var fileReadingCommand = GetFileReadingCommand("TestSaleItemInvalid.txt");
            var commandResult = new FileReadingCommandHandler(_mockAppSettings.Object).Handle(fileReadingCommand, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(errorCode));
        }

        #region Private Functions
        private FileReadingCommand GetFileReadingCommand(string fileName)
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string inputPath = string.Format("{0}\\Files\\in\\{1}", path, fileName);
            string outputPath = string.Format("{0}\\Files\\out\\{1}", path, fileName);

            var fileReadingCommand = new FileReadingCommand(fileName, inputPath, outputPath);
            return fileReadingCommand;
        } 
        #endregion

    }
}

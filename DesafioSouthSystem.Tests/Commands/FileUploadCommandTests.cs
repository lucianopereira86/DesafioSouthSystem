using Xunit;
using DesafioSouthSystem.Domain.Commands;
using System.Threading;
using DesafioSouthSystem.Domain.Handlers.Commands;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using MediatR;
using Moq;
using DesafioSouthSystem.WebAPI.Controllers;
using DesafioSouthSystem.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DesafioSouthSystem.Shared.CommandQuery;

namespace DesafioSouthSystem.Tests.Commands
{
    public class FileUploadCommandTests : BaseTests
    {
        private Mock<IMediator> _mockMediator;
        public FileUploadCommandTests()
        {
            _mockMediator = new Mock<IMediator>();
        }
        [Fact]
        public void SuccessWhenOkResult()
        {
            _mockMediator.Setup(s => s.Send(It.IsAny<FileUploadCommand>(), new CancellationToken())).
                ReturnsAsync(new CommandQueryResult(_mockAppSettings.Object));
            var fileController = new FileController(_mockMediator.Object, GetMapper(), _mockAppSettings.Object);
            
            var result = fileController.Upload(new FileUploadCommandVM()).Result;
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void ErrorWhenBadRequestObjectResult()
        {
            _mockMediator.Setup(s => s.Send(It.IsAny<FileUploadCommand>(), new CancellationToken())).
                ReturnsAsync(() =>
                {
                    var res = new CommandQueryResult(_mockAppSettings.Object);
                    res.AddError(1001);
                    return res;
                });
            var fileController = new FileController(_mockMediator.Object, GetMapper(), _mockAppSettings.Object);
            
            var result = fileController.Upload(new FileUploadCommandVM()).Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void ErrorWhenFileIsEmpty()
        {
            var file = GetFormFile("TestFileIsEmpty.txt", false);
            var request = new FileUploadCommand(file);
            var commandResult = new FileUploadCommandHandler(_mockAppSettings.Object).Handle(request, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(1001));
        }


        #region Private Functions
        private FormFile GetFormFile(string fileName, bool isSuccess)
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string folder = isSuccess ? "Files" : "Files\\in";
            string inputPath = string.Format("{0}\\{1}\\{2}", path, folder, fileName);
            var stream = File.OpenRead(inputPath);
            return new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/txt"
            };
        }
        #endregion
    }
}

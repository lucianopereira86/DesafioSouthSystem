using Xunit;
using System.Threading;
using MediatR;
using Moq;
using DesafioSouthSystem.WebAPI.Controllers;
using DesafioSouthSystem.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DesafioSouthSystem.Domain.Queries;
using DesafioSouthSystem.Domain.Handlers.Queries;
using DesafioSouthSystem.Shared.CommandQuery;

namespace DesafioSouthSystem.Tests.Queries
{
    public class FileDownloadQueryTests : BaseTests
    {
        private Mock<IMediator> _mockMediator;
        public FileDownloadQueryTests()
        {
            _mockMediator = new Mock<IMediator>();
        }
        [Fact]
        public void SuccessWhenFileContentResult()
        {
            _mockMediator.Setup(s => s.Send(It.IsAny<FileDownloadQuery>(), new CancellationToken())).
                ReturnsAsync(() =>
                {
                    var res = new CommandQueryResult(_mockAppSettings.Object);
                    var bytes = new byte[1];
                    res.SetData(bytes);
                    return res;
                });
            var fileController = new FileController(_mockMediator.Object, GetMapper(), _mockAppSettings.Object);

            var result = fileController.Download(new FileDownloadQueryVM()).Result;
            Assert.IsType<FileContentResult>(result);
        }

        [Fact]
        public void ErrorWhenBadRequestObjectResult()
        {
            _mockMediator.Setup(s => s.Send(It.IsAny<FileDownloadQuery>(), new CancellationToken())).
                ReturnsAsync(() =>
                {
                    var res = new CommandQueryResult(_mockAppSettings.Object);
                    res.AddError(1002);
                    return res;
                });
            var fileController = new FileController(_mockMediator.Object, GetMapper(), _mockAppSettings.Object);

            var result = fileController.Download(new FileDownloadQueryVM()).Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void ErrorWhenFileNameNotInformed()
        {
            var fileDownloadCommand = new FileDownloadQuery(null);
            var commandResult = new FileDownloadQueryHandler(_mockAppSettings.Object).Handle(fileDownloadCommand, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(1002));
        }
        [Fact]
        public void ErrorWhenFileNotFound()
        {
            var fileDownloadCommand = new FileDownloadQuery("xxx.txt");
            var commandResult = new FileDownloadQueryHandler(_mockAppSettings.Object).Handle(fileDownloadCommand, new CancellationToken()).Result;

            Assert.True(commandResult.HasError(1003));
        }
    }
}

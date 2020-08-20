using AutoMapper;
using DesafioSouthSystem.Domain.Commands;
using DesafioSouthSystem.Domain.Queries;
using DesafioSouthSystem.Shared.Models;
using DesafioSouthSystem.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DesafioSouthSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private string _inputPath;
        private string _outputPath;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public FileController(IMediator mediator, IMapper mapper, IOptions<AppSettings> optionsAppSettings)
        {
            _mediator = mediator;
            _mapper = mapper;

            LoadPaths(optionsAppSettings);
        }

        #region Private Functions
        private void LoadPaths(IOptions<AppSettings> optionsAppSettings)
        {
            var homeDrive = optionsAppSettings.Value.IsHomePathComplete? "" : Environment.GetEnvironmentVariable("HOMEDRIVE");
            var homePath = Environment.GetEnvironmentVariable("HOMEPATH");

            _inputPath = string.Format("{0}{1}\\data\\in\\", homeDrive, homePath);
            _outputPath = string.Format("{0}{1}\\data\\out\\", homeDrive, homePath);
        }
        #endregion

        /// <summary>
        /// Upload a text file (.txt)
        /// </summary>
        /// <param name="commandVM" cref="FileUploadCommandVM"></param>
        /// <returns></returns>
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadCommandVM commandVM)
        {
            try
            {
                var command = _mapper.Map<FileUploadCommand>(commandVM);
                command.SetInputPath(_inputPath);

                var commandResult = await _mediator.Send(command);
                if (!commandResult.Valid)
                    return BadRequest(commandResult.Errors);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Download file by its name
        /// </summary>
        /// <param name="commandVM" cref="FileDownloadQueryVM"></param>
        /// <returns></returns>
        [HttpGet("Download")]
        [Produces("application/txt")]
        public async Task<IActionResult> Download([FromQuery] FileDownloadQueryVM commandVM)
        {
            try
            {
                var command = _mapper.Map<FileDownloadQuery>(commandVM);
                command.SetOutputPath(_outputPath);

                var commandResult = await _mediator.Send(command);
                if (!commandResult.Valid)
                    return BadRequest(commandResult.Errors);
                byte[] fileContent = (byte[]) commandResult.Data;
                return File(fileContent, "application/txt", commandVM.FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

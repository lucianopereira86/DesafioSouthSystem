using AutoMapper;
using DesafioSouthSystem.Shared.Models;
using DesafioSouthSystem.WebAPI.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace DesafioSouthSystem.Tests
{
    public class BaseTests
    {
        protected readonly Mock<IOptions<AppSettings>> _mockAppSettings;
        public BaseTests()
        {
            var appSettings = new AppSettings();
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .Build()
                .Bind(appSettings);

            var optionsAppSettings = Options.Create(appSettings);
            _mockAppSettings = new Mock<IOptions<AppSettings>>();
            _mockAppSettings.Setup(s => s.Value).Returns(optionsAppSettings.Value);
        }

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
            return config.CreateMapper();
        }
    }
}

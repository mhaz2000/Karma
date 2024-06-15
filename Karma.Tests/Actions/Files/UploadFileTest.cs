using Azure;
using FakeItEasy;
using FluentAssertions;
using Karma.API.Controllers;
using Karma.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Karma.Tests.Actions.Files
{
    public class UploadFileTest
    {
        private readonly FilesController _filesController;
        private readonly IFileService _fileService;

        public UploadFileTest()
        {
            _fileService = A.Fake<IFileService>();

            _filesController = new FilesController(_fileService);

            _filesController.ControllerContext = Fixture.FakeControllerContext();
        }

        [Fact]
        public async Task Must_Store_File()
        {
            //Arrange
            var expectedId = Guid.NewGuid();

            var content = "Fake Image";
            var fileName = "FakeImange.png";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);


            A.CallTo(() => _fileService.StoreFileAsync(A<MemoryStream>._, A<Guid>._, A<string>._)).Returns(expectedId);

            //Act
            var response = await _filesController.Upload(file);

            var result = (OkObjectResult)response;

            result.StatusCode.Should().Be(200);

            result.Value!.GetType().GetProperty("message", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)!
               .GetValue(result.Value, null)
               .Should().Be("فایل با موفقیت باگذاری شد.");


            result.Value!.GetType().GetProperty("value", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)!
                .GetValue(result.Value, null)
                .Should().Be(expectedId);
        }
    }
}

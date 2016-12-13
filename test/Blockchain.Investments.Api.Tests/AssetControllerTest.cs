using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft​.Extensions​.Options;
using Blockchain.Investments.Core.Model;
using Blockchain.Investments.Core.Repositories;
using FakeItEasy;
using Xunit;

namespace Blockchain.Investments.Api.Controllers
{
    public class AssetsControllerTest
    {
        [Fact]
        public void Create_ReturnsNewlyCreatedAsset()
        {
            // Arrange
            string testId = "testID";
            string testName = "testName";
            double testValue = 1000;
            DateTime testDate = DateTime.Now;
            int testPercentage = 10;
            string testImageSource = "";

            var mockLogger = A.Fake<ILogger<AssetsController>>();
            var mockRepo = A.Fake<IRepository<Asset>>();
            var mockOptionsAccessor = A.Fake<IOptions<AppConfig>>();
            var controller = new AssetsController(mockLogger, mockRepo, mockOptionsAccessor);

            var newAsset = new Asset()
            {
                UniqueId = testId,
                Name = testName,
                Value = testValue,
                Date = testDate,
                Percentage= testPercentage,
                ImageSource = testImageSource
            };
            
            // Act
            var result = controller.Post(newAsset);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnAsset = Assert.IsType<Asset>(okResult.Value);
            Assert.Equal(testId, returnAsset.UniqueId);
            Assert.Equal(testName, returnAsset.Name);
        }
    }
}
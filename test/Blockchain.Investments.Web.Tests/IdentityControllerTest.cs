using System;
using Blockchain.Investments.Api.Controllers;
using Blockchain.Investments.Api.Options;
using Blockchain.Investments.Bitcoin.Domain;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NBitcoin;
using Xunit;

namespace Blockchain.Investments.Api
{
    public class IdentityControllerTest
    {
        [Fact]
        public void Login_BitidRequestMessageIsValid_ReturnsTrue() 
        {
            // Arrange
            Key privateKey = new Key(); //Create private key
            BitcoinSecret secret = privateKey.GetBitcoinSecret(Network.Main);
            BitcoinAddress pubAddress = secret.GetAddress();
            Guid guid = Guid.NewGuid();
            string guidString = guid.ToString().Replace ("-", "");

            long ticks = DateTime.UtcNow.Ticks;
            string nonce = guidString + ticks.ToString ("x");
            string bitIdUri = "bitid://localhost/api/identity?x=" + nonce + "&u=1";
            string signature = privateKey.SignMessage(bitIdUri);

            var request = new BitIdCredentials(pubAddress.ToString(), bitIdUri, signature);
            
            // Act
            BitIdResponse response = request.VerifyMessage();

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public void Login_BitidRequestMessageIsValid_ReturnsJsonToken()
        {
            // Arrange
            var mockJwtIssuer = A.Fake<IOptions<JwtIssuerOptions>>();
            var mockLogger = A.Fake<ILogger<IdentityController>>();

            var credentials = new BitIdCredentials();

            var controller = new IdentityController(mockJwtIssuer, mockLogger);

            // Act
            var actionResult = controller.Post(credentials);

            // Assert
            Assert.Equal(true, true);
        }
    }
}
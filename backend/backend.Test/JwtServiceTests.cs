using Microsoft.VisualStudio.TestTools.UnitTesting;
using backend.Application.Services;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace backend.Tests
{
    [TestClass]
    public class JwtServiceTests
    {
        private JwtService _jwtService;

        [TestInitialize]
        public void Setup()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"JwtSettings:SecretKey", "supersecretkey1234567890longenough"},
                {"JwtSettings:Issuer", "TruckoomAPI"},
                {"JwtSettings:Audience", "TruckoomClients"},
                {"JwtSettings:ExpiryMinutes", "60"}
            };

            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _jwtService = new JwtService(config);
        }

        [TestMethod]
        public void GenerateToken_ShouldReturnTokenString()
        {
            // Arrange
            string username = "admin";

            // Act
            var token = _jwtService.GenerateToken(username);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(token));
            Assert.IsTrue(token.Contains("."));
        }
    }
}

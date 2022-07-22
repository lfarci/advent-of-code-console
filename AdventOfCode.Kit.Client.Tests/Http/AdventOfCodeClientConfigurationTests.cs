using AdventOfCode.Kit.Client.Http;
using System;
using Xunit;

namespace AdventOfCode.Kit.Client.Tests.Http
{
    public class AdventOfCodeClientConfigurationTests
    {

        [Theory]
        [InlineData("ValidHost", "")]
        [InlineData("ValidHost", null)]
        [InlineData("", "ValidSessionId")]
        [InlineData(null, "ValidSessionId")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        public void Constructor_EmptyOrNullArgument_ThrowsArgumentException(string? host, string? sessionId)
        {
            Assert.Throws<ArgumentException>(() => new AdventOfCodeClientConfiguration(host, sessionId));
        }

        [Fact]
        public void RequireNotNullOrEmpty_Null_ThrowsArgumentException()
        { 
            Assert.Throws<ArgumentException>(() => AdventOfCodeClientConfiguration.RequireNotNullOrEmpty(null));
        }

        [Fact]
        public void RequireNotNullOrEmpty_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => AdventOfCodeClientConfiguration.RequireNotNullOrEmpty(""));
        }

        [Fact]
        public void RequireNotNullOrEmpty_ValidString_ReturnsString()
        {
            string text = "Some text";
            Assert.Equal(text, AdventOfCodeClientConfiguration.RequireNotNullOrEmpty(text));
        }
    }
}

using AdventOfCode.Kit.Client.Http;
using System;
using Xunit;

namespace AdventOfCode.Kit.Client.Tests.Http
{
    public class AdventOfCodeHttpClientConfigurationTests
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
            Assert.Throws<ArgumentException>(() => new AdventOfCodeHttpClientConfiguration(host, sessionId));
        }

        [Fact]
        public void RequireNotNullOrEmpty_Null_ThrowsArgumentException()
        { 
            Assert.Throws<ArgumentException>(() => AdventOfCodeHttpClientConfiguration.RequireNotNullOrEmpty(null));
        }

        [Fact]
        public void RequireNotNullOrEmpty_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => AdventOfCodeHttpClientConfiguration.RequireNotNullOrEmpty(""));
        }

        [Fact]
        public void RequireNotNullOrEmpty_ValidString_ReturnsString()
        {
            string text = "Some text";
            Assert.Equal(text, AdventOfCodeHttpClientConfiguration.RequireNotNullOrEmpty(text));
        }
    }
}

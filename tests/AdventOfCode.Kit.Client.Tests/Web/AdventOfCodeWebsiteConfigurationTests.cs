using AdventOfCode.Kit.Client.Web;
using System;
using Xunit;

namespace AdventOfCode.Kit.Client.Tests.Web.Http
{
    public class AdventOfCodeWebsiteConfigurationTests
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
            Assert.Throws<ArgumentException>(() => new AdventOfCodeWebsiteConfiguration(host, sessionId));
        }

        [Fact]
        public void RequireNotNullOrEmpty_Null_ThrowsArgumentException()
        { 
            Assert.Throws<ArgumentException>(() => AdventOfCodeWebsiteConfiguration.RequireNotNullOrEmpty(null));
        }

        [Fact]
        public void RequireNotNullOrEmpty_EmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => AdventOfCodeWebsiteConfiguration.RequireNotNullOrEmpty(""));
        }

        [Fact]
        public void RequireNotNullOrEmpty_ValidString_ReturnsString()
        {
            string text = "Some text";
            Assert.Equal(text, AdventOfCodeWebsiteConfiguration.RequireNotNullOrEmpty(text));
        }
    }
}

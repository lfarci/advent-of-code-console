using AdventOfCode.Kit.Console.Web.Client;
using AdventOfCode.Kit.Console.Web.Resources;
using Moq;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace AdventOfCodeConsole.Tests.Helpers
{
    internal static class Helpers
    {
        public static string ReadResourceContentAsString(string resourceName)
        {
            string calendarPage = "";
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                calendarPage = new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            }
            return calendarPage;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static IAdventOfCodeClient GetClientThatThrows<TException>(Expression<Action<IAdventOfCodeClient>> call) where TException : Exception, new()
        {
            var client = new Mock<IAdventOfCodeClient>();
            client.Setup(call).Throws<TException>();
            return client.Object;
        }

        public static IAdventOfCodeClient GetClientThatReturns(string result, Expression<Func<IAdventOfCodeClient, Stream>> call)
        {
            var client = new Mock<IAdventOfCodeClient>();
            client.Setup(call).Returns(GenerateStreamFromString(result));
            return client.Object;
        }

        public static ICalendarPageRepository GetCalendarPageRepositoryThatThrows<TException>() where TException : Exception, new()
        {
            var client = GetClientThatThrows<TException>(c => c.GetCalendarPageAsStreamAsync(It.IsAny<int>()));
            return new CalendarPageRepository(client);
        }

        public static ICalendarPageRepository GetCalendarPageRepositoryThatReturns(string result)
        {
            var client = GetClientThatReturns(result, c => c.GetCalendarPageAsStreamAsync(It.IsAny<int>()).Result);
            return new CalendarPageRepository(client);
        }

    }
}

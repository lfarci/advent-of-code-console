using System.IO;
using System.Reflection;
using System.Text;

namespace Tests.Helpers
{
    public static class TestHelpers
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
    }
}

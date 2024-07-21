using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace s2ProtocolFurry.Protocol
{
    public static class ProtocolImporter
    {
        public static string[] ListAllProtocols(string basePath = null)
        {
            if (basePath == null)
            {
                basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            var pattern = new Regex(@"protocol\d+\.py$", RegexOptions.Compiled);

            var files = Directory.GetFiles(basePath)
                .Select(Path.GetFileName)
                .Where(file => pattern.IsMatch(file))
                .OrderBy(file => file) // Ensure files are sorted by name
                .ToArray();

            return files;
        }

        public static ProtocolInstance ImportLatestProtocol(string basePath = null)
        {
            if (basePath == null)
            {
                basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            var files = ListAllProtocols(basePath);

            if (files.Length == 0)
            {
                throw new InvalidOperationException("No protocol files found.");
            }

            var latestVersion = files.Last();

            var protocol = Path.GetFileNameWithoutExtension(latestVersion);
        }

        private static ProtocolInstance ImportProtocol(string basePath, string protocolName)
        {

        }
    }
}

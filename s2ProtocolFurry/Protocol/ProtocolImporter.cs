using System.Reflection;
using System.Text.RegularExpressions;

namespace s2ProtocolFurry.Protocol
{
    public class ProtocolImporter
    {
        private readonly string[] _protocols;
        private readonly Dictionary<string, List<ProtocolTypeInfo>> _typeInfosDictionary = new();

        public ProtocolImporter(string basePath)
        {
            _protocols = GetAllProtocolFiles(basePath);

            var protocolTypeInfoParser = new ProtocolTypeInfoParser();

            foreach(var protocol in _protocols)
            {
                var parsed = ProtocolTypeInfoParser2.Parse(File.ReadAllText(protocol));
                //var typeInfoContent = ExtractProtocolTypeinfos(protocol);
                //var parsed = protocolTypeInfoParser.ParseProtocolTypes(typeInfoContent);
                _typeInfosDictionary.Add(
                    protocol,
                    parsed
                );
            }
        }

        public List<ProtocolTypeInfo> GetTypeInfos(int? protocolNumber = null)
        {
            if (protocolNumber is null)
            {
                // latest protocol
                return _typeInfosDictionary.FirstOrDefault().Value;
            }

            var protocol = _protocols.FirstOrDefault(x => x.Contains(protocolNumber!.ToString()));
            _typeInfosDictionary.TryGetValue(protocol!, out var result);

            return result!;
        }

        private string[] GetAllProtocolFiles(string basePath = null)
        {
            if (basePath == null)
            {
                basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            var pattern = new Regex(@"protocol\d+\.py$", RegexOptions.Compiled);

            var files = Directory.GetFiles(basePath)
            .Where(file => pattern.IsMatch(Path.GetFileName(file)))
            .OrderByDescending(file => file)
            .ToArray();

            return files;
        }
        
        private List<string> ExtractProtocolTypeinfos(string filePath)
        {           
            var resultLines = new List<string>();
            var capturing = false;

            foreach(var line in File.ReadAllLines(filePath))
            {
                if (line.Contains("# Decoding instructions for each protocol type."))
                {
                    capturing = true;
                    continue;
                }

                if (capturing)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        break;
                    }

                    if (line.TrimStart().StartsWith("("))
                    {
                        resultLines.Add(line.Trim());
                    }
                }
            }

            return resultLines;
        }
    }
}

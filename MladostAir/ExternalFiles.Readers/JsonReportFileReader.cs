using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExternalFiles.Readers
{
    public class JsonReportFileReader
    {
        private const string JsonDirectory = "../../../../ExternalFiles/JsonReports/dataJson.json";

        public string ReadJsonReports()
        {
            //var filesPath = Directory.GetFiles(JsonDirectory);

            //var jsonTexts = filesPath.Select(path => File.ReadAllText(path));

            //return jsonTexts;

            StreamReader file = File.OpenText(JsonDirectory);
            JsonTextReader reader = new JsonTextReader(file);

            var json = (JObject)JToken.ReadFrom(reader);

            return json.ToString();
        }

       
    }
}

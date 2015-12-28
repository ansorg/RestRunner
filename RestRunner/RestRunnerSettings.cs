using System.Runtime.Serialization;
using Windows.Data.Json;

namespace RestRunner
{
    [DataContract]
    public class RestRunnerSettings
    {
        private const string settingsKey = "AppSettings";
        private const string methodKey = "Method";
        private const string urlKey = "Url";

        [DataMember]
        internal string Method;
        [DataMember]
        internal string Url;

        public RestRunnerSettings(string jsonString)
        {
            try
            {
                JsonObject jsonObject = JsonObject.Parse(jsonString);
                Method = jsonObject.GetNamedString(methodKey);
                Url = jsonObject.GetNamedString(urlKey);

            }
            catch (System.Exception)
            {
                Url = "";
                Method = "GET";
            }
        }

        public string toJson()
        {
            JsonObject jsonObject = new JsonObject();
            jsonObject[methodKey] = JsonValue.CreateStringValue(Method);
            jsonObject[urlKey] = JsonValue.CreateStringValue(Url);
            return jsonObject.Stringify();
        }
    }
}
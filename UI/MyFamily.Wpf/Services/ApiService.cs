using Newtonsoft.Json;
using System.Net;
using System.Net.Security;
using System.Text;

namespace Test_requests_mobile
{
    public static class ApiTool
    {
        private const string _address = "https://192.168.0.100:80/api/";

        static ApiTool()
        {
            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
               delegate { return true; }
            );
        }

        private static WebClient WebClient => new WebClient() { Encoding = Encoding.UTF8 };

        public static string Get(string method)
        {
            return WebClient.DownloadString(_address + method);
        }

        public static T? Get<T>(string method)
        {
            return JsonConvert.DeserializeObject<T>(WebClient.DownloadString(_address + method));
        }

        public static string Put<T>(string method, T editObject)
        {
            var client = WebClient;
            client.Headers.Add(HttpRequestHeader.ContentType, "application/Json");
            string data = JsonConvert.SerializeObject(editObject);
            return client.UploadString(_address + method, "PUT", data);
        }

        public static string Post<T>(string method, T editObject)
        {
            var client = WebClient;
            client.Headers.Add(HttpRequestHeader.ContentType, "application/Json");
            string data = JsonConvert.SerializeObject(editObject);
            return client.UploadString(_address + method, "POST", data);
        }

        public static string Delete(string method)
        {
            return WebClient.UploadString(_address + method, "DELETE", "");
        }
    }
}


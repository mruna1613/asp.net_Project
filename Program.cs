using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HRMS_APP.Folder
{
    internal class Program
    {
        internal class Json
        {
            static async Task Main(string[] args)
            {
                try
                {
                    var postData = new PostData
                    {
                        userid = "ram",
                        email = "rutuja.22110140@gmail.com",
                        password = "Rutuja123",
                        city = "Satara",
                        status = "Active"
                    };

                    string baseApiUrl = "https://api.example.com/"; // Replace with your actual API base URL
                    string endpoint = "api/posts"; // Replace with your actual API endpoint

                    using var client = new HttpClient();
                    client.BaseAddress = new Uri(baseApiUrl);

                    // Bypass certificate validation (use with caution)
                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                        (sender, certificate, chain, sslPolicyErrors) => true;

                    var json = Serializer(postData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseContent);
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error: {response.StatusCode}, Content: {responseContent}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                }
            }

            private static string Serializer(PostData postData)
            {
                return JsonConvert.SerializeObject(postData);
            }
        }
    }

}

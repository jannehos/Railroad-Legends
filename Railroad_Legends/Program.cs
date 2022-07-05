using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Railroad_Legends
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            //Jere mukana

            // Harrin kommentti


            Console.WriteLine("Hello World!"); // Lauran kommentti

            Console.WriteLine("Hello World!");

            //Jere mukana
            // Harrin kommentti 
        }
    }
    class Junat
    {
        public int Id;


    }


    public static class ApiHelper
    {
        // create HTTP client
        private static HttpClient GetHttpClient(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static async Task<T> RunAsync<T>(string url, string urlParams)
        {
            try
            {
                using (var client = GetHttpClient(url))
                {
                    // send GET request
                    HttpResponseMessage response = await client.GetAsync(urlParams);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        // JSON to an object
                        var result = JsonSerializer.Deserialize<T>(json);
                        return result;
                    }

                    return default(T);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return default(T);
            }



        }

    }

}

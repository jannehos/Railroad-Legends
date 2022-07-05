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
        static async Task Main(string[] args)
        {


            Console.WriteLine("Hello World!"); // Lauran kommentti
            await GetTrainInfo();
         
        }
        public static async Task GetTrainInfo()
        {
            Class1 klassi = new Class1();
            Console.Write("\nGive name of the fruit: ");
            int input = Convert.ToInt32(Console.ReadLine());

            Class1 juna = await JunaApi.GetTrain(input);

            if (juna == null)
                Console.WriteLine("\n  Fruit not found :(");
            else
                Console.WriteLine(klassi.trainNumber);

        }
        public static class JunaApi
        {
            const string url = "https://rata.digitraffic.fi/api/v1/trains/latest/";
            public static async Task<Class1> GetTrain(int trainNumber)
            {
                string urlParams = "" + trainNumber;

                Class1 response = await ApiHelper.RunAsync<Class1>(url, urlParams);

                return response;
            }
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
}

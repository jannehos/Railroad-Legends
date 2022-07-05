using System;
using System.Linq;
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

        public static class JunaApi
        {
            const string url = "https://rata.digitraffic.fi/api/v1/metadata/stations";
            public static async Task<Class2> GetTrain()
            {
                string urlParams = "";

                Class2 response = await ApiHelper.RunAsync<Class2>(url, urlParams);

                return response;
            }
        }
        public static async Task GetTrainInfo()
        {
            Asemat kokeilu = new Asemat();
            Class2 klassi = new Class2();
            //Console.Write("\nAnna junan numero: ");
            const string url = "https://rata.digitraffic.fi/api/v1";
            string urlParams =  "/metadata/stations";
            Class2 juna = await ApiHelper.RunAsync<Class2>(url, urlParams);
            Asemat muna = await ApiHelper.RunAsync<Asemat>(url, urlParams);
            //Console.WriteLine(muna.Property1[0]);
            var ala = (from o in juna.stationName where juna.stationName == "Alapitkä" select o).ToList();
            foreach(var o in ala)
            {
                Console.WriteLine(o);
            }
            //Console.WriteLine(ala);
            // Console.WriteLine(juna.stationName);
          /*  "passengerTraffic": false,
    "type": "STATION",
    "stationName": "Alapitkä",
    "stationShortCode": "APT",
    "stationUICCode": 415,
    "countryCode": "FI",
    "longitude": 27.535426,
    "latitude": 63.200823*/



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

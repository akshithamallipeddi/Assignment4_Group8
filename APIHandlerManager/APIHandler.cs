using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Assignment4_Group8.Models;
using Newtonsoft.Json.Linq;

namespace Assignment4_Group8.APIHandlerManager
{
    public class APIHandler
    {
        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://catalog.data.gov/dataset/crime-incidents-a7479
        // https://data.honolulu.gov/api/views/a96q-gyhq

        static string BASE_URL = "https://data.honolulu.gov/resource/a96q-gyhq.json";
        static string API_KEY = "K6jUkQDU6azHU7csUkpcfoZHnaRdvI4VwcsJv6qc"; //Add your API key here inside ""

        HttpClient httpClient;

        /// <summary>
        ///  Constructor to initialize the connection to the data source
        /// </summary>
        public APIHandler()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<RootData> GetData()
        {
            string API_PATH = BASE_URL;
            string apiData = "";

            List<RootData> data = null;

            httpClient.BaseAddress = new Uri(API_PATH);

            // It can take a few requests to get back a prompt response, if the API has not received
            //  calls in the recent past and the server has put the service on hibernation
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    apiData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!apiData.Equals(""))
                {
                    var aa = JArray.Parse(apiData).ToList();

                    data = JsonConvert.DeserializeObject<JArray>(apiData).ToObject<List<RootData>>();
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                   // var a = JsonConvert.DeserializeObject<RootData>(apiData);
                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }

            return data;
        }
    }
}

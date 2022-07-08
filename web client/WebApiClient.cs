using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using web_client.Models;

namespace web_client

{


    public class WebApiClient
    {
        private readonly HttpClient HttpClient;


        public WebApiClient()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("http://localhost:5071/api/");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        

        public async Task<PaisesResponseModel?> GetPaises()
        {
            HttpResponseMessage response = await HttpClient.GetAsync("Pais");

            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();

                PaisesResponseModel? paises = JsonConvert.DeserializeObject<PaisesResponseModel>(content);

                if (paises != null)
                {
                    return paises;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


    }













   


}
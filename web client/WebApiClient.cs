using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

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
            HttpResponseMessage response = await HttpClient.GetAsync("paises");

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






    public class PaisResponseModel

    {


        public int pais_ID { get; set; }
        public string pais_nombre { get; set; }
    

    }


    public partial class PaisesResponseModel
    {

        public IEnumerable<PaisResponseModel> paises { get; set; }

    
    }








    public class GenericRrespondeModel
    {
        public bool Succcess { get; set; }

        public string ErrorNumber { get; set; }
        public string ErrorDetail { get; set; }
        public int NumberOfRecords { get; set; }



    }
    
   
}
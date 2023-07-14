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

    ///<summary>
    ///Crea una conexion con un httpcliente a una API.
    /// </summary>
    
    public class WebApiClient
    {
        private readonly HttpClient HttpClient;

        /// <summary>
        /// Constructor de la clase <c>WebApiClient</c>, al instanciar la clase, se crear un nuevo HttpClient, que servira para conectarse
        /// a la API por medio de la URI quemada o suministrada por medio de un "futuro parametro".
        /// </summary>
        public WebApiClient()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("http://localhost:5071/api/");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Usa el cliente web para llamar al controlador Pais de la API RH
        /// por medio de una peticion GET async, Deserializa la informacion recebida en el Modelo <c>PaisesResponseModel</c>
        /// </summary>
        /// <returns>Null o un modelo que contiene informacion sobre los paises y estado de la operacion</returns>
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
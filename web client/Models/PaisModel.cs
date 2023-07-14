using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_client.Models
{
    /// <summary>
    /// Modelo para la entidad Pais
    /// </summary>
    public class PaisModel

    {
        /// <value>propiedad pais_ID correspondiente al ID del pais</value>      
        public string? pais_ID { get; set; }

        /// <value>propiedad pais_nombre correspondiente al nombre del pais</value>   
        public string? pais_nombre { get; set; }

    }

    /// <summary>
    /// Modelo de respuesta para la entidad Pais, hereda las propiedades de la clase <c>GenereicResponseModel</c>
    /// </summary>
    public class PaisResponseModel : GenericResponseModel
    {
        /// <value>propiedad pais_ID correspondiente al ID del pais</value>      
        public string? pais_ID { get; set; }

        /// <value>propiedad pais_nombre correspondiente al nombre del pais</value>   
        public string? pais_nombre { get; set; }
    }


    /// <summary>
    /// Modelo de respuesta para una lista de entidades Pais, hereda las propiedades de la clase <c>GenereicResponseModel</c>
    /// </summary>
    public class PaisesResponseModel : GenericResponseModel
    {
        /// <value>propiedad Paises del tipo IEnumerable correspondientea lista de paises del tipo Entidad <c>PaisModel</c> </value>  
        public IEnumerable<PaisModel>? Paises { get; set; }
    }


    /// <summary>
    /// Modelo de respuesta generica usada para el envio de request API
    /// </summary
    public class GenericResponseModel
    {
        /// <value>propiedad Succes, correspondiente a si el request es o no exitoso</value>  
        public bool Succcess { get; set; }

        /// <value>propiedad ErrorNumber, correspondiente al numero de error generado</value> 
        public string? ErrorNumber { get; set; }

        /// <value>propiedad ErrorDetail, correspondiente al mensaje de error</value> 
        public string? ErrorDetail { get; set; }

        /// <value>propiedad NumberOfRecord, correspondiente a la cantidad de datos para enviar</value> 
        public int NumberOfRecords { get; set; }
    }
}

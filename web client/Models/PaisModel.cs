using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_client.Models
{
    public class PaisModel

    {
        public string pais_ID { get; set; }
        public string pais_nombre { get; set; }

    }

    public class PaisResponseModel : GenericResponseModel
    {
        public string Pais_ID { get; set; }
        public string Pais_nombre { get; set; }
    }  

    public class PaisesResponseModel : GenericResponseModel
    {
        public IEnumerable<PaisModel> Paises { get; set; }
    }

    public class GenericResponseModel
    {
        public bool Succcess { get; set; }
        public string ErrorNumber { get; set; }
        public string ErrorDetail { get; set; }
        public int NumberOfRecords { get; set; }
    }
}

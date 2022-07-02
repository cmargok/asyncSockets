namespace WebApplication1.ResponseModels
{
    public class PaisResponseModel
    {
        public int pais_ID { get; set; }
        public string? pais_nombre { get; set; }
    }

    public partial class PaisesResponseModel
    {
        public IEnumerable<PaisResponseModel> paises { get; set; }

    }
}

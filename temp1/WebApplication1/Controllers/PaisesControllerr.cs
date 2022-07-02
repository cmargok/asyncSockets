using DataContext;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ResponseModels;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {

        private readonly db_rhContext rh_context;


        public PaisesController(db_rhContext _context)
        {
            rh_context = _context;
        }

        [HttpPost]
        public async Task<IActionResult> PostPais(PaisCreateModel paisName)
        {
            Pais pais = new();
            pais.pais_nombre = paisName.pais_nombre;

            rh_context.Paises.Add(pais);

            await rh_context.SaveChangesAsync();


            return Created("api/localizaciones/pais/" + pais.pais_ID, pais);
        }


        [HttpGet]
        public async Task<IActionResult> getPaises()
        {
            PaisesResponseModel paises = new();

            paises.paises = rh_context.Paises.ToList().Select(
                                                                    o => new PaisResponseModel
                                                                    {
                                                                        pais_ID = o.pais_ID,
                                                                        pais_nombre = o.pais_nombre

                                                                    });
            

           

            return Ok(paises);
        }



    }
}

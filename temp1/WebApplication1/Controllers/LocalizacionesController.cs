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
    public class LocalizacionesController : ControllerBase
    {
        private readonly db_rhContext rh_context;


        public LocalizacionesController( db_rhContext _context)
        {
            rh_context = _context; 
        }

       


        [HttpPost]
        [Route("Ciudad")]
        public async Task<IActionResult> PostCiudad(CiudadCreateModel ciudadName)
        {

            Ciudad ciudad = new();
            ciudad.CiudNombre = ciudadName.ciudad_name;

            Pais? pais = rh_context.Paises.SingleOrDefault(c => c.pais_nombre == ciudadName.pais_name);
                
                
            if (pais == null)
            {
                pais = new Pais { pais_nombre = ciudadName.pais_name   };

                ciudad.Pais = pais;
            }
            else
            {
                ciudad.Pais = pais;
            }

            rh_context.Ciudades.Add(ciudad);

            await rh_context.SaveChangesAsync();


            return Created("api/localizaciones/ciudad/" , ciudad);
        }


        













        }

    
    

}

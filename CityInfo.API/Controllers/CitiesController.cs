using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;
        public CitiesController(CitiesDataStore citiesDataStore) 
        {
            _citiesDataStore = citiesDataStore;
        }

        public CitiesDataStore CitiesDataStore { get; }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {

            return Ok(_citiesDataStore.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityReturn = _citiesDataStore.Cities.FirstOrDefault(x => x.Id == id);

            if (cityReturn == null)
            {
                return NotFound();
            }

            return Ok(cityReturn);
        }
    }
}

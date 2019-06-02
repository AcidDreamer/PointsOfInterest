using CityInfo.API.DataStores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controller
{
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetCities()
        {
            var cities = CitiesDataStore.Current;
            return Ok(cities);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCities(int id)
        {
            var city = CitiesDataStore.Current.Cities.Where(c => c.ID == id).FirstOrDefault();

            if (city == null)
                return NotFound();

            return Ok(city);
        }

    }
}

using CityInfo.API.DataStores;
using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controller
{
    [Route("api/cities")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;

        }
        [HttpGet("{cityId:int}/pointsofinterest")]
        public IActionResult PointsOfInterest(int cityID)
        {
            var city = CitiesDataStore.Current.Cities.Where(c => c.ID == cityID).FirstOrDefault();

            if (city == null)
            {
                _logger.LogInformation($"City with ID {cityID} wasn't found when accessing points of interest.");
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityId:int}/pointsofinterest/{poiID:int}" , Name = "GetPointOfInterest")]
        public IActionResult GetPointsOfInterest(int cityID,int poiID)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null)
                return NotFound();

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(poi => poi.ID == poiID);
            if (pointOfInterest == null)
                return NotFound();

            return Ok(pointOfInterest);
        }

        [HttpPost("{cityId:int}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityID,
            [FromBody] PointOfInterestCreationDTO pointOfInterest)
        {
            if (pointOfInterest == null)
                return BadRequest();

            if (pointOfInterest.Description == pointOfInterest.Name)
                ModelState.AddModelError("Description", "The provided description cannot be the same as the name");
            //TODO: Check FluentValidation

            if (!ModelState.IsValid)
                return BadRequest();

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null)
                return NotFound();

            var maxPointOfInterestID = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.ID);

            var finalPointOfInterest = new PointOfInterestDTO()
            {
                ID = maxPointOfInterestID + 1,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityID,
                poiID = finalPointOfInterest.ID
            }, finalPointOfInterest);
        }

        [HttpPut("{cityId:int}/pointsofinterest/{poiID:int}")]
        public IActionResult UpdatePointOfInterest(int cityID, int poiID,
            [FromBody] PointOfInterestUpdateDTO poiModel)
        {
            if (poiModel == null)
                return BadRequest();

            if (poiModel.Description == poiModel.Name)
                ModelState.AddModelError("Description", "The provided description cannot be the same as the name");
            //TODO: Check FluentValidation

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null)
                return NotFound();

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(poi => poi.ID == poiID);

            if (pointOfInterest == null)
                return NotFound();

            pointOfInterest.Name = poiModel.Name;
            pointOfInterest.Description = poiModel.Description;

            return NoContent();
        }


        [HttpPatch("{cityId:int}/pointsofinterest/{poiID:int}")]
        public IActionResult PatchPointOfInterest(int cityID, int poiID,
            [FromBody] JsonPatchDocument<PointOfInterestUpdateDTO> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null)
                return NotFound();

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(poi => poi.ID == poiID);

            if (pointOfInterest == null)
                return NotFound();

            var poiToPatch =
                new PointOfInterestUpdateDTO()
                {
                    Name = pointOfInterest.Name,
                    Description = pointOfInterest.Description
                };

            patchDoc.ApplyTo(poiToPatch);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (poiToPatch.Description == poiToPatch.Name)
                ModelState.AddModelError("Description", "The provided description cannot be the same as the name");

            TryValidateModel(poiToPatch);

            pointOfInterest.Name = poiToPatch.Name;
            pointOfInterest.Description = poiToPatch.Description;

            return NoContent();
        }


        [HttpDelete("{cityId:int}/pointsofinterest/{poiID:int}")]
        public IActionResult DeletePointOfInterest(int cityID, int poiID)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.ID == cityID);
            if (city == null)
                return NotFound();

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(poi => poi.ID == poiID);

            if (pointOfInterest == null)
                return NotFound();

            city.PointsOfInterest.Remove(pointOfInterest);

            return NoContent();
        }


    }
}

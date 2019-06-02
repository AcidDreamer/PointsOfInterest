using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.DataStores
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDTO> Cities{ get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDTO>
            {
                new CityDTO()
                {
                    ID = 1,
                    Name = "Athens",
                    Description = "Daaah",
                    PointsOfInterest = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            ID = 1,
                            Name = "Parthenon",
                            Description = "Lorem ipsum."
                        },
                        new PointOfInterestDTO()
                        {
                            ID = 2,
                            Name = "Acropolis",
                            Description = "Lorem ipsum 2.0."
                        }
                    }
                },
                new CityDTO()
                {
                    ID = 2,
                    Name = "Thessaloniki",
                    Description = "Δε με λες",
                    PointsOfInterest = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            ID = 3,
                            Name = "Souvlaki",
                            Description = "Eeeee ti les."
                        },
                        new PointOfInterestDTO()
                        {
                            ID = 4,
                            Name = "Alifi",
                            Description = ";)"
                        }
                    }
                },
                new CityDTO()
                {
                    ID = 3,
                    Name = "Kalamata",
                    Description = "Ksereis esy ;)",
                    PointsOfInterest = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            ID = 5,
                            Name = "Ksereis esy",
                            Description = ";)"
                        },
                    }
                },
            };
        }
    }
}

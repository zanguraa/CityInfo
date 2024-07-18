using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

       // public static CitiesDataStore Current { get; set; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Kaspi",
                    Description = "City of industry",
                    PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Saakadze tower",
                            Description = "Saakadze Tower is historycal place in Kaspi"
                        },
                        new PointOfInterestDto()
                        {
                            Id= 2,
                            Name = "Rkoni",
                            Description = "Rkoni is Village in Kaspi, where is 'Bridge of Tamari' wich is builded with egs and rocks"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Tbilisi",
                    Description = "Capital City of Georgia",
                     PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Botanical Garden",
                            Description = "Botanical Garden is very beautiful place in Tbilisi"
                        },
                        new PointOfInterestDto()
                        {
                            Id= 2,
                            Name = "Mtatsminda",
                            Description = "Mtatsminda is mountain of view over Tbilisi"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Kutaisi",
                    Description = "Old Capital City of Georgia",
                     PointOfInterests = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Bagrati cathedral",
                            Description = "Bagrati cathedral is historycal place in Kutaisi"
                        },
                        new PointOfInterestDto()
                        {
                            Id= 2,
                            Name = "Gelati Cathedral",
                            Description = "Gelati Cathedral in Kutaisi is the oldest cathedral in this region"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 4,
                    Name = "batumi",
                    Description = "SeaSide City"
                }
        };

        }
    }
}

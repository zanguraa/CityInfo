using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _cityInfoContext;

        public CityInfoRepository(CityInfoContext cityInfoContext)
        {
            _cityInfoContext = cityInfoContext ?? throw new ArgumentNullException(nameof(cityInfoContext));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _cityInfoContext.Cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _cityInfoContext.Cities as IQueryable<City>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery) || a.Description != null && a.Description.Contains(searchQuery));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetaData = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(a => a.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task<City?> GetCityAsync(int CityId, bool includepointsOfInterest)
        {
            if (includepointsOfInterest)
            {
                return await _cityInfoContext.Cities
                    .Include(c => c.PointOfInterest)
                    .Where(c => c.Id == CityId)
                    .FirstOrDefaultAsync();
            }

            return await _cityInfoContext.Cities
                .Where(c => c.Id == CityId)
                .FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointId)
        {
            return await _cityInfoContext.PointOfInterest
                .FirstOrDefaultAsync(p => p.CityId == cityId && p.Id == pointId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointOfInterestForCityAsync(int CityId)
        {
            return await _cityInfoContext.PointOfInterest
                 .Where(p => p.CityId == CityId).ToListAsync();
        }

        public async Task<bool> CityExistsAsync(int cityid)
        {
            return await _cityInfoContext.Cities.AnyAsync(c => c.Id == cityid);
        }



        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointOfInterest.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _cityInfoContext.SaveChangesAsync() >= 0);
        }

        public void DeletePointOfInterestForCityAsync(PointOfInterest pointOfInterest)
        {
            _cityInfoContext.PointOfInterest.Remove(pointOfInterest);
        }
    }
}

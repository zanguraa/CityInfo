using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int CityId, bool includepointsOfInterest);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int CityId, int PointId);
        Task<IEnumerable<PointOfInterest>> GetPointOfInterestForCityAsync(int CityId);
        Task<bool> CityExistsAsync(int cityid);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterestForCityAsync(PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}

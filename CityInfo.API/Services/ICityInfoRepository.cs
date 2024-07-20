using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int CityId, bool includepointsOfInterest);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int CityId, int PointId);
        Task<IEnumerable<PointOfInterest>> GetPointOfInterestForCityAsync(int CityId);
        Task<bool> CityExistsAsync(int cityid);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}

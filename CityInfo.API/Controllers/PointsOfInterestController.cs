using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, IMapper mapper, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                _logger.LogInformation(
                    $"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();
            }

            var pointOfInterestForCity = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterestForCity));
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterst = await _cityInfoRepository.GetPointOfInterestForCityAsync(cityId, pointOfInterestId);

            if (pointOfInterst == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterst));
        }


        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            try
            {
                if (!await _cityInfoRepository.CityExistsAsync(cityId))
                {
                    return NotFound();
                }

                var finalPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

                await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);

                await _cityInfoRepository.SaveChangesAsync();

                var createdPointOfInterestReturn = _mapper.Map<PointOfInterestDto>(finalPointOfInterest);

                return CreatedAtRoute("GetPointOfInterest",
                    new
                    {
                        cityId = cityId,
                        pointOfInterestId = createdPointOfInterestReturn.Id,
                    }, createdPointOfInterestReturn);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework or just log to console for now)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        //[HttpPut("{pointofinterestid}")]
        //public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointofinterestid, PointOfInterestForUpdateDto pointOfInterestForUpdateDto)
        //{

        //}

        //[HttpPatch("{pointofinterestid}")]
        //public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        //{

        //}

        //[HttpDelete("{pointofinterestid}")]
        //public ActionResult DeletePointOfInterest(int cityId, int pointofinterestid)
        //{


        //}
    }
}

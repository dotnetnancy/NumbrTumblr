using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Models;
using VSNumberTumbler.Services;
using VSNumberTumbler.ViewModels;

namespace VSNumberTumbler.Controllers.Api
{
    public class ShuffleController : Controller
    {
        IShuffleService _shuffleService;
        //IConfigurationRoot _config;

        public ShuffleController(IShuffleService shuffleService)
        {
            _shuffleService = shuffleService;
        }

        [HttpGet("api/shuffle/GetShuffle/{shuffleId}")]
        public IActionResult GetShuffle(int shuffleId)
        {
            var data = _shuffleService.GetShuffle(shuffleId);
            return Ok(AutoMapper
                .Mapper
                .Map<ShuffleViewModel>(data));
        }

        ///api/shuffle/" + vm.numberSetID + "/" + shuffleType)

        [HttpGet("api/shuffle/{numberSetId}/{shuffleIncludeType}")]
        public IActionResult Get(int numberSetId, string shuffleIncludeType)
        {
            var shuffle = _shuffleService.ShuffleNumbersByShuffleType(numberSetId, shuffleIncludeType);

            return Ok(shuffle);
        }

        [HttpGet("api/shuffle/{numberSetId}")]
        public IActionResult Get(int numberSetId)
        {
            var data = _shuffleService.GetShufflesByNumberSetId(numberSetId);

            return Ok(AutoMapper
                .Mapper
                .Map<IEnumerable<ShuffleViewModel>>(data));
        }

        [HttpGet("api/shuffle")]
        public IActionResult Get()
        {
            var data = _shuffleService.GetAllShuffles();

            return Ok(AutoMapper
                .Mapper
                .Map<IEnumerable<ShuffleViewModel>>(data));
        }

        [HttpDelete("api/shuffle/delete/{shuffleID}")]
        public IActionResult Delete(int shuffleID)
        {
            if (_shuffleService.DeleteShuffle(shuffleID))
            {
                return Ok();
            }
            //in general if the validation failed or the database call failed you would just
            //fall down here to return a bad request.
            return BadRequest("Failed to delete the shuffle");
            //if validation was bad then you would return a bad request
            //return BadRequest(ModelState);
        }

        [HttpPut("api/shuffle/edit")]
        public IActionResult Put([FromBody] ShuffleViewModel shuffle)
        {
            var shuffleToEdit = AutoMapper.Mapper.Map<Shuffle>(shuffle);
            if (_shuffleService.EditShuffle(shuffleToEdit))
            {
                return Accepted("$api/shuffle/{shuffle}",
                    AutoMapper
                    .Mapper
                    .Map<ShuffleViewModel>(shuffleToEdit));
            }
            return BadRequest("Failed to save the shuffle");
        }

        [HttpPost("api/shuffle")]
        public IActionResult Post([FromBody] ShuffleViewModel shuffle)
        {
            var newShuffle = AutoMapper.Mapper.Map<Shuffle>(shuffle);

            if (_shuffleService.AddShuffle(newShuffle))
            {
                return Created("$api/shuffle/{shuffle}",
                    AutoMapper
                    .Mapper
                    .Map<ShuffleViewModel>(newShuffle));
            }
            return BadRequest("Failed to save the shuffle");
        }

        [HttpPost("api/shuffleNumbers/{numberSetID}/{shuffleName}")]
        public IActionResult Post([FromBody] List<ShuffleNumberViewModel> shuffleNumbers, [FromRoute] int numberSetID, [FromRoute]string shuffleName)
        {
            var newShuffleNumbers = AutoMapper.Mapper.Map<IEnumerable<ShuffleNumber>>(shuffleNumbers).ToList();
            Shuffle shuffle = _shuffleService.UpsertShuffle(newShuffleNumbers, numberSetID,shuffleName);
                      
            newShuffleNumbers.ForEach(x => x.ShuffleID = shuffle.ShuffleID);           

            if (_shuffleService.AddShuffleNumbers(newShuffleNumbers))
            {
                return Created("$api/shuffleNumbers/{shuffleNumbers}",
                    AutoMapper
                    .Mapper
                    .Map<IEnumerable<ShuffleNumberViewModel>>(newShuffleNumbers));
            }
            return BadRequest("Failed to save the shuffle numbers");
        }

        [HttpDelete("api/shuffleNumbers/delete")]
        public IActionResult Delete([FromBody] List<ShuffleNumberViewModel> shuffleNumbers)
        {
            var shuffleNumbersToDelete = AutoMapper.Mapper.Map<IEnumerable<ShuffleNumber>>(shuffleNumbers);

            if (_shuffleService.DeleteShuffleNumbers(shuffleNumbersToDelete))
            {
                return Ok();
            }
            return BadRequest("Failed to delete the shuffle numbers");
        }
    }
}

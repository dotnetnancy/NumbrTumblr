using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Models;
using VSNumberTumbler.Models.Repositories;
using VSNumberTumbler.Services;
using VSNumberTumbler.ViewModels;

namespace VSNumberTumbler.Controllers.Api
{
    public class NumberSetController : Controller
    {
        INumberSetService _numberSetService; 

        public NumberSetController(INumberSetService numberSetService)
        {
            //_config = config;
            _numberSetService = numberSetService;
        }

        [HttpGet("api/numberset/{numberSetId}")]
        public IActionResult Get(int numberSetId)
        {
            var data = _numberSetService.GetNumberSet(numberSetId);
            return Ok(AutoMapper
                .Mapper
                .Map<NumberSetViewModel>(data));
        }

        [HttpGet("api/numberset")]
        public IActionResult Get()
        {
            var data = _numberSetService.GetAllNumberSets();

            return Ok(AutoMapper
                .Mapper
                .Map<IEnumerable<NumberSetViewModel>>(data));
        }

        [HttpDelete("api/numberset/delete/{numberSetID}")]
        public IActionResult Delete(int numberSetID)
        {         
            if (_numberSetService.DeleteNumberSet(numberSetID))
            {
                return Ok();
            }
            //in general if the validation failed or the database call failed you would just
            //fall down here to return a bad request.
            return BadRequest("Failed to delete the numberset");
            //if validation was bad then you would return a bad request
            //return BadRequest(ModelState);
        }

        [HttpPut("api/numberset/edit")]
        public IActionResult Put([FromBody] NumberSetViewModel numberSet)
        {
            var numberSetToEdit = AutoMapper.Mapper.Map<NumberSet>(numberSet);
            
            if (_numberSetService.EditNumberSet(numberSetToEdit))
            {
                return Accepted("$api/numberset/{numberSet}",
                    AutoMapper
                    .Mapper
                    .Map<NumberSetViewModel>(numberSetToEdit));
            }           
            return BadRequest("Failed to save the numberset");           
        }

        [HttpPost("api/numberset")]
        public IActionResult Post([FromBody] NumberSetViewModel numberSet)
        {  
            var newNumberSet = AutoMapper.Mapper.Map<NumberSet>(numberSet);
         
            if (_numberSetService.AddNumberSet(newNumberSet))
            {
                return Created("$api/numberset/{numberSet}",
                    AutoMapper
                    .Mapper
                    .Map<NumberSetViewModel>(newNumberSet));
            }             
            return BadRequest("Failed to save the numberset");          
        }

        [HttpPost("api/numbersetNumbers")]
        public IActionResult Post([FromBody] List<NumberSetNumberViewModel> numberSetNumbers)
        {
            var newNumberSetNumbers = AutoMapper.Mapper.Map<IEnumerable<NumberSetNumber>>(numberSetNumbers);
           
            if (_numberSetService.AddNumberSetNumbers(newNumberSetNumbers))
            {
                return Created("$api/numbersetNumbers/{numberSetNumbers}",
                    AutoMapper
                    .Mapper
                    .Map<IEnumerable<NumberSetNumberViewModel>>(newNumberSetNumbers));
            }            
            return BadRequest("Failed to save the numberset numbers");            
        }

        [HttpDelete("api/numbersetNumbers/delete")]
        public IActionResult Delete([FromBody] List<NumberSetNumberViewModel> numberSetNumbers)
        {
            var numberSetNumbersToDelete = AutoMapper.Mapper.Map<IEnumerable<NumberSetNumber>>(numberSetNumbers);
            
            if (_numberSetService.DeleteNumberSetNumbers(numberSetNumbersToDelete))
            {
                return Ok();
            }
            return BadRequest("Failed to delete the numberset numbers");
        }
    }
}

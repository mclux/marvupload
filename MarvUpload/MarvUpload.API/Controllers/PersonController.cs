using MarvUpload.Core.Entities;
using MarvUpload.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarvUpload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _unitOfWork.PersonRepository.GetAll();
            return Ok(result);
        }

        // GET api/<PersonController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<PersonController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<Person> Person)
        {
            _unitOfWork.PersonRepository.AddRange(Person);
            bool isComplete = await _unitOfWork.Complete();
            if(isComplete)
            {
                return Ok();
            }

            return BadRequest();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Person value)
        {
            var personRecord = await _unitOfWork.PersonRepository.Get(id);
            if(personRecord==null)
            {
                return NotFound($"Person with ID {id} does not exist.");
            }

            personRecord.Active = value.Active;
            personRecord.Age = value.Age;
            personRecord.FirstName = value.FirstName;
            personRecord.LastName = value.LastName;
            personRecord.Sex = value.Sex;
            personRecord.Mobile = value.Mobile;
            _unitOfWork.PersonRepository.Update(personRecord);
            bool isComplete = await _unitOfWork.Complete();
            if (isComplete)
            {
                return Ok("Record updated successfully.");
            }
            else
            {
                return Ok("Record not updated. Please try again.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var personRecord = await _unitOfWork.PersonRepository.Get(id);
            if (personRecord == null)
            {
                return NotFound($"Person with ID {id} does not exist.");
            }

            _unitOfWork.PersonRepository.Delete(personRecord);
            bool isComplete = await _unitOfWork.Complete();
            if (isComplete)
            {
                return Ok("Record updated deleted.");
            }
            else
            {
                return Ok("Record not deleted. Please try again.");
            }
        }
    }
}

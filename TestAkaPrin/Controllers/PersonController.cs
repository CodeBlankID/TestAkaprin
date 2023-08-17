using Microsoft.AspNetCore.Mvc;
using TestAkaPrin.Models;

namespace TestAkaPrin.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        // GET: api/<PersonController>
        [HttpGet]
        public IEnumerable<Identity> Get()
        {
            var list = new Identity().dbShow("SELECT");
            return list;
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public Identity Get(int id)
        {
            var list = new Identity().dbShow("SELECTBYID", id);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return new Identity();
            }
           
        }

        // POST api/<PersonController>
        [HttpPost]
        public IActionResult Post([FromBody] Identity value)
        {
            var getstatus = new Identity().dbModify(value,"INSERT");
            if (Boolean.Parse(getstatus))
            {
                return StatusCode(200, "Data Inserted Successfully");
            }
            else
            {
                return StatusCode(400, "Failed Insert data");
            }

        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Identity value)
        {
            var getstatus = new Identity().dbModify(value,"UPDATE",id);
            if (Boolean.Parse(getstatus))
            {
                return StatusCode(200,"Data Updated Successfully");
            }
            else
            {
                return StatusCode(400,"Failed Update data");
            }
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Identity a = new Identity();
            var getstatus = new Identity().dbModify(a,"UPDATE",id);
            if (Boolean.Parse(getstatus))
            {
                return StatusCode(200, "Data Updated Successfully");
            }
            else
            {
                return StatusCode(400, "Failed Update data");
            }
        }
    }
}

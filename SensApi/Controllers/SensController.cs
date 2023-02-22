using Microsoft.AspNetCore.Mvc;
using SensApi.Models;
using SensApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensController : ControllerBase
    {
        private readonly ISensScrapper _scrapper;

        public SensController(ISensScrapper scrapper)
        {
            _scrapper = scrapper;
        }

        // GET: api/Sens
        [HttpGet]
        public async Task<IEnumerable<SensAnnouncement>> Get()
        {
            //Store this URL in a constant
            var data = await _scrapper.ScrapeData("https://clientportal.jse.co.za/communication/sens-announcements");
            data.ToList();

            return data.ToList();
            //return new string[] { "value1", "value2" };
        }

        // GET api/Sens/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Sens
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SensController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SensController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

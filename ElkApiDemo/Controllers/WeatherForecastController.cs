using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElkApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        /// <summary>
        /// No errors or delays
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-north")]
        public IEnumerable<WeatherForecast> GetNorth()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// Random Delay
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-south")]
        public async Task<IEnumerable<WeatherForecast>> GetSouth()
        {
            var rand = new Random();
            var value = (int)(rand.NextDouble() * 10);

            await Task.Delay(value * 1000);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// BadRequest on value % 4 == 0
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-east")]
        public ActionResult GetEast()
        {
            var rand = new Random();
            var value = (int)(rand.NextDouble() * 10);

            if (value % 4 == 0)
            {
                return BadRequest("Oops!");
            }

            var rng = new Random();
            var arr = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(arr);
        }

        /// <summary>
        /// Exception on value % 4 == 0
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get-west")]
        public ActionResult GetWest()
        {
            var rand = new Random();
            var value = (int)(rand.NextDouble() * 10);
            if (value % 4 == 0)
            {
                throw new Exception("This is bad code!!");
            }

            var rng = new Random();
            var arr = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Ok(arr);
        }
    }
}

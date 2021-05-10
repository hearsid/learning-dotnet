using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cinemaAPI.Models;
using Microsoft.EntityFrameworkCore;
using cinemaAPI.Data;
using Microsoft.AspNetCore.Http;

namespace cinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController: ControllerBase
    {
        private CinemaDbContext _dbContext;

        public MoviesController(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Movies);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Movie movie = _dbContext.Movies.Find(id);
            if (movie == null)
            {
                return Ok("No movie against this ID.");
            }
            return Ok(_dbContext.Movies.Find(id));
        }

        // api/Movies/retrieveId/1
        [HttpGet("[action]/{id}")]
        public int retrieveId(int id)
        {
            return id;
        }

        [HttpPost]
        public IActionResult Post([FromForm] Movie movieObj)
        {
            if (movieObj.Image != null)
            {
                Guid guid = Guid.NewGuid();
                string filePath = Path.Combine("wwwroot", movieObj.Image.FileName + "_" + guid + ".jpg");
                FileStream fs = new FileStream(filePath, FileMode.Create);
                movieObj.Image.CopyTo(fs);
                movieObj.ImageUrl = filePath.Remove(0, 7);
                _dbContext.Movies.Add(movieObj);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Movie movieObj) // [FromBody] for raw json
        {
            Movie movie = _dbContext.Movies.Find(id);
            if (movie == null)
            {
                return NotFound("Record not found");
            } 
            movie.Name = movieObj.Name;
            movie.Rating = movieObj.Rating;

            if(movieObj.Image != null)
            {
                Guid guid = Guid.NewGuid();
                string filePath = Path.Combine("wwwroot", movieObj.Image.FileName + "_" + guid + ".jpg");
                FileStream fs = new FileStream(filePath, FileMode.Create);
                movie.Image.CopyTo(fs);
                movie.ImageUrl = filePath.Remove(0, 7);
            }
            _dbContext.SaveChanges();
            return Ok("Record Updated Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Movie movie = _dbContext.Movies.Find(id);
            if (movie == null)
            {
                return NotFound("Record not found");
            }
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
            return Ok("Record Deleted.");
        }
        /**
        private static List<Movie> movies = new List<Movie>
        {
            new Movie(){ Id = 0, Name = "Mission Impossible: Rouge Nation" },
            new Movie(){ Id = 1, Name = "Gangs of Wasseypur" }
        };

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return movies;
        }

        [HttpPost]
        public void Post([FromBody] Movie movie)
        {
            movies.Add(movie); 
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Movie movie)
        {
            movies[id] = movie;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            movies.RemoveAt(id);
        }
        **/
    }
}

using DataEntities;
using MoviesRepositoryStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MoviesWebAPI.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        MoviesData data = new MoviesData();


        [Route("")]
        public IEnumerable<MovieDetails> Get()
        {
            var movies =  data.GetAllMovies();
            return movies;
        }

        [Route("{id:int}")]
        public MovieDetails Get(int id)
        {
            var movie = data.GetMovieByID(id);

            if (movie == null)
            {
                return null;
            }

            return movie;
        }


        
        // POST: api/Movies
        public IHttpActionResult Post([FromBody]MovieDetails value)
        {
            bool result = data.Save(value);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Invalid Request.");
        }

    }
}

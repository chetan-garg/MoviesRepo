using DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRepositoryStorage
{
    public class MoviesData : IMovieData
    {
        MoviesXmlRepository _repo = new MoviesXmlRepository();

        public IEnumerable<MovieDetails> GetAllMovies()
        {
            return _repo.GetAll();
        }

        public MovieDetails GetMovieByID(int id)
        {
            return _repo.GetByID(id);
        }

        public bool Save(MovieDetails details)
        {
            return _repo.Add(details);
        }
    }
}

using DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesRepositoryStorage
{
    public interface IMovieData
    {
        bool Save(MovieDetails details);

        IEnumerable<MovieDetails> GetAllMovies();

        MovieDetails GetMovieByID(int id);
    }
}

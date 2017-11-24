using DataEntities;
using System.Collections.Generic;

namespace MoviesRepositoryStorage
{
    public interface IMovieRepository
    {
        IEnumerable<MovieDetails> GetAll();

        MovieDetails GetByID(int id);

        bool Add(MovieDetails movie);
    }
}
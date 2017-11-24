using DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MoviesRepositoryMVC.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Index()
        {
            IEnumerable<MovieDetails> movies = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SettingsReaderClass.UrlString);
                //HTTP GET
                var responseTask = client.GetAsync("movies");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<MovieDetails>>();
                    readTask.Wait();

                    movies = readTask.Result;
                }
                else
                {
                    movies = Enumerable.Empty<MovieDetails>();

                    ModelState.AddModelError(string.Empty, "An Error Occured on server. Please contact admin.");
                }
            }
            return View(movies.OrderBy(mov => mov.ID));
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<MovieDetails> movies = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(SettingsReaderClass.UrlString);
                //HTTP GET
                var responseTask = client.GetAsync(string.Format("movies/{0}", id));
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<MovieDetails>();
                    readTask.Wait();

                    var movie = readTask.Result;
                    if (movie != null)
                    {
                        movies = new List<MovieDetails>();
                        ((List<MovieDetails>)movies).Add(movie);
                    }
                    else
                    {
                        movies = Enumerable.Empty<MovieDetails>();
                        ModelState.AddModelError(string.Empty, "An Error Occured on server. Please contact admin.");
                    }
                }
                else
                {
                    movies = Enumerable.Empty<MovieDetails>();

                    ModelState.AddModelError(string.Empty, "An Error Occured on server. Please contact admin.");
                }
            }
            return View(movies);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        public ActionResult Create(MovieDetails movieDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(SettingsReaderClass.UrlString);
                        //HTTP GET
                        var responseTask = client.PostAsJsonAsync("movies/movie", movieDetails);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (!result.IsSuccessStatusCode)
                        {
                            ModelState.AddModelError(string.Empty, "An Error Occured on server. Please contact admin.");
                            return View();
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(movieDetails);
            }
            catch
            {
                return View();
            }
        }



    }
}

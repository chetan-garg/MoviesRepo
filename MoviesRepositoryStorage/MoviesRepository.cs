using DataEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;

namespace MoviesRepositoryStorage
{
    internal class MoviesXmlRepository : IMovieRepository
    {
        private string xmlFileName = "MoviesData.xml";

        List<MovieDetails> allDetails = new List<MovieDetails>();

        public bool Add(MovieDetails movie)
        {
            try
            {
                var path = HttpContext.Current.Request.PhysicalApplicationPath + "\\bin\\";
                var xmlFile = path + xmlFileName;
                if (allDetails.Count == 0)
                {
                    GetAll();
                }
                if (movie != null && allDetails.Find(x => x.MovieName == movie.MovieName && x.YearOfRelease == movie.YearOfRelease) == null)
                {
                    movie.ID = allDetails.Max(x => x.ID) + 1;
                    string xml = XmlToObjectConverter.GetMovieXml(movie);
                    if (string.IsNullOrEmpty(xml))
                    {
                        return false;
                    }
                    XmlDocument doc = new XmlDocument();
                    doc.Load(xmlFile);
                    XmlDocument nodeDoc = new XmlDocument();
                    nodeDoc.LoadXml(xml);
                    XmlNode node = doc.ImportNode(nodeDoc.FirstChild, true);
                    doc.DocumentElement.AppendChild(node);
                    doc.Save(xmlFile);
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<MovieDetails> GetAll()
        {
            try
            {
                var path = HttpContext.Current.Request.PhysicalApplicationPath + "\\bin\\";
                var xmlFile = path + xmlFileName;

                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFile);
                if (doc != null)
                {
                    foreach (var node in doc.SelectNodes("//MovieDetails"))
                    {
                        allDetails.Add(XmlToObjectConverter.GetMovieDetails(((XmlNode)node).OuterXml));

                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return allDetails;
        }

        public MovieDetails GetByID(int id)
        {
            if (allDetails.Count == 0)
            {
                GetAll();
            }

            return allDetails.Find(x => x.ID == id);
        }
    }
}
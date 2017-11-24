using DataEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MoviesRepositoryStorage
{
    public class XmlToObjectConverter
    {
        public static MovieDetails GetMovieDetails(string node)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(node);
            MovieDetails detail = new MovieDetails();

            detail.ID = int.Parse(doc.SelectSingleNode("//ID").InnerText);
            detail.MovieName = doc.SelectSingleNode("//MovieName").InnerText;
            detail.MovieDescription = doc.SelectSingleNode("//MovieDescription").InnerText;
            detail.YearOfRelease = int.Parse(doc.SelectSingleNode("//YearOfRelease").InnerText);
            return detail;
        }

        public static string GetMovieXml(MovieDetails details)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                writer.WriteStartElement("MovieDetails");
                writer.WriteElementString("ID", details.ID.ToString());
                writer.WriteElementString("MovieName", details.MovieName);
                writer.WriteElementString("MovieDescription", details.MovieDescription);
                writer.WriteElementString("YearOfRelease", details.YearOfRelease.ToString());
                writer.WriteEndElement();
            }
            return sb.ToString();
        }
    }
}
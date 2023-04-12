using System.Net;
using System.Xml.Linq;

namespace NSSCTF.InvitationGenerator
{
    public static class Utils
    {
        public static bool SaveStreamToFile(string Path, Stream stream)
        {
            try
            {
                using (var fileStream = File.Create(Path))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void SaveStyle(PosterStyle ps, string path)
        {
            XDocument doc = new XDocument(
                            new XElement("Poster",
                            new XElement("Font", new XAttribute("Value", ps.Font)),
                            new XElement("FontSize", new XAttribute("Value", ps.FontSize)),
                            new XElement("FontColor", new XAttribute("Value", ps.FontColor)),
                            new XElement("Poster", new XAttribute("File", ps.Poster)),
                            new XElement("Gravity", new XAttribute("Value", ps.Gravity)),
                            new XElement("AvatarStyle", new XAttribute("Value", ps.AvatarStyle)),
                            new XElement("AvatarSize", new XAttribute("Width", ps.AvatarSize.Width), new XAttribute("Height", ps.AvatarSize.Width)),
                            new XElement("AvatarPosition", new XAttribute("X", ps.AvatarPosition.X), new XAttribute("Y", ps.AvatarPosition.Y)),
                            new XElement("UsernamePosition", new XAttribute("X", ps.UsernamePosition.X), new XAttribute("Y", ps.UsernamePosition.Y))
            ));
            doc.Save(path);
        }
    }
}

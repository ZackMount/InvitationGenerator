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
                            new XElement("Font", new XAttribute("value", ps.Font)),
                            new XElement("FontSize", new XAttribute("value", ps.FontSize)),
                            new XElement("FontColor", new XAttribute("value", ps.FontColor)),
                            new XElement("Poster", new XAttribute("value", ps.Poster)),
                            new XElement("Gravity", new XAttribute("value", ps.Gravity)),
                            new XElement("AvatarStyle", new XAttribute("value", ps.AvatarStyle)),
                            new XElement("AvatarSize", new XAttribute("value", $"{ps.AvatarSize.Width}x{ps.AvatarSize.Height}")),
                            new XElement("AvatarPosition", new XAttribute("value", $"{ps.AvatarPosition.X},{ps.AvatarPosition.Y}")),
                            new XElement("UsernamePosition", new XAttribute("value", $"{ps.UsernamePosition.X},{ps.UsernamePosition.Y}"))
            ));
            doc.Save(path);
        }
    }
}

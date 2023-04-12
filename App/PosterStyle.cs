using System.Xml.Linq;

namespace NSSCTF.InvitationGenerator
{
    /// <summary>
    /// 海报风格
    /// </summary>
    public class PosterStyle
    {
        /// <summary>
        /// 字体颜色(RGB or 颜色名)
        /// </summary>
        public string FontColor { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string Font { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public Gravity Gravity { get; set; }
        /// <summary>
        /// 用户名位置
        /// </summary>
        public Point UsernamePosition { get; set; }
        /// <summary>
        /// 头像位置
        /// </summary>
        public Point AvatarPosition { get; set; }
        /// <summary>
        /// 头像大小
        /// </summary>
        public Size AvatarSize { get; set; }
        /// <summary>
        /// 头像风格
        /// </summary>
        public AvatarStyleType AvatarStyle { get; set; }
        /// <summary>
        /// 海报文件位置
        /// </summary>
        public string Poster { get; set; }
        /// <summary>
        /// 解析配置文件
        /// </summary>
        /// <param name="Path"></param>
        public PosterStyle() 
        {

        }
        public PosterStyle(string Path)
        {
            if (!File.Exists(Path)) new FileNotFoundException(Path);
            Parse(Path);
        }
        public void Parse(string Path)
        {
            XDocument doc = XDocument.Load(Path);
            XElement poster = doc.Element("Poster");

            PosterStyle ps = new PosterStyle();
            Font = (string)poster.Element("Font").Attribute("Value");
            FontSize = (int)poster.Element("FontSize").Attribute("Value");
            FontColor = (string)poster.Element("FontColor").Attribute("Value");
            Poster = (string)poster.Element("Poster").Attribute("File");
            Gravity = GetEnumValue<Gravity>(poster.Element("Gravity"), "Value");
            AvatarStyle = GetEnumValue<AvatarStyleType>(poster.Element("AvatarStyle"), "Value");
            AvatarSize = new Size((int)poster.Element("AvatarSize").Attribute("Width"), (int)poster.Element("AvatarSize").Attribute("Height"));
            AvatarPosition = new Point((int)poster.Element("AvatarPosition").Attribute("X"), (int)poster.Element("AvatarPosition").Attribute("Y"));
            UsernamePosition = new Point((int)poster.Element("UsernamePosition").Attribute("X"), (int)poster.Element("UsernamePosition").Attribute("Y"));
        }
        private static T GetEnumValue<T>(XElement element, string attributeName) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), (string)element.Attribute(attributeName));
        }
    }
 }

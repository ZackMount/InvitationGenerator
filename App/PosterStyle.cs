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
            Font = (string)poster.Element("Font").Attribute("value");
            FontSize = (int)poster.Element("FontSize").Attribute("value");
            FontColor = (string)poster.Element("FontColor").Attribute("value");
            Poster = (string)poster.Element("Poster").Attribute("value");
            Gravity = GetEnumValue<Gravity>(poster.Element("Gravity"), "value");
            AvatarStyle = GetEnumValue<AvatarStyleType>(poster.Element("AvatarStyle"), "value");
            

            string[] avatarSize = ((string)poster.Element("AvatarSize").Attribute("value")).Split('x');
            AvatarSize = new Size(int.Parse(avatarSize[0]), int.Parse(avatarSize[1]));

            string[] avatarPosition = ((string)poster.Element("AvatarPosition").Attribute("value")).Split(',');
            AvatarPosition = new Point(int.Parse(avatarPosition[0]), int.Parse(avatarPosition[1]));

            string[] usernamePosition = ((string)poster.Element("UsernamePosition").Attribute("value")).Split(',');
            UsernamePosition = new Point(int.Parse(usernamePosition[0]), int.Parse(usernamePosition[1]));
        }
        private static T GetEnumValue<T>(XElement element, string attributeName) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), (string)element.Attribute(attributeName));
        }
    }
 }

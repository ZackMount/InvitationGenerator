using ImageMagick;
using Invitation_Generator;

namespace NSSCTF.InvitationGenerator
{
    public class Poster
    {
        private string Username;

        private string AvatarPath;
        private byte[] Avatar;

        private PosterStyle posterStyle;
        public Poster(PosterStyle posterStyle, string Username, string AvatarPath)
        {
            this.posterStyle = posterStyle;
            this.Username = Username;
            this.Avatar = posterStyle.AvatarStyle switch
            {
                AvatarStyleType.Cirlce => FixAvatar(AvatarPath, Resource.T_circle),
                AvatarStyleType.Fillet => FixAvatar(AvatarPath, Resource.T_fillet),
                _ => File.ReadAllBytes(AvatarPath),
            };
        }
        /// <summary>
        /// 生成图片，格式为Jpg
        /// </summary>
        /// <returns></returns>
        public Stream Generate()
        {
            if (posterStyle is null) throw new ArgumentNullException("Poster Style");
            var Result = new MemoryStream();
            using (var Image = new MagickImage(posterStyle.Poster))
            {
                var drawUsername = new Drawables()
                    .Font(posterStyle.Font)
                    .FontPointSize(posterStyle.FontSize)
                    .FillColor(new MagickColor(posterStyle.FontColor))
                    .Gravity((ImageMagick.Gravity)posterStyle.Gravity)
                    .Text(posterStyle.UsernamePosition.X, posterStyle.UsernamePosition.Y, Username)
                    .Draw(Image);
                using (var drawAvatar = new MagickImage(Avatar))
                {
                    drawAvatar.Resize(new MagickGeometry(posterStyle.AvatarSize.Width, posterStyle.AvatarSize.Height));
                    Image.Composite(drawAvatar, posterStyle.AvatarPosition.X, posterStyle.AvatarPosition.Y, CompositeOperator.Over);
                }
                Image.Write(Result, MagickFormat.Jpg);
                Image.Dispose();
            }
            return Result;
        }
        /// <summary>
        /// 按样式裁切头像
        /// </summary>
        /// <param name="Avatar"></param>
        /// <param name="Template"></param>
        /// <returns></returns>
        private byte[] FixAvatar(string AvatarPath, byte[] Template)
        {
            var Result = new MemoryStream();
            using (var Image = new MagickImage(Template))
            {
                Image.Resize(new MagickGeometry(posterStyle.AvatarSize.Width, posterStyle.AvatarSize.Height));
                using (var drawAvatar = new MagickImage(AvatarPath))
                {
                    drawAvatar.Resize(new MagickGeometry(posterStyle.AvatarSize.Width, posterStyle.AvatarSize.Height));
                    Image.Composite(drawAvatar, (ImageMagick.Gravity)posterStyle.Gravity, 0, 0, CompositeOperator.Atop);
                }
                Image.Write(Result);
            }
            return Result.ToArray();
        }
    }
}

using ImageMagick;
using Invitation_Generator;

namespace NSSCTF.InvitationGenerator
{
    public class Poster
    {
        private string username;

        private byte[] avatar;

        private PosterStyle posterStyle;
        public Poster(PosterStyle posterStyle, string username, string avatarPath)
        {
            this.posterStyle = posterStyle;
            this.username = username;
            this.avatar = posterStyle.AvatarStyle switch
            {
                AvatarStyleType.Cirlce => FixAvatar(avatarPath, Resource.T_circle),
                AvatarStyleType.Fillet => FixAvatar(avatarPath, Resource.T_fillet),
                _ => File.ReadAllBytes(avatarPath),
            };
        }
        /// <summary>
        /// 生成图片，格式为Jpg
        /// </summary>
        /// <returns></returns>
        public Stream Generate()
        {
            if (posterStyle is null) throw new ArgumentNullException(nameof(posterStyle), "Poster Style is null.");

            var resultStream = new MemoryStream();

            using (var image = new MagickImage(posterStyle.Poster))
            {
                DrawUsernameOnImage(image);
                DrawAvatarOnImage(image);

                image.Write(resultStream, MagickFormat.Jpg);
            }

            resultStream.Position = 0; //将流位置重置为 0
            return resultStream;
        }
        /// <summary>
        /// 绘制用户名
        /// </summary>
        /// <param name="image"></param>
        private void DrawUsernameOnImage(MagickImage image)
        {
            new Drawables()
                .Font(posterStyle.Font)
                .FontPointSize(posterStyle.FontSize)
                .FillColor(new MagickColor(posterStyle.FontColor))
                .Gravity((ImageMagick.Gravity)posterStyle.Gravity)
                .Text(posterStyle.UsernamePosition.X, posterStyle.UsernamePosition.Y, username)
                .Draw(image);
        }
        /// <summary>
        /// 绘制头像
        /// </summary>
        /// <param name="image"></param>
        private void DrawAvatarOnImage(MagickImage image)
        {
            using (var drawAvatar = new MagickImage(avatar))
            {
                drawAvatar.Resize(new MagickGeometry(posterStyle.AvatarSize.Width, posterStyle.AvatarSize.Height));
                image.Composite(drawAvatar, posterStyle.AvatarPosition.X, posterStyle.AvatarPosition.Y, CompositeOperator.Over);
            }
        }

        /// <summary>
        /// Resizes and crops the avatar according to the style.
        /// </summary>
        /// <param name="avatarPath">The path of the avatar image file.</param>
        /// <param name="template">The template image as a byte array.</param>
        /// <returns>The resized and cropped avatar as a byte array.</returns>
        private byte[] FixAvatar(string avatarPath, byte[] template)
        {
            using var templateImage = new MagickImage(template);
            using var avatarImage = new MagickImage(avatarPath);
            using var resultStream = new MemoryStream();

            ResizeAndCropAvatar(templateImage, avatarImage);

            templateImage.Write(resultStream);

            return resultStream.ToArray();
        }

        private void ResizeAndCropAvatar(MagickImage templateImage, MagickImage avatarImage)
        {
            var avatarSize = new MagickGeometry(posterStyle.AvatarSize.Width, posterStyle.AvatarSize.Height);

            templateImage.Resize(avatarSize);
            avatarImage.Resize(avatarSize);

            templateImage.Composite(avatarImage, ImageMagick.Gravity.Center, 0, 0, CompositeOperator.Atop);
        }

    }
}

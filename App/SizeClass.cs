namespace NSSCTF.InvitationGenerator
{
    public class Size
    {
        /// <summary>
        /// 宽
        /// </summary>
        public int Width {  get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int Height { get; set; }
        public Size(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
        public Size() 
        {
            Width = 0;
            Height = 0;
        }
    }
}

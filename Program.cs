using NSSCTF.InvitationGenerator;


//使用样例


#region 生成一个图片
var posterStyle = new PosterStyle("style.xml");

var poster = new Poster(posterStyle, "NSSCTF", "avatar.jpg");

var stream = poster.Generate();

Utils.SaveStreamToFile("out.jpg", stream);
#endregion


#region 创建一个样式并保存

var ps = new PosterStyle();

ps.Poster = "poster.jpg";
ps.AvatarPosition = new Point(0, 0);
ps.AvatarSize = new Size(128, 128);
ps.AvatarStyle = AvatarStyleType.Square;
ps.Font = "Arial"; //使用已安装的字体或指定字体文件路径
ps.FontColor = "white"; //使用颜色名称或RGB值(#000000)
ps.FontSize = 32;
ps.Gravity = Gravity.Center; //名字重心，不应用于头像
ps.UsernamePosition = new Point(0, 0); //重心在Center,坐标(0,0)，绘制在正中心

Utils.SaveStyle(ps, "newstyle.xml");

#endregion
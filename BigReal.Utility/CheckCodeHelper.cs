using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;

namespace BigReal.Utility
{
    public class CheckCodeHelper
    {
        public static string GenerateCheckCode()
        {
            int number;
            char code;
            var checkCode = new StringBuilder();

            var random = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode.Append(code);

            }

            return checkCode.ToString();
        }

        public static byte[] CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return null;

            var image = new Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            var g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                var random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                int x1, x2, y1, y2;
                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    x1 = random.Next(image.Width);
                    x2 = random.Next(image.Width);
                    y1 = random.Next(image.Height);
                    y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                var font = new Font("Arial", 12, (FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout));
                var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                int x, y;
                for (int i = 0; i < 100; i++)
                {
                    x = random.Next(image.Width);
                    y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                var ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

                return ms.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}

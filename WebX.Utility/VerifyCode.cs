using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace WebX.Utility
{
    public class VerifyCode
    {   
        /// <summary>
        /// 生成验证码string
        /// </summary>
        /// <param name="vCodeLen">验证码长度</param>
        /// <returns></returns>
        private static string RndCodeString(int vCodeLen)
        {
            string Vchar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            List<string> VcList = null;
            int Vlen =0;
            string code = "";
            Random rand = new Random();
            for (int i = 0; i < vCodeLen; i++)
            {   
                // 当vCodeLen过长,开始新的取值；
                if (Vlen == 0)
                {   
                    VcList = new List<string>(Vchar.Split(','));
                    Vlen = VcList.Count;
                }
                int t = rand.Next(Vlen);
                code += VcList[t];  // 获取该位置元素值到code;
                VcList.RemoveAt(t); // 移除该位置元素;
                Vlen--;
            }
            return code;
        }

        /// <summary>
        /// 获取随机颜色 最低rgb(10,10,10)
        /// </summary>
        /// <param name="lowvalue">最低值(0-255)</param>
        /// <param name="highvalue">最高值(0-255)</param>
        /// <returns></returns>
        public static Color GetrandomColor(int lowvalue,int highvalue)
        {   
            Random random = new Random();
            int nRed, nGreen, nBlue;
            int low = lowvalue;
            int high = highvalue;
            nRed = random.Next(low,high);
            nGreen = random.Next(low,high);
            nBlue = random.Next(low,high);
            Color color = Color.FromArgb(nRed, nGreen, nBlue);
            return color;
        }

        /// <summary>
        /// 获取验证码图像流
        /// </summary>
        /// <param name="code">输出验证码值</param>
        /// <param name="codelen">验证码长度，默认为4</param>
        /// <returns></returns>
        public static MemoryStream CreatePic(out string code, int codelen = 4)
        {
            if (codelen > 10)
                codelen = 10;
            code = RndCodeString(codelen);
          
            MemoryStream ms = new MemoryStream();
            Random random = new Random();
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };

            using (Bitmap img = new Bitmap((int)code.Length * 23, 28))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);
                    //画入code
                    for (int i = 0; i < code.Length; i++)
                    {
                        int findx = random.Next(5);
                        int fontSize = random.Next(15, 18);
                        Font drawFont = new Font(fonts[findx], fontSize, FontStyle.Bold);
                        SolidBrush solidBrush = new SolidBrush((GetrandomColor(10, 150)));
                        int charH = random.Next(2,5);
                        g.DrawString(code.Substring(i, 1), drawFont, solidBrush, 3 + (i *20), charH);

                    }
                    //添加干扰线
                    for (int i = 0; i < 10; i++)
                    {
                        int x1 = random.Next(img.Width);
                        int y1 = random.Next(img.Height);
                        int x2 = random.Next(img.Width);
                        int y2 = random.Next(img.Height);
                        g.DrawLine (new Pen(GetrandomColor(150, 255)), x1, y1, x2, y2);
                    }
                    // 添加干扰点
                    for (int i = 0; i < 50; i++)
                    {
                        int x = random.Next(img.Width);
                        int y = random.Next(img.Height);
                        img.SetPixel(x, y, Color.FromArgb(random.Next()));
                    }
                    g.DrawRectangle(new Pen(Color.White), 0, 0, img.Width, img.Height);
                    g.DrawRectangle(new Pen(Color.White), -1, -1, img.Width, img.Height);
                    img.Save(ms, ImageFormat.Jpeg);
                }
            }
            return ms;
        }
    }
}

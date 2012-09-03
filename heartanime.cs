using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace SortDesktopIcon
{
    class heartanime
    {
        public static int count = 0;
        const double pi = 3.1415926;
        static double t = 0;
        IcoObj ico;
        int num;
        double myt;
        static int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        static int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        double scale = getscale();
        Point center = getCenterPoint();

        public heartanime(IcoObj vico,int i)
        {
            ico = vico;
            num = i;
            
        }

        Point calPoint()
        {
            
            myt = t + num * 2 * pi / count;
            double r = scale *( Math.Sin(myt) * Math.Sqrt(Math.Abs(Math.Cos(myt))) / (Math.Sin(myt) + 7.0 / 3) - 2 * Math.Sin(myt) + 2);
            double x = r * Math.Cos(myt) + center.X;
            double y = -r * Math.Sin(myt) + center.Y;
            return new Point((int)x, (int)y);
            
        }
        public static  double nextT()
        {
            t += 2 * pi / 30;
            return t;
        }

        public void move()
        {
            Point newPoint = calPoint();
            WinAPI.SendMessage(ico.itemIntPtr, WinAPI.LVM_SETITEMPOSITION, num, WinAPI.MAKELPARAM(newPoint.X, newPoint.Y));
        }

        static double getscale()
        {
            
            double scale = height * 0.7 / 4;
            return scale;
        }

        static Point getCenterPoint()
        {
            int centerX = width / 2;
            int centerY =(int)( height * 0.15);
            return new Point(centerX, centerY);

        }

    }
}

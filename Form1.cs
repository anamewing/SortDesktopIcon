using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace SortDesktopIcon
{
    public partial class Form1 : Form
    {
        IList<IcoObj> icoObj;
        IDictionary<string, IcoObj> icoDics;
        DesktopIcon myDesktopIcon;
        public Form1()
        {
            InitializeComponent();
            myDesktopIcon = new DesktopIcon();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //IList<Point> pointArr=new List<Point>();
            icoObj = myDesktopIcon.getICO();
            
            configfile myconfig = new configfile(icoObj);
            myconfig.writefile();
            //int i= 0;
            //pointArr.Add(new Point(200, 200));
            //WinAPI.SendMessage(icoObj[i].itemIntPtr, WinAPI.LVM_SETITEMPOSITION, i, WinAPI.MAKELPARAM(pointArr[i].X, pointArr[i].Y));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            icoObj = myDesktopIcon.getICO();
            configfile myconfig = new configfile(icoObj);
            icoDics = myconfig.readfile();
            for (int i=0;i<icoObj.Count;i++)
            {
                IcoObj ico=icoObj[i];
                if (icoDics.ContainsKey(ico.name))
                {
                    Point newPoint = icoDics[ico.name].itemPoint;
                    WinAPI.SendMessage(ico.itemIntPtr, WinAPI.LVM_SETITEMPOSITION, i, WinAPI.MAKELPARAM(newPoint.X, newPoint.Y));
                    icoDics.Remove(ico.name);

                }
            }
        }

        
    }
}

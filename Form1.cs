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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<IcoObj> icoObj;
            IList<Point> pointArr=new List<Point>();
            DesktopIcon myDesktopIcon=new DesktopIcon();
            icoObj = myDesktopIcon.getICO();
            int i= 0;
            pointArr.Add(new Point(200, 200));
            WinAPI.SendMessage(icoObj[i].itemIntPtr, WinAPI.LVM_SETITEMPOSITION, i, WinAPI.MAKELPARAM(pointArr[i].X, pointArr[i].Y));
        }

        
    }
}

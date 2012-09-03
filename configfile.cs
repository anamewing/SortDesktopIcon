using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace SortDesktopIcon
{
    class configfile
    {
        string filename = "config.txt";
        IList<IcoObj> icoObjs;


        public configfile(IList<IcoObj> vicoObj)
        {
            icoObjs = vicoObj;

        }

        public bool writefile()
        {
            bool succeed = true;
            try
            {
                StreamWriter sw1 = new StreamWriter(filename);
                foreach (IcoObj ico in icoObjs)
                {
                    string line = ico.name + "/" + ico.itemPoint.X.ToString() + "/" + ico.itemPoint.Y.ToString();
                    sw1.WriteLine(line);
                }
                sw1.Close();
            }
            catch
            {
                succeed = false;
            }
            return succeed;
        }

        public IDictionary<string, IcoObj> readfile()
        {
            IDictionary<string, IcoObj> icoDics = new Dictionary<string, IcoObj>();
            //bool succeed = true;
            StreamReader sr1 = new StreamReader(filename);
            while (sr1.Peek() >= 0)
            {
                string line = sr1.ReadLine();
                string[] paras = line.Split('/');
                if (paras.Length != 3)
                {
                    Trace.WriteLine(line);
                    continue;
                }
                IcoObj ico = new IcoObj(paras[0], new IntPtr(), new Point(Convert.ToInt32(paras[1]), Convert.ToInt32(paras[2])));
                try
                {
                    icoDics.Add(paras[0], ico);
                }
                catch
                {
                    icoDics.Add(paras[0] + "?", ico);
                }


            }
            sr1.Close();
            return icoDics;
        }



    }
}
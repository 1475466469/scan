using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL
{
  public  class Log
    {
        public void Wirtefile(string str)
        {
            try
            {


                FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "\\log.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now + ":" + str);
                sw.Close();
                fs.Close();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}

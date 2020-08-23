using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KadcoMain.Services
{
    public class SageLogger
    {
        public static void TraceService(string content)
        {

            //set up a filestream
            
            FileStream fs = new FileStream(@"D:\PROJECTS\CODE\WORK\KADCO\log.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //D:\PROJECTS\WORK\KADCO

            //set up a streamwriter for adding text
            StreamWriter sw = new StreamWriter(fs);

            //find the end of the underlying filestream
            sw.BaseStream.Seek(0, SeekOrigin.End);

            //add the text
            sw.WriteLine(content);
            //add the text to the underlying filestream

            sw.Flush();
            //close the writer
            sw.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace ModuleBuddiesASP.HelperClasses
{
    public class XmlResponse
    {
       
        public string getXmlResponse(string url)
        {
            return xmlResponse(url);
        }
        private string xmlResponse(string url)
        {
            string xml = "";

            // Create a new WebRequest Object to the mentioned URL.
            WebRequest myWebRequest = WebRequest.Create(url);


            // Set the 'Timeout' property in Milliseconds.
            myWebRequest.Timeout = 100000;

            // This request will throw a WebException if it reaches the timeout limit before it is able to fetch the resource.
            WebResponse myWebResponse = myWebRequest.GetResponse();

            // Obtain a 'Stream' object associated with the response object.
            Stream ReceiveStream = myWebResponse.GetResponseStream();

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

            // Pipe the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(ReceiveStream, encode);
            //Console.WriteLine("\nResponse stream received");
            Char[] read = new Char[256];

            // Read 256 charcters at a time.     
            int count = readStream.Read(read, 0, 256);
            //Console.WriteLine("HTML...\r\n");

            while (count > 0)
            {
                // Dump the 256 characters on a string and display the string onto the console.
                String str = new String(read, 0, count);
                //Console.Write(str);
                xml += str;
                count = readStream.Read(read, 0, 256);
            }


            //Console.WriteLine("");
            // Release the resources of stream object.
            readStream.Close();

            // Release the resources of response object.
            myWebResponse.Close();

            return xml;
        }
      
    }
}
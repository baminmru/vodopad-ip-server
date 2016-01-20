using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ROBUSTELDebugService
{
    class Program
    {

        static String GetMyDir()
        {
            String s;
            s = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            s = s.Substring(6);
            return s;
        }

        static void Main(string[] args)
        {

            XmlDocument xml;
            xml = new XmlDocument();
            xml.Load(GetMyDir() + "\\ROBUSTELServiceCFG.xml");
            XmlElement node;
            node = (XmlElement)xml.LastChild;
            String sAddr;
            short nPort;

            try
            {
                sAddr = node.Attributes.GetNamedItem("serveraddress").Value;
            }
            catch (System.Exception ex)
            {
                sAddr = "127.0.0.1";
            }

            try
            {
                nPort = Convert.ToInt16(node.Attributes.GetNamedItem("serverport").Value);
            }
            catch (System.Exception ex)
            {
                nPort = 2060;
            }


            Console.WriteLine("Start server on " + sAddr + ":" + nPort.ToString() );
            ROBUSTELServer.TCPServer srv = new ROBUSTELServer.TCPServer(sAddr, nPort);
            srv.StartServer();

       
            while (true)
            {


                if (!srv.IsLive())
                {
                    srv.StopServer();
                    System.Threading.Thread.Sleep(2000); 
                    srv = new ROBUSTELServer.TCPServer(sAddr, nPort);
                    srv.StartServer();
                }

             
                System.Threading.Thread.Sleep(30000);
                
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using STKTVMain;

namespace ATMDebugService
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
            xml.Load(GetMyDir() + "\\ATMServiceCFG.xml");
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
            ATMServer.TCPServer srv = new ATMServer.TCPServer(sAddr, nPort);
            

            //STKTVMain.TVMain tvmain = new STKTVMain.TVMain();
            //tvmain.Init();
            //Console.WriteLine("DBTime=" + tvmain.GetDBDateTime().ToString() ); 


            srv.StartServer();

       
            while (true)
            {


                if (!srv.IsLive())
                {
                    srv.StopServer();
                    System.Threading.Thread.Sleep(2000); 
                    srv = new ATMServer.TCPServer(sAddr, nPort);
                    srv.StartServer();
                }

             
                System.Threading.Thread.Sleep(30000);
                
            }

        }
    }
}

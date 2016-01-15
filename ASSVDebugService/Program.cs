using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ASSVDebugService
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
            xml.Load(GetMyDir() + "\\ASSVServiceCFG.xml");
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
            ASSVServerLib.TCPServer srv = new ASSVServerLib.TCPServer(sAddr, nPort);
            srv.StartServer();

            ASSVCallBack.Caller caller = null;
            try
            {
                caller = new ASSVCallBack.Caller();
            }
            catch (System.Exception ex)
            {
            }
            while (true)
            {


                if (!srv.IsLive())
                {
                    srv.StopServer();
                    System.Threading.Thread.Sleep(2000); 
                    srv = new ASSVServerLib.TCPServer(sAddr, nPort);
                    srv.StartServer();
                }

                if (caller != null)
                {
                    try
                    {
                        caller.ScanASSV();
                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine("Error as ASSVCallback ... " + Ex.Message);

                    }
                }
                System.Threading.Thread.Sleep(30000);
                
            }

        }
    }
}

using System.Collections.Generic;
using System.Text;

namespace STKService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

          

            STKService Srv = new STKService();
         
            Srv.StartMe();
            while (1 != 0)
            {
                System.Threading.Thread.Sleep(100);  
            }
            Srv.StopMe();
        }
    }
}
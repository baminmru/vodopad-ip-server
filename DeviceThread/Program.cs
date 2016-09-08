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

            // The Length property provides the number of array elements
            //System.Console.WriteLine("parameter count = {0}", args.Length);
            int DevID = 0;
            bool NextD=false ;
            for (int i = 0; i < args.Length; i++)
            {
                //System.Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);
                if (NextD==true){
                    NextD=false;
                    DevID = System.Convert.ToInt32(args[i]);
                }

                if (args[i].ToLower() == "-d")
                {
                    NextD = true;
                }
            }

            if (DevID == 0) return;
             
            DeviceProcessor Srv = new DeviceProcessor();
            Srv.DivID = DevID;
            Srv.Run();
            Srv = null; 
        }
    }
}
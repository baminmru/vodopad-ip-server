using System;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;


namespace ROBUSTELServer
{
    /// <summary>
    /// Summary description for TCPSocketListener.
    /// </summary>
    public class TCPSocketListener
    {

        //private string LogPath = "c:\\log\\frttlog_xr_";

        private void LogString(string s)
        {
            try
            {

             //   File.AppendAllText(LogPath + DateTime.Now.ToString("yyyy_MM_dd") + ".txt", DateTime.Now.ToString() + ":XR: " + s + "\r\n");
                Console.WriteLine(s);
            }
            catch (System.Exception)
            {
            }
        }

     
        /// <summary>
        /// Variables that are accessed by other classes indirectly.
        /// </summary>
        protected Socket m_clientSocket = null;
        private bool m_stopClient = false;
        private Thread m_clientListenerThread = null;
        private bool m_markedForDeletion = false;
        private ROBUSTELDeviceProcessor Srv = null;


      

        /// <summary>
        /// Client Socket Listener Constructor.
        /// </summary>
        /// <param name="clientSocket"></param>
        public  TCPSocketListener(Socket clientSocket)
        {
            m_clientSocket = clientSocket;
        }

        /// <summary>
        /// Client SocketListener Destructor.
        /// </summary>
        ~TCPSocketListener()
        {
            StopSocketListener();
        }

        /// <summary>
        /// Method that starts SocketListener Thread.
        /// </summary>
        public void StartSocketListener()
        {
            if (m_clientSocket != null)
            {
                LogString("Start Device processor");

                if (m_clientListenerThread == null)
                {
                    m_clientListenerThread = new Thread(new ThreadStart(ClientThread));
                    m_clientListenerThread.SetApartmentState(ApartmentState.MTA);
                    m_clientListenerThread.Name = "One device processor";
                }
          
                m_clientListenerThread.Start();

            }
        }

        private void ClientThread()
        {
            Srv = new ROBUSTELDeviceProcessor();
            Srv.Run(m_clientSocket);
            m_markedForDeletion = true;
        }

     
        /// <summary>
        /// Method that stops Client SocketListening Thread.
        /// </summary>
        public void StopSocketListener()
        {
            if (m_clientSocket != null)
            {
                
                m_stopClient = true;
                try
                {
                    m_clientSocket.Close();
                }
                catch(System.Exception ex)
                {
                    //LogString(ex.Message);
                }
                if (m_clientListenerThread !=null)
                {
                    // Wait for one second for the the thread to stop.
                    m_clientListenerThread.Join(1000);

                    // If still alive; Get rid of the thread.
                    if (m_clientListenerThread.IsAlive)
                    {
                        m_clientListenerThread.Abort();
                    }
                }
                m_clientListenerThread = null;
                m_clientSocket = null;
                m_markedForDeletion = true;
            }
        }

        /// <summary>
        /// Method that returns the state of this object i.e. whether this
        /// object is marked for deletion or not.
        /// </summary>
        /// <returns></returns>
        public bool IsMarkedForDeletion()
        {
            return m_markedForDeletion;
        }

      

 
    }
}

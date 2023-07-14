using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace cliente
{
    /// <summary>
    /// Clase que define el estandar para envio y recepcion de informacion por medio de los sockets
    /// </summary>
    public class StateObject
    {
        /// <value>Size of receive buffer.</value>  
        public const int BufferSize = 1024;

        /// <value>Receive buffer.</value>  
        public byte[] buffer = new byte[BufferSize];
        /// <value>Received data string</value>  
        public String sb = "";
        /// <value>Client socket.</value>  
        public Socket? workSocket = null;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace cliente
{

    /// <summary>
    /// Clase cliente socket asincrono
    /// </summary>
    public class AsynchronousClient
    {

        // The port number for the remote device.  
        private const int port = 11000;

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =  new ManualResetEvent(false);
        private static ManualResetEvent sendDone =  new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =  new ManualResetEvent(false);

        // The response from the remote device.  
        private static String response = String.Empty;

        private static void StartClient()
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // The name of the local is defined by Dns.GetHostName "same ip as pc user"  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,new AsyncCallback(ConnectCallback), client);

                connectDone.WaitOne();


                //init a backthread that is always listening data from the server
                 Thread th = new Thread(() => Receive(client));
                   th.Start();
                //  Receive(client);

                // Send test data to the remote device. 
                bool exit = false;
                string consoleMessage = "";
                do{
                    Console.WriteLine("Escriba la peticion al servidor");

                    consoleMessage = Console.ReadLine()!;
                    
                    if (consoleMessage == "exit")
                    {
                        //end the connection
                        Send(client, "This is a test<EOF>");
                        //breaks the loop
                        exit = true;
                    }
                    else
                    {
                        
                        Send(client, consoleMessage);

                        sendDone.WaitOne();

                        consoleMessage = "";

                 
                        
                    }

                } while (!exit);
                
            

                Console.WriteLine("llegare aqui?");
                // Receive the response from the remote device.  
               // Receive(client);
               // receiveDone.WaitOne();

                // Write the response to the console.  
                Console.WriteLine("Response received : {0}", response);

                // Release the socket.   <- not using now, socket in building


              //  client.Shutdown(SocketShutdown.Both);
               // client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        //this runs in the backgrround thread
        private static void Receive(Socket client)
        {           
           
                try
                {
                // Create the state object.  
                    StateObject state = new StateObject();
                    state.workSocket = client;

                    // Begin receiving the data from the remote device.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                   // state = null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                receiveDone.WaitOne();

        }


        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);
                    response = state.sb.ToString();
                    Console.WriteLine("-> " + response);
                    // Get the rest of the data.  
                    
                    
                    //creamos una nueva via abierta para seguir oyendo , por siempre
                      client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                    receiveDone.Set();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        //enviamos la info al servidor
        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,new AsyncCallback(SendCallback), client);
        }


        //
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static  void Main(String[] args)
        {
            Console.WriteLine("*************");
            StartClient();
            Console.ReadLine();
            //return 0;

        }
    }
}

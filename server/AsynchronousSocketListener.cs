﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using web_client;
using Newtonsoft.Json;
using web_client.Models;

namespace server
{

    /// <summary>
    /// Clase socket servidor asincrono
    /// </summary>
    public class AsynchronousSocketListener
    {

        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private static WebApiClient API_RH = new WebApiClient();

        public AsynchronousSocketListener()
        {


        }



        public static void StartListening()
        {
          
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket ServerSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                ServerSocket.Bind(localEndPoint);
                ServerSocket.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Server is Waiting for a connection...");

                    ServerSocket.BeginAccept(new AsyncCallback(AcceptCallback), ServerSocket);

                    

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listeners = (Socket)ar.AsyncState;

            Socket handler = listeners.EndAccept(ar);

            StateObject state = new StateObject();

            state.workSocket = handler;

            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);

          //  Thread th = new Thread(() => Receive(handler));
          //  th.Start();

        }


        public static async void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;

            Socket handler = state.workSocket;
            
                // Read data from the client socket.
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.sb = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);

                    // Check for end-of-file tag. If it is not there, read
                    // more data.  
                    content = state.sb.ToString();
                // if (content.IndexOf("-") > -1)
                //  {
                // All the data has been read from the
                // client. Display it on the console.  
               


                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    // Echo the data back to the client.  



               // Task<string> response = await 
                var response = GetPaises().Result;

               // Console.WriteLine("---+   "+ response);



                    Send(handler, response);

                //creamos un loop indirecto para seguir recibiendo
                 handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                /*   }
                   else
                   {
                       // Not all data received. Get more.  

                   }*/

            }
            
        }

        private static void Send(Socket handler, String data)
        {
           
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

              //  handler.Shutdown(SocketShutdown.Both);
               // handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        private static async Task<string> GetPaises()
        {
            PaisesResponseModel? paises = await API_RH.GetPaises();


            return JsonConvert.SerializeObject(paises, Formatting.Indented);
        }








        public static int Main(String[] args)
        {
            StartListening();
            return 0;
        }


    }


}

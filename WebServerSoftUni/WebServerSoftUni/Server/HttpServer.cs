using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServerSoftUni.HTTP;
using WebServerSoftUni.Routing;

namespace WebServerSoftUni.Server
{
    public class HttpServer
    {
        private readonly IPAddress iPAddress;
        private readonly int port;
        private readonly TcpListener serverListener;
        private readonly RoutingTable routingTable;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.iPAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.serverListener = new TcpListener(this.iPAddress, port);

            routingTableConfiguration(this.routingTable = new RoutingTable());
        }
        public HttpServer(int port, Action<IRoutingTable> routingTable)
            : this("127.0.0.1", port, routingTable)
        {

        }
        public HttpServer(Action<IRoutingTable> routingTable)
            : this(8080, routingTable)
        {

        }
        public async Task Start()
        {

            serverListener.Start();

            Console.WriteLine($"Server started on port {port}");
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();
                _ = Task.Run(async () =>
                {
                    var networkStream = connection.GetStream();
                    string requestText = await ReadRequest(networkStream);
                    Console.WriteLine(requestText);
                    var request = Request.Parse(requestText);
                    var response = this.routingTable.MatchRequest(request);
                    if (response.PreRenderAction != null)
                    {
                        response.PreRenderAction(request, response);
                    }

                    await WriteResponse(networkStream, response);
                    connection.Close();
                });

            }

        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            byte[] buffer = new byte[1024];
            StringBuilder requets = new StringBuilder();
            int totalBytes = 0;
            do
            {
                int bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);
                totalBytes += bytesRead;
                if (totalBytes > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large");
                }
                requets.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            } while (networkStream.DataAvailable);
            return requets.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream, Response response)
        {
            var responseByte = Encoding.UTF8.GetBytes(response.ToString());
            await networkStream.WriteAsync(responseByte);
        }
    }
}
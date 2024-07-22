using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Client
{
    static async Task Main()
    {
        UdpClient udpClient = new UdpClient();
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);

        while (true)
        {
            Console.WriteLine("Enter the component name (e.g., processor, memory, harddisk) or 'exit' to quit:");
            string userInput = Console.ReadLine();
            if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            byte[] messageBytes = Encoding.ASCII.GetBytes(userInput);
            await udpClient.SendAsync(messageBytes, messageBytes.Length, serverEndPoint);

            var result = await udpClient.ReceiveAsync();
            string responseMessage = Encoding.ASCII.GetString(result.Buffer);

            Console.WriteLine($"Server response: {responseMessage}");
        }

        udpClient.Close();
    }
}

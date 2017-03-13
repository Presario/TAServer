using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RabbitX
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string sendstring = "";
			string ipString ;
			int port = 8888;
			int buflen = 1024;
			byte[] response_buffer = new byte[buflen];

			Console.Write ("Server IP:");
			ipString = Console.ReadLine ();
			ipString = ipString == "" ? "127.0.0.1" : ipString;

			TcpClient clinet = new TcpClient();
			clinet.Connect (new IPEndPoint (IPAddress.Parse (ipString), port));

			Console.WriteLine (">> Starting connect ..." + ipString + ":" + port.ToString());

			NetworkStream _nstream = clinet.GetStream ();
			while ((sendstring=Console.ReadLine())!="*") {
				byte[] buffer = 
					System.Text.UnicodeEncoding.Unicode.GetBytes (sendstring);
				_nstream.Write (buffer, 0, buffer.Length);

				if (_nstream.DataAvailable) {
					int len = _nstream.Read (response_buffer, 0, response_buffer.Length);
					if (len > 0) {
						Console.WriteLine(">> "+
							System.Text.UnicodeEncoding.Unicode.GetString(response_buffer).Trim());
					}
				}

			}
		}
	}
}

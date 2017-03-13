using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RabbitHome
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.Write ("IP Bind:");
			string ipaddress = Console.ReadLine ();
			TServer server = new TServer (ipaddress,8888);
			server.Start ();

		}
	}
}

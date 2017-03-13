using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RabbitHome
{
	public class TServer
	{
		TcpListener _tcplistener;
		TcpClient _tcpclinet;

		public TServer (string ipString,int port)
		{
			_tcplistener = new TcpListener (IPAddress.Parse(ipString),port);
		}


		public void Start()
		{
			_tcplistener.Start ();
			int i = 1;
			while (true) 
			{
				_tcpclinet = _tcplistener.AcceptTcpClient ();
				var work = new DoJobs ();
				ThreadPool.QueueUserWorkItem (work.ThreadCallBack, _tcpclinet);
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine (">> 客户端{0:D5}连接", i++);
				Console.ForegroundColor = ConsoleColor.Gray;
			}
		}

	}
}


using System.Net;
using System.Net.Sockets;
using System.Threading;
using Rabbit;

namespace RabbitHome
{
	public class TServer
	{
		TcpListener _tcplistener;
		ThreadTcpClinet _clinet;

		public TServer (string ipString,int port)
		{
			_tcplistener = new TcpListener(IPAddress.Parse(ipString), port);

		}

		public void Start()
		{
			int i = 1;

			Toos.Msg("服务启动监听 @ {0} .\n", System.ConsoleColor.Green, _tcplistener.LocalEndpoint.ToString());
			_tcplistener.Start ();

			while (true) 
			{
				_clinet = new ThreadTcpClinet();
				_clinet.ThreadId = i;
				_clinet.TcpClinet = _tcplistener.AcceptTcpClient();

				TJobs work = new TJobs ();
				ThreadPool.QueueUserWorkItem (work.ThreadCallBack, _clinet);

				Toos.Msg_Warn("客户端({0:D5}) @ {1} 连接［OK］\n", i++,
				              _clinet.TcpClinet.Client.RemoteEndPoint.ToString());
			}
		}

	}

	public class ThreadTcpClinet
	{
		public TcpClient TcpClinet { get; set; }

		public int ThreadId { get; set; }
	}
}


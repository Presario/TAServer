using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Rabbit;

namespace RabbitHome
{
	public class TServer
	{
		TcpListener _tcplistener;
		XClinet _clinet;

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
				_clinet = new XClinet();
				_clinet.ThreadId = i;
				_clinet.TcpClinet = _tcplistener.AcceptTcpClient();

				DoJobs work = new DoJobs ();
				ThreadPool.QueueUserWorkItem (work.ThreadCallBack, _clinet);
				Toos.Msg_Warn("客户端:{0:D5}[IP {1}]连接\n", i++,
				              _clinet.TcpClinet.Client.RemoteEndPoint.ToString());

			}
		}

	}

	public class XClinet
	{
		public TcpClient TcpClinet
		{
			get;
			set;
		}

		public int ThreadId
		{
			get;
			set;
		}
	}
}


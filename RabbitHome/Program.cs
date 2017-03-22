using System;
using Rabbit;

namespace RabbitHome
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			int _port = 8888;
			string _serverip;
			int minWorkerThreads, maxWorkerThreads;
			int minCompletionThreads, maxCompletionThreads;

			System.Threading.ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionThreads);

			// Get the maximum number of completion threads
			System.Threading.ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionThreads);
			// Set the new max worker threads, but keep the old max completion threads
			//ThreadPool.SetMaxThreads(someDifferentValue, maxCompletionThreads);

			Toos.Msg_Message("本服务器默认提供最小线程数：{0:D5};最大线程数：{1:D5}\n", minWorkerThreads, maxWorkerThreads);
			if (args.Length > 0)
			{
				Toos.Msg_Message("*本服务器IP：{0}\n", args[0]);
				_serverip = args[0];
			}
			else
			{
				Toos.Msg_Message("本服务器IP：{0}\n", Toos.GetLocalIP());
				Toos.Msg_Message("输入IP地址或者直接回车使用默认地址：");

				_serverip = Console.ReadLine();
				if (_serverip == "")
				{
					_serverip = Toos.GetLocalIP();
				}
			}


			TServer server = new TServer(_serverip, _port);
			server.Start();

		}
	}
}

using System;
using Rabbit;

namespace RabbitHome
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int _port = 8888;

			//Toos.Msg(
			//	new TSqlAdapter().getInsertString()
			//);

			Toos.Msg_Message("本服务器IP：{0}\n  请按回车确认或者输入指定需要绑定的IP地址：", Toos.GetLocalIP());
			string _serverip = Console.ReadLine();
			if (_serverip=="")
			{
				_serverip = Toos.GetLocalIP();
			}

			TServer server = new TServer (_serverip,_port);
			server.Start ();

		}
	}
}

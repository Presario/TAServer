using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Rabbit;

namespace RabbitX
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string ipString ;
			int port = 8888;
			TPackage _package;
			IFormatter _formater = new BinaryFormatter();
			NetworkStream _networkstream;

			Random rand = new Random();

			IPAddress ipaddress=new IPAddress(0);
			if (args.Length > 0 && IPAddress.TryParse(args[0], out ipaddress))
			{
				ipString = args[0];
				Toos.Msg_Message("远程服务器地址:{0}",ipString);
			}
			else
			{
				Toos.Msg_Message("请输远程服务器地址:");
				ipString = Console.ReadLine();
			}

			TcpClient clinet = new TcpClient();
			clinet.Connect (new IPEndPoint (IPAddress.Parse (ipString), port));
			Toos.Msg_Message("发送超时：{0:D}；接受超时{1:D}", clinet.SendTimeout, clinet.ReceiveTimeout);

			_networkstream = clinet.GetStream();

			Toos.Msg_Error("正在连接{0}:{1} ...\n",ipString,port);
			Toos.Msg_Message("程序运行中按任意键退出:D\n");

		

			while (!Console.KeyAvailable)
			{
				_package = new TPackage()
				{
					NickFrom = "发送:" + rand.Next(),
					NickTo = "接收:" + rand.Next(),
					Text = "数据:" + rand.Next(),
					QQTo = rand.Next().ToString(),
					QQFrom = rand.Next().ToString(),
					DTime = DateTime.Now,
					MagicId = 1
				};
				System.Threading.Thread.Sleep(500);

				try
				{
					_formater.Serialize(_networkstream, _package);
					Toos.Msg_Message("{0}=>{1}#{2};{3};{4}\n",
					                 Toos.GetLocalIP(),
					                 ipString, _package.NickFrom, _package.NickTo, _package.Text);
				}
				catch (Exception ex)
				{
					Toos.Msg_Error("错误:{0}\n",ex.Message);
				}
			}

			//发送魔术包！
			_package = new TPackage()
			{
				NickFrom = "准备" + rand.Next(),
				NickTo = "结束" + rand.Next(),
				Text = "通讯" + rand.Next(),
				DTime = DateTime.Now,
				MagicId = 0
			};

			_formater.Serialize(_networkstream, _package);

			_networkstream.Close();
			clinet.Close();
		}
	}
}

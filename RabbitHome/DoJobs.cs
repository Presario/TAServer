using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Rabbit;

namespace RabbitHome
{
	public class DoJobs
	{

		NetworkStream _networkstream;
		XClinet _clinet;
		IFormatter _formatter;
		XPackage _package;

		public void ThreadCallBack(Object obj)
		{
			_clinet = obj as XClinet;
			_networkstream = _clinet.TcpClinet.GetStream();
			_formatter = new BinaryFormatter();

			bool running = true;

			try
			{

				while (running)
				{
					_package = (XPackage)_formatter.Deserialize(
						_networkstream);

					if (_package.MagicId == 0)
					{
						running = false;
						Toos.Msg_Warn("客户端{0:D5}[{1}]请求结束会话\n",
									  _clinet.ThreadId, _clinet.TcpClinet.Client.RemoteEndPoint.ToString());
						continue;
					}

					Toos.Msg_Message("获得数据{0}=>{1}:{2},{3},{4} ...\n",
									_clinet.TcpClinet.Client.RemoteEndPoint.ToString(),
									 Toos.GetLocalIP(), _package.From, _package.To, _package.Text);
				}

			}
			catch (Exception ex)
			{
				Toos.Msg_Alert("错误:{0}\n", ex.StackTrace);
			}
			finally
			{

				Toos.Msg_Warn("线程{0:D5},远程{1} 结束\n",
							  _clinet.ThreadId, _clinet.TcpClinet.Client.RemoteEndPoint.ToString());
				_networkstream.Close();
			}

		}


	}

}


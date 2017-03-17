using System;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Rabbit;

namespace RabbitHome
{
	public class TJobs
	{

		NetworkStream _networkstream;
		ThreadTcpClinet _clinet;
		IFormatter _formatter;
		TPackage _package;

		public void ThreadCallBack(Object obj)
		{
			_clinet = obj as ThreadTcpClinet;
			_networkstream = _clinet.TcpClinet.GetStream();
			_formatter = new BinaryFormatter();

			bool running = true;

			TSqlAdapter SqlAdapter = new TSqlAdapter();

			try
			{

				while (running)
				{
					_package = (TPackage)_formatter.Deserialize(
						_networkstream);

					if (_package.MagicId == 0)
					{
						running = false;
						Toos.Msg_Warn("客户端{0:D5}[{1}]请求结束会话\n",
									  _clinet.ThreadId, _clinet.TcpClinet.Client.RemoteEndPoint.ToString());
						break;
					}

					SqlAdapter.Save(_package);

					Toos.Msg_Message("{0}=>{1}#{2};{3};{4}\n",
									_clinet.TcpClinet.Client.RemoteEndPoint.ToString(),
									 Toos.GetLocalIP(), _package.NickFrom, _package.NickTo, _package.Text);
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


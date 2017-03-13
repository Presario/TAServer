using System;
using System.Net;
using System.Net.Sockets;

namespace RabbitHome
{
	public class DoJobs
	{
		int bufLen = 1024;
		byte[] buffer;
		NetworkStream _networkstream;

		public DoJobs ()
		{
			buffer = new byte[bufLen];
		}

		public void ThreadCallBack(Object obj)
		{
			int readbytes=0,totalbytes=0;
			TcpClient client = obj as TcpClient;
			_networkstream = client.GetStream ();

			bool running = true;

			try {

				while (running) {
					if ((readbytes = _networkstream.Read (buffer, 0, buffer.Length)) > 0) {

						totalbytes += readbytes;
						string text = GetString (buffer, readbytes);
						if (text!="") {
							Console.WriteLine (">> " + text);
						}

						byte[] write_buffer = System.Text.UnicodeEncoding.Unicode.GetBytes (
							string.Format ("＊服务器接收到数据:{0:D8}字节", totalbytes));
						_networkstream.Write (write_buffer, 0, write_buffer.Length);
						_networkstream.Flush ();
					}
				}

			}catch(System.IO.IOException ex){
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine (">>> "+ex.Message+",MAYBE THE CLINET IS DOWN!");
				Console.ForegroundColor = ConsoleColor.Gray;
			}
			catch (Exception ex) {
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.WriteLine (">>> "+ex.Message);
				Console.ForegroundColor = ConsoleColor.Gray;
			}
			finally{
				_networkstream.Close ();
			}

		}

		String GetString(byte[] buffer, int readbytes)
		{
			string returnstring = "";

			try {
				System.Text.UnicodeEncoding UnicodeEn = new System.Text.UnicodeEncoding ();
				returnstring = UnicodeEn.GetString (buffer, 0, readbytes);				
			} catch (Exception ex) {
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine (">>> "+ex.Message);
				Console.ForegroundColor = ConsoleColor.Gray;
			}
			return returnstring;
		}


	}

}


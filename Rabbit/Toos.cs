using System;
using System.Net;

namespace Rabbit
{
	public static class Toos
	{
		public static void Msg(string format, ConsoleColor color = ConsoleColor.Gray, params object[] paramters)
		{
			Console.ForegroundColor = color;
			Console.Write(format, paramters);
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static void Msg_Message(string format, params object[] paramters)
		{
			Msg(">> " + format, ConsoleColor.DarkGray, paramters);
		}

		public static void Msg_Warn(string format, params object[] paramters)
		{
			Msg(">> " + format, ConsoleColor.Yellow, paramters);
		}

		public static void Msg_Alert(string format, params object[] paramters)
		{
			Msg(">> " + format, ConsoleColor.Red, paramters);
		}

		public static string GetLocalIP()
		{
			return Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
		}

	}
}

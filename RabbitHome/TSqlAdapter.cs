using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using Rabbit;

namespace RabbitHome
{
	public class TSqlAdapter
	{
		string _connectString;
		SqlConnection _connection;
		SqlCommand _command;

		public TSqlAdapter(string connectionName = "DefaultConnection")
		{
			_connectString =
					ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();
			_connection = new SqlConnection(_connectString);

			_command = new SqlCommand(
				getInsertString(),
				_connection);

			IDictionary<string, string> dict = getParameterList();
			foreach (KeyValuePair<string, string> item in dict)
			{
				if (getTypeSize(item.Value) > 0)
				{
					_command.Parameters.Add(item.Key,
											(SqlDbType)Enum.Parse(typeof(SqlDbType), getTypeString(item.Value), true),
											getTypeSize(item.Value));
				}
				else
				{
					_command.Parameters.Add(item.Key,
											(SqlDbType)Enum.Parse(typeof(SqlDbType), getTypeString(item.Value), true));
				}
				//Toos.Msg_Message("'{0}' => '{1}' => {2}\n", item.Key, item.Value, getTypeSize(item.Value));
			}

		}

		public string getInsertString()
		{
			string a = ConfigurationManager.AppSettings["SQL_INSERT"];
			int start = 0, at = 0;
			int c1, c2;

			while (true)
			{
				at = a.IndexOf('@', start);
				start = at + 1;

				if (at == -1) break;
				c1 = a.IndexOf(',', start);
				c2 = a.IndexOf(',', c1+1);

				if (c1 > start && c2 > c1)
					a = a.Remove(c1, c2 - c1 + 1);
			}

			return a;
		}

		public IDictionary<string, string> getParameterList()
		{
			IDictionary<string, string> dict = new Dictionary<string, string>();

			string a = ConfigurationManager.AppSettings["SQL_INSERT"];

			int start = 0, at = 0;
			int c1, c2;

			while (true)
			{
				at = a.IndexOf('@', start);
				start = at + 1;

				if (at == -1) break;
				c1 = a.IndexOf(',', start);
				c2 = a.IndexOf(',', c1 + 1);
				if (c2 == -1)
				{
					c2 = a.IndexOf(')', c1);
				}
				string key = a.Substring(at, c1 - start + 1).Trim();
				string val = a.Substring(c1 + 1, c2 - c1 - 1).Trim();

				dict.Add(key, val);
			}


			return dict;

		}

		private int getTypeSize(string typeString)
		{
			int i = typeString.IndexOf('(');
			if (i == -1)
			{
				return 0;
			}

			string a = typeString.Substring(i + 1, typeString.IndexOf(')', i) - i - 1);

			return Convert.ToInt32(a);

		}

		private string getTypeString(string typeString)
		{
			int i = typeString.IndexOf('(');
			if (i == -1)
			{
				return typeString;
			}

			return typeString.Substring(0, i);
		}

		public void Save(TPackage package)
		{
			IDictionary<string, string> dict = getParameterList();
			foreach (KeyValuePair<string, string> item in dict)
			{
				PropertyInfo pinfo = package.GetType().GetProperty(item.Key.Substring(1));

				if(pinfo!=null)_command.Parameters[item.Key].Value = pinfo.GetValue(package,null);
			}
			try
			{
				if (_connection.State!= ConnectionState.Open)
				{
					_connection.Open();
				}

				_command.ExecuteNonQuery();
				_connection.Close();
			}
			catch (Exception ex)
			{
				Toos.Msg_Alert("错误:{0}",ex.Message);
			}

		}

	}
}

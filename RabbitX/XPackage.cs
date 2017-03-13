using System;

namespace RabbitX
{
	public class XPackage
	{
		public string From { get;set; }

		public string To { get;set; }

		public string Text { get;set;}

		public string Images { get; set;}

		public int ClinetId { get;set; }

		public XPackage (){ }

		public byte[] GetByteStream()
		{
			return null;
		}

		public void SetFromBytes(byte[] data)
		{

		}
	}
}


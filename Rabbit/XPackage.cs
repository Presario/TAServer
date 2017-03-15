using System;
namespace Rabbit
{
	[Serializable]
	public class XPackage
	{
		public string From { get; set; }

		public string To { get; set; }

		public string Text { get; set; }

		public int MagicId{ get;set; }

	}
}

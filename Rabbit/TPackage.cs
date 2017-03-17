using System;
namespace Rabbit
{
	[Serializable]
	public class TPackage
	{
		public string QQFrom { get; set; }

		public string QQTo { get; set; }

		public string NickFrom { get; set; }

		public string NickTo { get; set; }

		public string Text { get; set; }

		public int MagicId { get; set; }

		public DateTime DTime { get; set; }
	}
}

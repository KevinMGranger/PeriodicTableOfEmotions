using UnityEngine;
using UnityEngine.Events;

namespace Conversation
{
	/// <summary>
	/// A conversation option is a branch to be presented in a conversation tree.
	/// </summary>
	[System.Serializable]
	public class Option
	{
		public string text;
		public Point nextPoint;
		public UnityEvent whenChosen;

		public Option(string text, Point nextPoint, UnityEvent whenChosen)
		{
			this.text = text;
			this.nextPoint = nextPoint;
			this.whenChosen = whenChosen;
		}

		public Option(string text, Point nextPoint): this(text, nextPoint, new UnityEvent()) { }


		public override string ToString()
		{
			return text;
		}

		public static implicit operator string(Option co)
		{
			return co.ToString();
		}
	}
}
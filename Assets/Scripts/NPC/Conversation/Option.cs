using UnityEngine;
using UnityEngine.Events;

namespace Conversation
{

	public class OptionChosen : UnityEvent<Option> { }

	/// <summary>
	/// A conversation option is a branch to be presented in a conversation tree.
	/// </summary>
	[System.Serializable]
	public class Option
	{
		public string text;
		public Point nextPoint;
		public OptionChosen whenChosen;

		public Option(string text, Point nextPoint)
		{
			this.text = text;
			this.nextPoint = nextPoint;
		}

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
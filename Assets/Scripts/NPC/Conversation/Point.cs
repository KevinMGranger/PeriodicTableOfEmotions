using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Conversation
{
	/// <summary>
	/// A conversation point is a node in a conversation tree.
	/// It has some text to be presented in a window, and a dictionary (ResponseTree)
	/// That relates responses to the next conversation points.
	/// If the optionTree is null, that means this point is a "last word"
	/// and there's nothing more to the conversation.

	/// You can also attach events to be fired as soon as this is presented, or closed,
	/// but those must be kicked off from the ConversationManager.
	/// </summary>
	[System.Serializable]
	public class Point : IEnumerable<Option>
	{
		public string text;

		public Option[] options;

		public UnityEvent onPresentText;
		public UnityEvent onClosedText;

		public bool isLastWord
		{
			get
			{
				return options == null || options.Length == 0;
			}
		}
		public Point(string response, Option[] options, UnityEvent onPresentText, UnityEvent onClosedText)
		{
			this.text = response;
			this.options = options;
			this.onPresentText = onPresentText;
			this.onClosedText = onClosedText;
		}

		public Point(string response, Option[] options) : this(response, options, new UnityEvent(), new UnityEvent()) { }

		public Point(string response, Option option) : this(response, new Option[] { option }, new UnityEvent(), new UnityEvent()) { }

		public Point(string response) : this(response, (Option[])null) { }

		/// <summary>
		/// Get the next conversation point from the given response.
		/// </summary>
		/// <param name="response"></param>
		/// <returns>The next point or null if it couldn't be found.</returns>
		public Point getNextPointFromResponse(Option response)
		{
			foreach (var option in options)
			{
				if (option == response)
				{
					return option.nextPoint;
				}
			}

			return null;
		}

		/// <summary>
		/// Get the next conversation point from the given response.
		/// </summary>
		/// <param name="response"></param>
		/// <returns>The next point or null if it couldn't be found.</returns>
		public Point getNextPointFromResponse(string response)
		{
			foreach (var option in options)
			{
				if (option.text == response)
				{
					return option.nextPoint;
				}
			}

			return null;
		}
		/// <summary>
		/// Get the next conversation point from the given response.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>The next point or null if it couldn't be found.</returns>
		public Point this[Option value]
		{
			get
			{
				return getNextPointFromResponse(value);
			}
		}

		/// <summary>
		/// Get the next conversation point from the given response.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>The next point or null if it couldn't be found.</returns>
		public Point this[string value]
		{
			get
			{
				return getNextPointFromResponse(value);
			}
		}

		public override string ToString()
		{
			return text;
		}

		public static implicit operator string(Point cp)
		{
			return cp.ToString();
		}


		public IEnumerator<Option> GetEnumerator()
		{
			return ((IEnumerable<Option>)options).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return options.GetEnumerator();
		}
	}
}
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

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
public class ConversationPoint : IEnumerable<ConversationOption>
{
    public string text;

    public ConversationOption[] options;

	public UnityEvent onPresentText;
	public UnityEvent onClosedText;

    public bool isLastWord
    {
        get
        {
            return options == null || options.Length == 0;
        }
    }
    public ConversationPoint(string response, ConversationOption[] options, UnityEvent onPresentText, UnityEvent onClosedText)
    {
        this.text = response;
        this.options = options;
        this.onPresentText = onPresentText;
        this.onClosedText = onClosedText;
    }

    public ConversationPoint(string response, ConversationOption[] options) : this(response, options, new UnityEvent(), new UnityEvent()) { }

    public ConversationPoint(string response, ConversationOption option) : this(response, new ConversationOption[] {option}, new UnityEvent(), new UnityEvent()) { }

    public ConversationPoint(string response) : this(response, (ConversationOption[])null) { }

	/// <summary>
	/// Get the next conversation point from the given response.
	/// </summary>
	/// <param name="response"></param>
	/// <returns>The next point or null if it couldn't be found.</returns>
    public ConversationPoint getNextPointFromResponse(string response)
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
	public ConversationPoint this[string value]
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

	public static implicit operator string(ConversationPoint cp)
	{
		return cp.ToString();
	}


	public IEnumerator<ConversationOption> GetEnumerator()
	{
		return ((IEnumerable<ConversationOption>)options).GetEnumerator();
	}

	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
	{
		return options.GetEnumerator();
	}
}
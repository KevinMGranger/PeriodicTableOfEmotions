using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class ChoiceSelected : UnityEvent<string> { }

/// <summary>
/// A conversation point is a node in a conversation tree.
/// It has some text to be presented in a window, and a dictionary (ResponseTree)
/// That relates responses to the next conversation points.
/// If the optionTree is null, that means this point is a "last word"
/// and there's nothing more to the conversation.

/// You can also attach events to be fired as soon as this is presented, or closed,
/// but those must be kicked off from the ConversationManager.
/// </summary>
public class ConversationPoint
{
    public string spokenText;

    public ResponseTree optionTree;

	public UnityEvent onPresentText;
	public UnityEvent onClosedText;

    public bool isLastWord
    {
        get
        {
            return optionTree == null;
        }
    }
    public ConversationPoint(string response, ResponseTree optionTree, UnityEvent onPresentText, UnityEvent onClosedText)
    {
        this.spokenText = response;
        this.optionTree = optionTree;
        this.onPresentText = onPresentText;
        this.onClosedText = onClosedText;
    }

    public ConversationPoint(string response, ResponseTree optionTree) : this(response, optionTree, new UnityEvent(), new UnityEvent()) { }

    public ConversationPoint(string response) : this(response, null) { }

	/// <summary>
	/// Get the next conversation point based off of the given response.
	/// </summary>
	/// <param name="response"></param>
	/// <param name="nextPoint"></param>
	/// <returns>True if it was found successfully, false if not.</returns>
    public bool getNextPointFromResponse(string response, out ConversationPoint nextPoint)
    {
        if (optionTree != null)
        {
            return optionTree.TryGetValue(response, out nextPoint);
        }
        else
        {
            nextPoint = null;
            return false;
        }
    }

    public override string ToString()
    {
        return spokenText;
    }
}
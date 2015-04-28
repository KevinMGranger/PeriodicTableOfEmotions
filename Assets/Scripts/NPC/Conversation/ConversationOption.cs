using UnityEngine;

/// <summary>
/// A conversation option is a branch to be presented in a conversation tree.
/// </summary>
[System.Serializable]
public class ConversationOption
{
	public string text;
	public ConversationPoint nextPoint;

	public ConversationOption(string text, ConversationPoint nextPoint)
	{
		this.text = text;
		this.nextPoint = nextPoint;
	}

    public override string ToString()
    {
        return text;
    }

	public static implicit operator string(ConversationOption co)
	{
		return co.ToString();
	}
}

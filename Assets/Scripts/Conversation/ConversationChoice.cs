using UnityEngine;
using System.Collections;

public class ConversationChoice
{
    public string text;

    public ConversationChoice(string text)
    {
        this.text = text;
    }

    public static bool operator ==(ConversationChoice a, ConversationChoice b)
    {
        return a.text == b.text;
    }

    public static bool operator !=(ConversationChoice a, ConversationChoice b)
    {
        return a.text != b.text;
    }

    public override string ToString()
    {
        return text;
    }
}

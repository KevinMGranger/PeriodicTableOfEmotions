using UnityEngine;
using System.Collections;

public class ConversationResponse
{
    public string text;

    public ConversationResponse(string text)
    {
        this.text = text;
    }

    public static bool operator ==(ConversationResponse a, ConversationResponse b)
    {
        return a.text == b.text;
    }

    public static bool operator !=(ConversationResponse a, ConversationResponse b)
    {
        return a.text != b.text;
    }

    public override string ToString()
    {
        return text;
    }
}

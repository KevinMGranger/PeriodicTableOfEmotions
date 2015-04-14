using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public class ConversationChoiceSelected : UnityEvent<ConversationChoice> { }

public class ConversationPoint
{
    public ConversationResponse response;

    public IDictionary<ConversationChoice, ConversationPoint> optionTree;

    public bool isLastWord
    {
        get
        {
            return optionTree == null;
        }
    }

    public ConversationPoint(ConversationResponse response, Dictionary<ConversationChoice, ConversationPoint> optionTree)
    {
        this.response = response;
        this.optionTree = optionTree;
    }

    public ConversationPoint(string response, Dictionary<ConversationChoice, ConversationPoint> optionTree)
        : this(new ConversationResponse(response), optionTree) { }

    public bool getNextPointFromResponse(ConversationChoice response, out ConversationPoint nextPoint)
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
        return response.ToString();
    }
}
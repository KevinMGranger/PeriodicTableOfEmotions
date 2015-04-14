using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using ResponseTree = System.Collections.Generic.Dictionary<ConversationChoice, ConversationPoint>;

public class ConversationManager : MonoBehaviour {

    public RectTransform buttonMenu;

    public ResponseButton choiceButtonProto;

    public DialogBox box;

    public bool demoMode;

    [ContextMenuItem("Populate with example conversation", "PopulateExampleConvo")]
    public ConversationPoint conversationTree;

    void Awake()
    {
        if (demoMode)
        {
            PopulateExampleConvo();
        }
    }

	// Use this for initialization
	void Start () {
	}

    public void StartConvo()
    {
        box.gameObject.SetActive(true);
        box.Active = true;
        buttonMenu.gameObject.SetActive(true);
        setUI(conversationTree);
    }

    public void EndConvo()
    {
        box.Active = false;
        clearDialogChildren();

        box.gameObject.SetActive(false);
        buttonMenu.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void setUI(ConversationPoint newPoint)
    {
        box.Text = newPoint.ToString();

        clearDialogChildren();

        if (!newPoint.isLastWord)
        foreach (var choice in newPoint.optionTree.Keys)
        {
            var button = Instantiate(choiceButtonProto) as ResponseButton;
            button.Response = choice;
            button.Manager = this;

            button.gameObject.transform.SetParent(buttonMenu, false);
        }
    }

    void clearDialogChildren()
    {
        foreach (Transform potato in buttonMenu.gameObject.transform)
        {
            Destroy(potato.gameObject);
        }
    }

    void PopulateExampleConvo()
    {
        conversationTree = new ConversationPoint(
            "Is my electron kawaiiiiii~~~~~",
            new ResponseTree {
                { new ConversationChoice("Of course!"), new LastWord("Yay!") },
                { new ConversationChoice("...no."), new LastWord("Aww. You're mean.") }
            });
    }

    public void ConversationChoiceSelected(ConversationChoice choice)
    {
        ConversationPoint next;
        var found = conversationTree.getNextPointFromResponse(choice, out next);

        if (found)
        {
            conversationTree = next;
            box.Active = false;
            setUI(next);
            box.Active = true;
        }
        else
        {
            box.Active = false;
        }
    }
}
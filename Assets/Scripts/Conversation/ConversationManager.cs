using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A conversation manager hooks up a conversation tree
/// to its respective UI components (a RectTransform to hold responses,
/// and a DialogBox to hold the text.)
/// Those should probably be split out a bit more but WHATEVS LOLS
/// </summary>
public class ConversationManager : MonoBehaviour {

    public RectTransform buttonMenu;

    public ResponseButton choiceButtonProto;

    public DialogBox box;

    public string chosen;

	// this is to kick off PopulateExampleConvo from the UI
	// since you can't show a dictionary in the inspector easily
    public bool demoMode;

    [ContextMenuItem("Populate with example conversation", "PopulateExampleConvo")]
    public ConversationPoint conversationTree;

    public string GetText()
    {
        return chosen;
    }

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
		conversationTree.onClosedText.Invoke();

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
		conversationTree.onClosedText.Invoke();

        box.Text = newPoint.ToString();

		conversationTree.onPresentText.Invoke();

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
                { "Of course!", new ConversationPoint("Yay!") },
                { "...no.", new ConversationPoint("Aww. You're mean.") }
            });
    }

    public void ChoiceSelected(string choice)
    {
        chosen = choice;
        ConversationPoint next;
        bool found = conversationTree.getNextPointFromResponse(choice, out next);
        //Debug.Log(next.optionTree.Keys);
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
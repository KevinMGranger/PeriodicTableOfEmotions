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

    public ConversationPoint conversationTree;

    public string GetText()
    {
        return chosen;
    }

    void Awake()
    {
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
        foreach (var choice in newPoint)
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

    public void ChoiceSelected(string choice)
    {
        chosen = choice;
        ConversationPoint next = conversationTree[choice];

        if (next != null)
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
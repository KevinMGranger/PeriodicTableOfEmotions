using UnityEngine;
using UnityEngine.Events;
using System.Collections;
// States of the NPC
public enum Sentiment
{
    NeverMet,
    Met,
    Trusting,
    Matched
}

public class TawkToMe : MonoBehaviour {

	public GameObject talkBubble;
	public ConversationPoint convo;
    public ConversationPoint convoTrust;
    public ConversationPoint convoWin;
	public ConversationManager convoManager;

    public MouseLook msLook;

    // Trust Points and Sentiment is directly tied. The state the NPC is in, is directly proportional to how much trust is between them. 
    public int trustPoints;
    public Sentiment state;

    // Name of the NPC
    public string npcName;

    // choice made during quiz
    public string choice;

	public SpeechBubbleChanger sbc;

	[ContextMenu("What input name do we ask the Input Manager about?")]
	public string talkInputName = "Talk";

    // Changes the NPC's sentimental feelings towards the player.
    public UnityEvent changeEnumStateMet;
    public UnityEvent changeEnumStateTrust;
    public UnityEvent changeEnumStateMatched;

    [HideInInspector]
    // Self explanatory, but basically does a check to see if dialogue is open. Used to avoid bugs when the dialogue has never been opened. 
    public bool isDialogueOpen = false;
    [HideInInspector]
    // Isn't really used here, but in other scripts they can reference this if they want to do something with it. 
    public bool isColliding = false;
    [HideInInspector]
    // whether or not a choice was correct from the quiz
    public bool correct;

	// Use this for initialization
	void Awake() {
        if(npcName == null)
        {
            npcName = "Generic";
        }
		/*
        convo = new ConversationPoint("Hey! I'm " + npcName,
            new ResponseTree {
                { "Hi, I'm Adom!", new ConversationPoint("So you want to try and match me right? Well here's the deal, I'll quiz you on chemistry. If you can get three correct answers, I'll give you more information on my nature. (Coming Soon)"
                    )}});
                //new ResponseTree { 
                //    {"Sure", new ConversationPoint("Okay cool! Here's your first question: Who is the father of modern Chemistry?",
                //          new ResponseTree{
                //            {"Queen Elizabeth II", new ConversationPoint("She's too busy ruling England")},
                //            {"Sir Francis Drake", new ConversationPoint("No, I think not")},
                //            {"Antoine Lavoisier", new ConversationPoint("He is widely considered the founder of Modern Chemistry. You're correct!",
                //          //})}})}});
                //              new ResponseTree{
                //                  {"Great!", new ConversationPoint("Okay now on to the real thing",
                //                      new ResponseTree{
                //                      {"Let's do this",new ConversationPoint("What is an atom composed of?",
                //                              new ResponseTree{ 
                //                              {"Bits, Bytes, and Gigabytes", new ConversationPoint("'Fraid not.")},
                //                              {"Potatoes. Lots of Potatoes", new ConversationPoint("No.")},
                //                              {"Protons, Electrons, and Neutrons", new ConversationPoint("That's Correct! Now what is the definition of chemistry?", 
                //                                      new ResponseTree{
                //                                      {"Romance!!!", new ConversationPoint("Yes!!! But also no!!")},
                //                                      {"Study of Matter", new ConversationPoint("Yeah, that's basically it!")},
                //                                      {"The study of kittens", new ConversationPoint ("I don't think we're on the same page here.")}

                    //})}})}});
                //    })}})}})}})}})}})}});
        convoTrust = new ConversationPoint("Hey Adom!",
              new ResponseTree {
                { "Sup" + npcName, new ConversationPoint("nm, u?")}
              });
		convoWin = new ConversationPoint ("Congratulations!!! You've matched all of the elements together!");
		*/
		convoManager = GameObject.Find("Conversation Manager").GetComponent<ConversationManager>();

        state = Sentiment.NeverMet;
        trustPoints = 0;
	}
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isDialogueOpen)
        {
            choice = convoManager.GetText();
        }
        CheckDialogue();
        // call the event in the update
        InvokeEvent();
	}


	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Player")
		{
			talkBubble.SetActive(true);
			isColliding = true;
		}
	}
	void OnTriggerStay(Collider col)
	{
		if(Input.GetButtonDown(talkInputName) && col.gameObject.name == "Player" && isDialogueOpen == false)
		{
            msLook.active = false;
            convoManager.StartConvo();
			isDialogueOpen = true;
		}
        // for testing purposes
        if(Input.GetButtonDown("RaiseTrust") && col.gameObject.name == "Player")
        {
            Debug.Log(trustPoints + 1);
            trustPoints += 1;
        }
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "Player") 
		{
			talkBubble.SetActive (false);
			isColliding = false;
		} 
		if (isDialogueOpen == true) 
		{
            msLook.active = true;
			convoManager.EndConvo();
			isDialogueOpen = false;
		}
	}
    /// <summary>
    /// A dialogue check to see if a decision should change the NPC's opinion of the player.
    /// </summary>
    public void CheckDialogue()
    {
        // if the choice is valid, raise trust Points.
        if (choice == "Hi, I'm Adom!" && correct == false)
        {
            //Debug.Log(" trust: " + (trustPoints + 1));
            correct = true;
        }
        //if (choice == "Sup" && correct == false)
        //{
        //    //Debug.Log(" trust: " + (trustPoints + 1));
        //    correct = true;
        //}

        // also set the conversation nmananger to null to keep it from infinitely looping.
        if (correct == true)
        {
            Debug.Log(npcName + " trust: " + (trustPoints + 1));
            trustPoints++;
            correct = !correct;
            convoManager.chosen = null;
        }
    }
    /// <summary>
    /// This class is meant to be invoked when trust points reach the correct value. 
    /// It will then allow the NPC to have different behaviors depending on it's enum state.
    /// </summary>
    public void SentimentChangeMet()
    {
        state = Sentiment.Met;
        Debug.Log(state);
    }
    public void SentimentChangeTrust()
    {
        state = Sentiment.Trusting;
        Debug.Log(state);
    }
    public void SentimentChangeMatched()
    {
        state = Sentiment.Matched;
    }

    public void InvokeEvent()
    {
        if (trustPoints >= 2)
        {
            changeEnumStateTrust.Invoke();
        }
        else if (trustPoints >= 1)
        {
            changeEnumStateMet.Invoke();
        }
    }
}

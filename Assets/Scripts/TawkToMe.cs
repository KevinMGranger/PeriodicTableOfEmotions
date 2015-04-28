using UnityEngine;
using System.Collections;
// States of the NPC
public enum NPCState
{
    Waiting,
    Trusting,
    Matched
}
public class TawkToMe : MonoBehaviour {

	public GameObject talkBubble;
	public ConversationPoint convo;
    public ConversationPoint convoTrust;
    public ConversationPoint convoWin;
	public ConversationManager convoManager;
	public bool isDialogueOpen = false;
	public bool isColliding = false;
    public MouseLook msLook;
    public int trustPoints;
    public NPCState state;

    // Name of the NPC
    public string npcName;

    // quiz variables
    public string choice;
    public bool correct;

	public SpeechBubbleChanger sbc;

	[ContextMenu("What input name do we ask the Input Manager about?")]
	public string talkInputName = "Talk";

	// Use this for initialization
	void Awake() {
        if(npcName == null)
        {
            npcName = "Generic";
        }
		convo = new ConversationPoint("Hey! I'm " + npcName,
			new ResponseTree {
                { "Hi, I'm Adom!", new ConversationPoint("So you want to try and match me right? Well here's the deal, I'll quiz you on chemistry. If you can get three correct answers, I'll give you more information on my nature.", 
				new ResponseTree{ 
					{"Sure!", new ConversationPoint("Okay cool! Here's your first question: Who is the father of modern Chemistry?",
						  new ResponseTree{
							{"Queen Elizabeth II", new ConversationPoint("She's too busy ruling England")},
							{"Sir Francis Drake", new ConversationPoint("No, I think not")},
							{"Antoine Lavoisier", new ConversationPoint("He is widely considered the founder of Modern Chemistry. You're correct!",
								new ResponseTree{
									{"Great!", new ConversationPoint("Okay now on to the real thing",
										new ResponseTree{
										{"Let's do this",new ConversationPoint("What is an atom composed of?",
											    new ResponseTree{ 
												{"Bits, Bytes, and Gigabytes", new ConversationPoint("'Fraid not.")},
												{"Potatoes. Lots of Potatoes", new ConversationPoint("No.")},
												{"Protons, Electrons, and Neutrons", new ConversationPoint("That's Correct! Now what is the definition of chemistry?", 
													    new ResponseTree{
														{"Romance!!!", new ConversationPoint("Yes!!! But also no!!")},
														{"Study of Matter", new ConversationPoint("Yeah, that's basically it!")},
														{"The study of kittens", new ConversationPoint ("I don't think we're on the same page here.")}

					//})}})}});
					})}})}})}})}})}})}});
        convoTrust = new ConversationPoint("Hey Adom!",
              new ResponseTree {
                { "Sup " + npcName, new ConversationPoint("nm, u?")}
              });
		convoWin = new ConversationPoint ("Congratulations!!! You've matched all of the elements together!");
		convoManager = GameObject.Find("Conversation Manager").GetComponent<ConversationManager>();

        state = NPCState.Waiting;
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
        if (trustPoints >= 1)
        {
            state = NPCState.Trusting;
        }
        if (state == NPCState.Waiting)
        {
            convoManager.conversationTree = convo;
        }
        else if (state == NPCState.Trusting)
        {
            convoManager.conversationTree = convoTrust;
        }
        //Debug.Log(state);
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
    public void CheckDialogue()
    {
        // if the choice is valid, raise trust Points.
        if (choice == "Hi, I'm Adom" && correct == false)
        {
            //Debug.Log(" trust: " + (trustPoints + 1));
            correct = true;
        }

        // also set the conversation nmananger to null to keep it from infinitely looping.
        if (correct == true)
        {
            Debug.Log(npcName + " trust: " + (trustPoints + 1));
            trustPoints++;
            correct = !correct;
            convoManager.chosen = null;
        }
    }
}

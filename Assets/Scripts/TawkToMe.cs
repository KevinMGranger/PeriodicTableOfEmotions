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
										{"Hi, I'm Adom", new ConversationPoint("Nice to meet you!") }
		});
        convoTrust = new ConversationPoint("Hi Adom. What's up?",
            new ResponseTree{
                {"Just saying hi!", new ConversationPoint("Well that's no fun")},
                {"Let me learn more about you",new ConversationPoint("You want to know more about me? Then you're going to have to beat my chemistry quiz! (Coming Soon)")}
            });
                    //new ResponseTree{
                    //    {"Okay", new ConversationPoint("Good! First question: What is Chemistry the study of?",
                    //        new ResponseTree{
                    //            {"Study of matter", new ConversationPoint("Correct!")},
                    //            {"Study of kittens", new ConversationPoint("Nope")},
                    //            {"Study of romance", new ConversationPoint("Well you'd be correct with anyone else")}
                    //        //})}})},
                    //        })}})}});
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

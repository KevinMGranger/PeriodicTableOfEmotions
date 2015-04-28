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
	// Get ZoneCheck's GameObject
	public ZoneCheck zone;
    public NPCState state;

    // Name of the NPC
    public string npcName;

    // quiz variables
    public string choice;
    public bool correct;

	// Use this for initialization
	void Awake() {
        if(npcName == null)
        {
            npcName = "Generic";
        }
		zone = gameObject.GetComponent<ZoneCheck>();
        convo = new ConversationPoint("Hey! I'm " + this.gameObject.GetComponent<TawkToMe>().npcName,
		                               new ResponseTree {
										{"Hi, I'm Adom", new ConversationPoint("Nice to meet you!") }
		});
        convoTrust = new ConversationPoint("Hi Adom. What's up?",
            new ResponseTree{
                {"Let me learn more about you",new ConversationPoint("You want to know more about me? Then you're going to have to beat my chemistry quiz!")},
                {"Just saying hi!", new ConversationPoint("Well that's no fun")}
            });
		convoManager = GameObject.Find("Conversation Manager").GetComponent<ConversationManager>();

        state = NPCState.Waiting;
        trustPoints = 0;
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (trustPoints >= 1)
        {
            convoManager.conversationTree = convoTrust;
        }
        choice = convoManager.GetText();
        CheckDialogue();

        if (state == NPCState.Waiting)
        {
            convoManager.conversationTree = convo;
        }
        else if (state == NPCState.Trusting)
        {

        }
        else
        {

        }
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
		if(Input.GetKeyDown(KeyCode.E) && col.gameObject.name == "Player" && isDialogueOpen == false)
		{
            msLook.active = false;
			convoManager.StartConvo ();
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
        // if the choice is freddy mercury, raise trust points
        if (choice == "Hi, I'm Adom" && correct == false)
        {
            //Debug.Log(" trust: " + (trustPoints + 1));
            correct = true;
        }

        // also set the conversation nmananger to null to keep it from infinitely looping.
        if (correct == true)
        {
            Debug.Log(" trust: " + (trustPoints + 1));
            this.gameObject.GetComponent<TawkToMe>().trustPoints++;
            correct = !correct;
            convoManager.chosen = null;
        }
    }
}

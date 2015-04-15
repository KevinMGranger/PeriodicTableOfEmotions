using UnityEngine;
using System.Collections;

public class AtomNPC : MonoBehaviour {

    // holds the charge of the NPC
    public int atomicCharge =  0;

	// The NPC's trust towards the player
	public int trustPoints = 0;

    // Get ZoneCheck's GameObject
    public ZoneCheck zone;

	// Name of the NPC
	public string npcName;

	// Attribute(s) of NPC
	public string attribute = "";

	// Is the player next to me?
	public bool playerAttention;

	// Is this atom matched?
	public bool isMatched;

	// Has this atom been added to the Match List?
	public bool inMatchList;

	// Attribute Checks
	public bool likesPudding;
	public bool likesVideoGames;

	// Instance of the player
	public Character player;

    // Conversation Manager
    public ConversationPoint convo;

    public ConversationManager convoManager;

	void Awake () {
        // find the script and allow me to check for collision
        //zone = GameObject.Find("InteractionZone").GetComponent<ZoneCheck>();
		zone = gameObject.transform.GetChild(0).GetComponent<ZoneCheck>();
        // player matching
		playerAttention = false;
		isMatched = false;
		inMatchList = false;

		if(likesPudding)
		{
			attribute += "Likes Pudding";
		}
		else
		{
			attribute += "Hates Pudding";
		}

		if(likesVideoGames)
		{
			attribute += ", Likes Video Games";
		}
		else
		{
			attribute += ", Hates Video Games";
		}

        convo = new ConversationPoint ("Hey! I'm " + name + "!\nHere's some facts about me:\n" + attribute,
            new ResponseTree {
                { "Cool story bro", new ConversationPoint("I don't think I trust you quite yet. Do you want to try to gain my trust?", 
				new ResponseTree{ 
					{"Sure!", new ConversationPoint("Okay cool! Here's your first question: Who is the father of Chemistry?",
						  new ResponseTree{
							{"Queen Elizabeth II", new ConversationPoint("She's too busy ruling England")},
							{"Freddy Mercury", new ConversationPoint("Yeah sure, let's go with that")},
							{"Sir Francis Drake", new ConversationPoint("No, I think not")}
					})}})}});

        convoManager = GameObject.Find("Conversation Manager").GetComponent<ConversationManager>();
	}

    void Start(){}
	
	// Update is called once per frame
	void Update () {
        // if the player is colliding with the zone, allow him to interact with the NPC
        if (zone.collides == true)
        {
			if(playerAttention == false)
			{
                convoManager.conversationTree = convo;
                convoManager.StartConvo();
				//Debug.Log(npcName + ":'Hello, Adom!'");
				if(inMatchList == false)
				{
					Debug.Log("Press F to add to Match List!");
				}
				playerAttention = true;
			}
			else
			{
            	//Debug.Log("Charge: " + atomicCharge);
            	if(Input.GetKeyDown(KeyCode.F) && inMatchList == false)
            	{
					inMatchList = true;
					player.AddToMatchList(this);
           	   	  	atomicCharge++;
           		}
			}
        }
        // if not, then don't do anything right now.
		else if(playerAttention == true)
        {
            convoManager.EndConvo();
            //Debug.Log(npcName + ": Goodbye!");
			playerAttention = false;
        }
	}

	// Check to see if the match this has been 
	// paired with is a good match
	// This will be more complex later on...
	public int CheckMatch(AtomNPC match)
	{
		// Calculate score for match here
		isMatched = true;
		int grade = 0;
		if(likesPudding == match.likesPudding)
		{
			grade++;
		}
		if(likesVideoGames == match.likesVideoGames)
		{
			grade++;
		}
		return grade;
	}
}

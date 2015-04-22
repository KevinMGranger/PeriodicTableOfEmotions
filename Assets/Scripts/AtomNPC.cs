using UnityEngine;
using System.Collections;

public enum NPCState
{
	Waiting,
	Trusting,
	Matched
}

public class AtomNPC : MonoBehaviour {

    // holds the charge of the NPC (Electron rotating number)
    public int atomicCharge =  0;

	// The NPC's trust towards the player
	public int trustPoints = 0;

    // Image Loader, brought in from the seperate script (Made no sense to have a seperate script to load all images atm)
    public GameObject npcImage;

	// Check Dialogue
	public bool isDialogueOpen;

    // Get ZoneCheck's GameObject
    public ZoneCheck zone;

    // Check Conversation cycle
    public bool changeConversation;

	// Name of the NPC
	public string npcName;

	// Attribute(s) of NPC
	public string attribute = "";

	// Is the player next to me?
	public bool playerAttention;

	// Is this atom matched?
	public bool isMatched;

	// Has this atom been added to the Match List?
	public bool readyToMatch;

	// Attribute Checks
	public bool likesPudding;
	public bool likesVideoGames;

	// Instance of the player
	public Character player;

    // A prompt for the player
    public GameObject GPrompt;
	public GameObject DPrompt;
    public GameObject EPrompt;

    // Conversation Manager
    public ConversationPoint convo;
    public ConversationPoint convoTrust;
	public ConversationPoint convoWin;


    public ConversationManager convoManager;

    // quiz variables
    public string choice;
	public bool correct;

	void Awake () {
        // find the script and allow me to check for collision
        //zone = GameObject.Find("InteractionZone").GetComponent<ZoneCheck>();
		zone = gameObject.transform.GetChild(0).GetComponent<ZoneCheck>();
        changeConversation = false;
        // player matching
		playerAttention = false;
		isMatched = false;
		readyToMatch = false;
		correct = false;

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

        convo = new ConversationPoint ("Hey! I'm " + npcName,
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
        convoTrust = new ConversationPoint("Hey Adom!! How's it going?",
              new ResponseTree {
                { "Hi " + npcName, new ConversationPoint("Since you passed my quiz, i'll just give you my information:\n " + attribute)}
              });
		convoWin = new ConversationPoint ("Congratulations!!! You've matched all of the elements together!");


        convoManager = GameObject.Find("Conversation Manager").GetComponent<ConversationManager>();

	}

    void Start(){}
	
	// Update is called once per frame
	void Update () {
		choice = convoManager.GetText();

		// check to see if any player left in list.
		if (player.AllMatch () == true) 
		{
			convoManager.conversationTree = convoWin;
			convoManager.StartConvo();
		}

        // if the player is colliding with the zone, allow him to interact with the NPC
        if (zone.collides == true && !isMatched)
        {
			if(player.tryGrab && !player.grabbing && readyToMatch)
			{
				this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
				player.grabbedNPC = this;
				player.tryGrab = false;
				player.grabbing = true;
				DPrompt.gameObject.SetActive(true);
			}
			if(player.grabbing != true)
			{
				DPrompt.gameObject.SetActive(false);
			}

			if(playerAttention == false)
			{
				//Debug.Log(npcName + ":'Hello, Adom!'");
				if(readyToMatch == false)
				{
					//Debug.Log("Press F to add to Match List!");
				}
				playerAttention = true;
			}
			else
			{
                EPrompt.gameObject.SetActive(true);
                if (trustPoints >= 3)
                {
                    EPrompt.gameObject.SetActive(false);
                    GPrompt.gameObject.SetActive(true);
					readyToMatch = true;
                }
                if (player.grabbing == true)
                {
                    GPrompt.gameObject.SetActive(false);
					EPrompt.gameObject.SetActive(false);
                }
                // handles quite literally the whole conversation system now.
            	//Debug.Log("Charge: " + atomicCharge);
            	if(Input.GetKeyDown(KeyCode.F) && readyToMatch == false)
            	{
					player.AddToMatchList(this);
           	   	  	atomicCharge++;
           		}
				if(Input.GetKeyDown(KeyCode.E))
				{
					isDialogueOpen = true;
                    if (trustPoints >= 3)
                    {
                        npcImage.gameObject.SetActive(true);
						convoManager.conversationTree = convoTrust;
                        convoManager.StartConvo();
                    }
                    else
                    {
                        npcImage.gameObject.SetActive(true);
                        convoManager.conversationTree = convo;
                        convoManager.StartConvo();
                    }
				}
				if(Input.GetKeyDown(KeyCode.Escape))
				{
					isDialogueOpen = false;
					convoManager.EndConvo();
					npcImage.gameObject.SetActive(false);
				}
			}
			CheckQuiz ();
        }
        // if not in zone, then don't do anything right now.
		else if(playerAttention == true)
        {
			// Should in theory set the prompts to inactive.
			GPrompt.gameObject.SetActive(false);
            EPrompt.gameObject.SetActive(false);
            npcImage.gameObject.SetActive(false);
			if(isDialogueOpen == true)
			{
            	convoManager.EndConvo();
				isDialogueOpen = false;
			}
            //Debug.Log(npcName + ": Goodbye!");
			playerAttention = false;
        }

		if(zone.hitsNPC && isMatched == false && readyToMatch && zone.collidedNPC.readyToMatch == true)
		{
			FormBond();
		}

		if(player.grabbedNPC == this)
		{
			FollowPlayer();
		}
		else
		{
			this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

	// Follow the player around
	private void FollowPlayer()
	{
		// Follow the player here
		if((Vector3.Distance(this.transform.position, player.GetGameObject().transform.position)) > 1.7f)
		{
			Quaternion startingRotation = transform.rotation;
			transform.LookAt(player.transform.position);
			transform.Translate(Vector3.forward * 0.1f);
			transform.rotation = startingRotation;
		}

	}

	private void FormBond()
	{
		isMatched = true;
		player.grabbedNPC = null;
		player.grabbing = false;
		this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(255.0f, 135.0f, 135.0f);
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
	public void CheckQuiz()
	{
		// if the choice is freddy mercury, raise trust points
		if (choice == "Antoine Lavoisier" && correct == false)
		{
			Debug.Log (this.gameObject.GetComponent<AtomNPC> ().npcName + " trust: " + (trustPoints + 1));
			correct = true;
		}
		if (choice == "Protons, Electrons, and Neutrons" && correct == false) 
		{
			Debug.Log (this.gameObject.GetComponent<AtomNPC> ().npcName + " trust: " + (trustPoints + 1));
			correct = true;
		}
		if (choice == "Study of Matter" && correct == false) 
		{
			Debug.Log (this.gameObject.GetComponent<AtomNPC> ().npcName + " trust: " + (trustPoints + 1));
			correct = true;
		}
		if (choice == "Bits, Bytes, and Gigabytes") 
		{
			Debug.Log (this.gameObject.GetComponent<AtomNPC> ().npcName + " trust: " + (trustPoints + 1));
			correct = false;
			trustPoints--;
			convoManager.chosen = null;
		}
		if (choice == "Potatoes. Lots of Potatoes")
		{
			Debug.Log (this.gameObject.GetComponent<AtomNPC> ().npcName + " trust: " + (trustPoints + 1));
			correct = false;
			trustPoints--;
			convoManager.chosen = null;
		}
		if (choice == "Romance!!!") 
		{
			Debug.Log (this.gameObject.GetComponent<AtomNPC> ().npcName + " trust: " + (trustPoints + 1));
			correct = false;
			trustPoints--;
			convoManager.chosen = null;
		}
		if (choice == "The study of kittens") 
		{
			Debug.Log (this.gameObject.GetComponent<AtomNPC> ().npcName + " trust: " + (trustPoints + 1));
			correct = false;
			trustPoints--;
			convoManager.chosen = null;
		}
		// also set the conversation nmananger to null to keep it from infinitely looping.
		if (correct == true) 
		{
			Debug.Log (this.gameObject.GetComponent<AtomNPC> ().npcName + " trust: " + (trustPoints + 1));
			this.gameObject.GetComponent<AtomNPC> ().trustPoints++;
			correct = !correct;
			convoManager.chosen = null;
		}
	}

}

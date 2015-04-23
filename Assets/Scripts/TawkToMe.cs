using UnityEngine;
using System.Collections;

public class TawkToMe : MonoBehaviour {

	public GameObject talkBubble;
	public ConversationPoint convo;
	public ConversationManager convoManager;
	public bool isDialogueOpen = false;
	// Get ZoneCheck's GameObject
	public ZoneCheck zone;
	// Use this for initialization
	void Start () {
		zone = gameObject.GetComponent<ZoneCheck>();
		convo = new ConversationPoint ("Hey! I'm Generic!",
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
		convoManager = GameObject.Find("Conversation Manager").GetComponent<ConversationManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Player")
		{
			talkBubble.SetActive(true);

		}
	}

	void OnTriggerStay(Collider col)
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			convoManager.conversationTree = convo;
			convoManager.StartConvo ();
			isDialogueOpen = true;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "Player") {
			talkBubble.SetActive (false);

		} 
		if (isDialogueOpen == true) 
		{
			convoManager.EndConvo();
			isDialogueOpen = false;
		}
	}
}

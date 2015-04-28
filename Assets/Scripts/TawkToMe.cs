using UnityEngine;
using System.Collections;

public class TawkToMe : MonoBehaviour {

	public GameObject talkBubble;
	public ConversationPoint convo;
	public ConversationManager convoManager;
	public bool isDialogueOpen = false;
	public bool isColliding = false;
    public MouseLook msLook;
	// Get ZoneCheck's GameObject
	public ZoneCheck zone;
	// Use this for initialization
	void Awake() {
		zone = gameObject.GetComponent<ZoneCheck>();
		convo = new ConversationPoint ("Hey! I'm generic!",
		                               new ResponseTree {
										{"Hi, I'm Adom", new ConversationPoint("Nice to meet you!") }
		});
		convoManager = GameObject.Find("Conversation Manager").GetComponent<ConversationManager>();
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
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
			convoManager.conversationTree = convo;
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
}

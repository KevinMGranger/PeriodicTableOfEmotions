using UnityEngine;
using System.Collections;

public class TawkToMe : MonoBehaviour {

	public GameObject talkBubble;

	public SpeechBubbleChanger sbc;

	[ContextMenu("What input name do we ask the Input Manager about?")]
	public string talkInputName = "Talk";

	// Use this for initialization
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
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (Input.GetButtonDown(talkInputName)) sbc.SetDemoAlternating();
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "Player")
		{
			talkBubble.SetActive(false);
		}
	}
}

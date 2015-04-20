using UnityEngine;
using System.Collections;

public class TawkToMe : MonoBehaviour {

	public GameObject talkBubble;

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
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == "Player")
		{
			talkBubble.SetActive(false);
		}
	}
}

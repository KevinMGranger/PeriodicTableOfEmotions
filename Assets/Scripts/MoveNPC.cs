using UnityEngine;
using System.Collections;

public class MoveNPC : MonoBehaviour {
	public Character player;
	public TawkToMe ttm;
	public bool isMoving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// check for input
		if (Input.GetKeyDown (KeyCode.F) && isMoving == false && ttm.isColliding == true) {
			isMoving = true;
		} else if (Input.GetKeyDown (KeyCode.G) && isMoving == true) {
			isMoving = false;
		}
		if (isMoving == true) {
			followPlayer ();
		}

	}
	void followPlayer()
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
}

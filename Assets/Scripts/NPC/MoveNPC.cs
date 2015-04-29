using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MoveNPC : MonoBehaviour {
	public Character player;
	public TawkToMe ttm;

    // a bool meant to control the event's invoke. 
    public bool isMoving = false;

    [ContextMenu("Is the object following the player?")]
    public UnityEvent followMe;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        InvokeFollow();
	}
    /// <summary>
    /// If the distance between the player and the gameObject is too large, transform the gameObject towards the player. 
    /// </summary>
	public void followPlayer()
	{
		// This object's parent is the player.
        this.transform.parent = player.gameObject.transform;
        /*
		if((Vector3.Distance(this.transform.position, player.gameObject.transform.position)) > 1.7f)
		{
			Quaternion startingRotation = transform.rotation;
			transform.LookAt(player.transform.position);
			transform.Translate(Vector3.forward * 0.1f);
			transform.rotation = startingRotation;
		}
         */
	}
    void InvokeFollow()
    {
        // If the F key is pressed, the player will carry the object
		if (Input.GetKeyDown(KeyCode.F) && isMoving == false && ttm.isColliding == true) {
			isMoving = true;
        }
        // if the G key is pressed, the player will release the object.
        else if (Input.GetKeyDown(KeyCode.G) && isMoving == true)
        {
			isMoving = false;
		}
        // This will invoke the event as long as the player pressed the F key. 
        if (isMoving == true)
        {
            followMe.Invoke();
        }
        // if the player releases the object, isMoving is then false so the object is allowed to stop. 
        else
        {
            this.transform.parent = null;
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
    /// <summary>
    /// Checks to see if a match was made between two atoms
    /// </summary>
    void checkMatch()
    {

    }
}

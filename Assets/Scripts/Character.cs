using UnityEngine;
using System.Collections;

// Tracks the player's current state
public enum PlayerState
{
	walking,
	matching,
	talking
}

// Tracks the player during matchmaking
public enum MatchingState
{
	notMatching,
	selection1,
	selection2
}

public class Character : MonoBehaviour
{
    // get the speed and acceleration of object
    public float maxSpeed = 10;
    public float speed = 5.0f;

	// List of possible NPCs to match
	public AtomNPC[] matchList;

	// Match Choice
	public AtomNPC grabbedNPC;

    // grab Object's positions.
    public float posX;
    public float posY;

	// Grabbing booleans
	public bool tryGrab;
	public bool grabbing;

	// States of the player
	public PlayerState playerState;
	public MatchingState matchingState;

    // Use this for initialization
    void Start()
    {
		playerState = PlayerState.walking;
		matchingState = MatchingState.notMatching;
    }

    // Update is called once per frame
    void Update()
    {
		if(tryGrab)
		{
			tryGrab = false;
		}
		// If the player state is walking...
		if(playerState == PlayerState.walking)
		{
        	Movement();

			// Enters the player into the Matching state
			if(Input.GetKeyDown(KeyCode.G))
			{
				if(grabbing)
				{
					grabbing = false;
					grabbedNPC = null;
				}
				else
				{
					tryGrab = true;
				}
				/*
				if(matchList[3] != null)
				{
					Debug.Log("Type the Number of the player you want to Match. Press G again to cancel.");
					for(int i = 0; i < matchList.Length; i++)
					{ 
						Debug.Log("" + (i+1) + ". " + matchList[i].npcName + ": " + matchList[i].attribute);
					}
					playerState = PlayerState.matching;
					matchingState = MatchingState.selection1;
				}
				else
				{
					Debug.Log("You haven't added everyone to your match list!");
				}
				*/
			}
		}

		// If the player state is matching...
		else if(playerState == PlayerState.matching)
		{
			// Call the Match method
			Match ();

			/*
			// If 'G' is pressed while matching, exit the matching process
			if(Input.GetKeyDown(KeyCode.G))
			{
				Debug.Log("Stopped Matching. Walk Around Now.");
				playerState = PlayerState.walking;
				matchingState = MatchingState.notMatching;
			}
			*/
		}
    }

    // Handles all of the movement. (Called in Update)
    private void Movement()
    {
        // get position.
        posX = Input.GetAxis("Horizontal");
        posY = Input.GetAxis("Vertical");

        // Set the vector
        Vector2 move = (new Vector2(posX, posY).normalized) * speed;
        //Vector2 slow = (new Vector2(posX, posY).normalized) * -speed;

        // Move the rigidbody, using the vector
        rigidbody2D.AddForce(move, ForceMode2D.Impulse);

        //if (move == Vector2.zero)
        //{
        //    rigidbody2D.velocity = slow;
        //}
        // if the speed of the object goes over the maxspeed, set the speed to maxspeed.
        if (Mathf.Abs(rigidbody2D.velocity.x) >= maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(maxSpeed * rigidbody.velocity.x, rigidbody.velocity.y);
        }
        if (Mathf.Abs(rigidbody2D.velocity.y) >= maxSpeed)
        {
            rigidbody2D.velocity = new Vector2(rigidbody.velocity.x, maxSpeed * rigidbody.velocity.y);
        }
    }

	// Adds an NPC to the match list
	public void AddToMatchList(AtomNPC atom)
	{
		for(int i = 0; i < matchList.Length; i++)
		{
			if(matchList[i] == null)
			{
				matchList[i] = atom;
				break;
			}
		}
	}
	public bool AllMatch()
	{
		int counter = 0;
		foreach (AtomNPC atom in matchList) 
		{
			if(atom.isMatched == true)
			{
				counter++;
			}
		}
		if (counter >= 4)
		{
			return true;
		}
		return false;

	}
	// Obsolete methods, here for now
	// This method matches two NPCs of the player's choice
	// It also gives them a score out of 4 on the match (To be flushed out later)
	private void Match()
	{
	}

	// Shows the player the next set of choices when matching
	private void ShowSecondMatchChoices(int initialChoice)
	{
	}

	public GameObject GetGameObject()
	{
		return gameObject;
	}
}
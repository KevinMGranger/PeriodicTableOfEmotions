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
        	//Movement();

			// Enters the player into the Matching state
			if(Input.GetKeyDown(KeyCode.G))
			{
				if(grabbing)
				{
					grabbing = false;
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
}
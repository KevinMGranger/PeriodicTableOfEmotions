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
	public AtomNPC firstMatchChoice;

    // grab Object's positions.
    public float posX;
    public float posY;

	// States of the player
	public PlayerState playerState;
	public MatchingState matchingState;

    // Use this for initialization
    void Start()
    {
		matchList = new AtomNPC[4];
		playerState = PlayerState.walking;
		matchingState = MatchingState.notMatching;
    }

    // Update is called once per frame
    void Update()
    {
		// If the player state is walking...
		if(playerState == PlayerState.walking)
		{
        	Movement();

			// Enters the player into the Matching state
			if(Input.GetKeyDown(KeyCode.G))
			{
				Debug.Log("Type the Number of the player you want to Match. Press G again to cancel.");
				for(int i = 0; i < matchList.Length; i++)
				{
					Debug.Log("" + (i+1) + ". " + matchList[i].npcName + ": " + matchList[i].attribute);
				}
				playerState = PlayerState.matching;
				matchingState = MatchingState.selection1;
			}
		}

		// If the player state is matching...
		else if(playerState == PlayerState.matching)
		{
			// Call the Match method
			Match ();

			// If 'G' is pressed while matching, exit the matching process
			if(Input.GetKeyDown(KeyCode.G))
			{
				Debug.Log("Stopped Matching. Walk Around Now.");
				playerState = PlayerState.walking;
				matchingState = MatchingState.notMatching;
			}
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

	// This method matches two NPCs of the player's choice
	// It also gives them a score out of 4 on the match (To be flushed out later)
	private void Match()
	{
		// First step in matching, choose the first matchee with the number keys
		if(matchingState == MatchingState.selection1)
		{
			if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
			{
				Debug.Log("Type the Number of the player you want to Match. Press G to cancel.");
				firstMatchChoice = matchList[0];
				ShowSecondMatchChoices (0);
				matchingState = MatchingState.selection2;
			}
			if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
			{
				Debug.Log("Type the Number of the player you want to Match. Press G to cancel.");
				firstMatchChoice = matchList[1];
				ShowSecondMatchChoices (1);
				matchingState = MatchingState.selection2;
			}
			if(Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
			{
				Debug.Log("Type the Number of the player you want to Match. Press G to cancel.");
				firstMatchChoice = matchList[2];
				ShowSecondMatchChoices (2);
				matchingState = MatchingState.selection2;
			}
			if(Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
			{
				Debug.Log("Type the Number of the player you want to Match. Press G to cancel.");
				firstMatchChoice = matchList[3];
				ShowSecondMatchChoices (3);
				matchingState = MatchingState.selection2;
			}
		}

		// Second part of matching, choose the second person
		else if(matchingState == MatchingState.selection2)
		{
			if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
			{
				// Call the choice Match Method
				int matchGrade1 = firstMatchChoice.CheckMatch(matchList[0]);
				int matchGrade2 = matchList[0].CheckMatch(firstMatchChoice);
				int matchGradeFinal = matchGrade1 + matchGrade2;
				Debug.Log("Your grade for matching " + firstMatchChoice.npcName + " and " + matchList[0].npcName + " is " + matchGradeFinal.ToString () + "/4");

				// Remove them from THE LIST
				playerState = PlayerState.walking;
				matchingState = MatchingState.notMatching;
			}
			if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
			{
				// Call the choice Match Method
				int matchGrade1 = firstMatchChoice.CheckMatch(matchList[1]);
				int matchGrade2 = matchList[1].CheckMatch(firstMatchChoice);
				int matchGradeFinal = matchGrade1 + matchGrade2;
				Debug.Log("Your grade for matching " + firstMatchChoice.npcName + " and " + matchList[1].npcName + " is " + matchGradeFinal.ToString () + "/4");

				// Remove them from THE LIST
				playerState = PlayerState.walking;
				matchingState = MatchingState.notMatching;
			}
			if(Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
			{
				// Call the choice Match Method
				int matchGrade1 = firstMatchChoice.CheckMatch(matchList[2]);
				int matchGrade2 = matchList[2].CheckMatch(firstMatchChoice);
				int matchGradeFinal = matchGrade1 + matchGrade2;
				Debug.Log("Your grade for matching " + firstMatchChoice.npcName + " and " + matchList[2].npcName + " is " + matchGradeFinal.ToString () + "/4");

				// Remove them from THE LIST
				playerState = PlayerState.walking;
				matchingState = MatchingState.notMatching;
			}
			if(Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
			{
				// Call the choice Match Method
				int matchGrade1 = firstMatchChoice.CheckMatch(matchList[3]);
				int matchGrade2 = matchList[3].CheckMatch(firstMatchChoice);
				int matchGradeFinal = matchGrade1 + matchGrade2;
				Debug.Log("Your grade for matching " + firstMatchChoice.npcName + " and " + matchList[3].npcName + " is " + matchGradeFinal.ToString () + "/4");

				// Remove them from THE LIST
				playerState = PlayerState.walking;
				matchingState = MatchingState.notMatching;
			}
		}
	}

	// Shows the player the next set of choices when matching
	private void ShowSecondMatchChoices(int initialChoice)
	{
		for(int i = 0; i < matchList.Length; i++)
		{
			if(i != initialChoice)
			{
				Debug.Log("" + (i+1) + ". " + matchList[i].npcName + ": " + matchList[i].attribute);
			}
		}
	}
}
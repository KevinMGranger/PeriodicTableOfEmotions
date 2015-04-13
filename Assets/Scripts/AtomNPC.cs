using UnityEngine;
using System.Collections;

public class AtomNPC : MonoBehaviour {
    // holds the charge of the NPC
    public int atomicCharge =  0;
    // Get ZoneCheck's GameObject
    public ZoneCheck zone;
	// Use this for initialization
	void Start () {
        // find the script and allow me to check for collision
        zone = GameObject.Find("InteractionZone").GetComponent<ZoneCheck>();
	}
	
	// Update is called once per frame
	void Update () {
        // if the player is colliding with the zone, allow him to interact with the NPC
        if (zone.collides == true)
        {
            Debug.Log("Press F to raise Charge!");
            Debug.Log("Charge: " + atomicCharge);
            if(Input.GetKeyDown(KeyCode.F))
            {
                atomicCharge++;
            }
        }
        // if not, then don't do anything right now.
        else
        {
            Debug.Log("Goodbye");
        }
	}
}

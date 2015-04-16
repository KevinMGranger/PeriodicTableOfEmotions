using UnityEngine;
using System.Collections;

public class ZoneCheck : MonoBehaviour
{
    // Have a collision check
    public bool collides = false;
	public bool hitsNPC = false;
	public GameObject InteractionZoneCollided;

    // If Player enters collision zone, return true
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            collides = true;
        }
		if(other.gameObject.name == "InteractionZone1" || other.gameObject.name == "InteractionZone2" || other.gameObject.name == "InteractionZone3" || other.gameObject.name == "InteractionZone4")
		{
			InteractionZoneCollided = other.gameObject;
			hitsNPC = true;
		}
    }
    // If the player leaves, return false
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            collides = false;
        }
		if(other.gameObject.name == "InteractionZone1" || other.gameObject.name == "InteractionZone2" || other.gameObject.name == "InteractionZone3" || other.gameObject.name == "InteractionZone4")
		{
			InteractionZoneCollided = null;
			hitsNPC = false;
		}
    }

}

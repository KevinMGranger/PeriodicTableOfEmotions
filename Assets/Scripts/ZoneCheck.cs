using UnityEngine;
using System.Collections;

public class ZoneCheck : MonoBehaviour
{
    // Have a collision check
    public bool collides = false;
	public bool hitsNPC = false;
	public AtomNPC collidedNPC;
	public GameObject InteractionZoneCollided;

    // If Player enters collision zone, return true
    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag != "Visual" && other.gameObject.tag != "Room") 
		{
			if (other.gameObject.tag == "Player")
			{
				collides = true;
			}
			if(other.gameObject.tag == "Atom")
			{
				Debug.Log("Zone Check Works");
				InteractionZoneCollided = other.gameObject;
				collidedNPC = other.gameObject.GetComponentInParent<AtomNPC>();
				hitsNPC = true;
			}
		}
    }
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log ("HERE! " + col.gameObject.tag);
	}
    // If the player leaves, return false
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            collides = false;
        }
		if(other.gameObject.name == "InteractionZone1" || other.gameObject.name == "InteractionZone2" || other.gameObject.name == "InteractionZone3" || other.gameObject.name == "InteractionZone4")
		{
			InteractionZoneCollided = null;
			collidedNPC = null;
			hitsNPC = false;
		}
    }

}

using UnityEngine;
using System.Collections;

public class ZoneCheck : MonoBehaviour
{
    // Have a collision check
    public bool collides = false;

    // If Player enters collision zone, return true
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            collides = true;
        }
    }
    // If the player leaves, return false
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            collides = false;
        }
    }

}

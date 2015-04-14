using UnityEngine;
using System.Collections;

public class ImageLoader : MonoBehaviour
{
    public ZoneCheck[] zone;
    //public Sprite NPC_1;
    //public SpriteRenderer npc;
    //public AtomNPC npcObject;
    public GameObject npcImage;
    public GameObject npcImage2;
    public GameObject npcImage3;
    public GameObject npcImage4;
    // Use this for initialization
    void Start()
    {
        zone = GameObject.FindObjectsOfType<ZoneCheck>() as ZoneCheck[];
        //npc = GameObject.Find("NPCImage1").GetComponent<SpriteRenderer>();
        //npcImage = GameObject.Find("NPCImage1");
        //npcImage2 = GameObject.Find("NPCImage2");
        //npcImage3 = GameObject.Find("NPCImage3");
        //npcImage4 = GameObject.Find("NPCImage4");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ZoneCheck z in zone)
        {
            if (zone[0].collides == true)
            {
                npcImage.SetActive(true);
            }
            else
            {
                npcImage.SetActive(false);
            }
            if (zone[1].collides == true && gameObject.name == "NPC2")
            {
                npcImage2.SetActive(true);
            }
            else
            {
                npcImage2.SetActive(false);
            }
            if (zone[2].collides == true && gameObject.name == "NPC3")
            {
                npcImage3.SetActive(true);
            }
            else
            {
                npcImage3.SetActive(false);
            }
            if (zone[3].collides == true && gameObject.name == "NPC4")
            {
                npcImage4.SetActive(true);
            }
            else
            {
                npcImage4.SetActive(false);
            }
        }
    }
}

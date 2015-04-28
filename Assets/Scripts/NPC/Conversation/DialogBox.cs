using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Text))]
public class DialogBox : MonoBehaviour {

    Animator anim;

    Text txt;

    public string Text
    {
        set
        {
            txt.text = value;
        }
        get
        {
            return txt.text;
        }
    }

    public bool Active
    {
        get
        {
            return boxActive;
        }
        set
        {
            if (boxActive != value)
            {
                anim.SetTrigger((value) ? "Active" : "Inactive");
                boxActive = value;
            }
        }
    }

    public bool boxActive;

    void Awake()
    {
        anim = GetComponent<Animator>();
        txt = GetComponentInChildren<Text>();
    }


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void toggle()
    {
        Active = !Active;
    }
}

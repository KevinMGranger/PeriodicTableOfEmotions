using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResponseButton : MonoBehaviour {

    Button btn;
    Text txt;

    public string Response
    {
        get { return txt.text; }
        set
        {
            txt.text = value;
        }
    }

    public ConversationManager Manager
    {
        get;
        set;
    }

    void Awake()
    {
        btn = GetComponent<Button>();
        txt = GetComponentInChildren<Text>();
    }

	// Use this for initialization
	void Start () {
        btn.onClick.AddListener(this.Clicked);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Clicked()
    {
        Manager.ChoiceSelected(txt.text);
    }
}

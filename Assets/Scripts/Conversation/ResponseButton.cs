using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResponseButton : MonoBehaviour {

    Button btn;
    Text txt;

    ConversationChoice response;
    public ConversationChoice Response
    {
        get { return response; }
        set
        {
            response = value;
            txt.text = value.text;
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
        Manager.ConversationChoiceSelected(response);
    }
}

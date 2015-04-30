using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Conversation
{
	public class UI : MonoBehaviour
	{
		Converser con;

		DialogBox dialogBox;

		OptionContainer options;

		NamePanel namePanel;

        Picture pic;

        Canvas can;

		void Awake()
		{
			this.CheckComponentInChildren(ref dialogBox);
			this.CheckComponentInChildren(ref options);
			this.CheckComponentInChildren(ref namePanel);
			this.CheckComponentInChildren(ref pic);
            this.CheckComponent(ref can);
		}

		void Start() { }

		void Update() { }

		public void EnableConversation(Converser con)
		{
            can.enabled = true;
			namePanel.SetName(con.name);
			options.SetOptions(con, con.options);
			dialogBox.SetText(con.text);
            pic.SetImage(con.visual);
		}

		public void LeaveConversation()
		{
			namePanel.LeaveConversation();
            can.enabled = true;
		}
	}
}
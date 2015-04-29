using UnityEngine;
using System.Collections;

namespace Conversation
{
	public class UI : MonoBehaviour
	{
		Converser con;

		DialogBox dialogBox;

		OptionContainer options;

		NamePanel namePanel;

		void Awake()
		{
			this.CheckComponentInChildren(ref dialogBox);
			this.CheckComponentInChildren(ref options);
			this.CheckComponentInChildren(ref namePanel);
		}

		void Start() { }

		void Update() { }

		public void EnableConversation(Converser con)
		{
			namePanel.SetName(con.name);
			options.SetOptions(con, con.options);
			dialogBox.SetText(con.text);
		}

		public void LeaveConversation()
		{
			namePanel.LeaveConversation();
		}
	}
}
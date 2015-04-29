using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Conversation
{
	public class NamePanel : MonoBehaviour
	{
		public Text txt;

		public string Name
		{
			get { return txt.text; }
			set
			{
				txt.text = value;
			}
		}

		void Start() { }

		void Update() { }

		public void SetName(string name)
		{
			Name = name;
		}

		public void LeaveConversation()
		{
		}
	}
}
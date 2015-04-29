using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Conversation
{
	/// <summary>
	/// A box of spoken text
	/// </summary>
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Text))]
	public class DialogBox : MonoBehaviour
	{
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

		bool boxActive;

		void Awake()
		{
			anim = GetComponent<Animator>();
			txt = GetComponentInChildren<Text>();
		}

		void Start() { } 

		void Update() { } 

		public void SetText(string text)
		{
			Active = false;
			Text = text;
			Active = true;
		}

		public void toggle()
		{
			Active = !Active;
		}
	}
}

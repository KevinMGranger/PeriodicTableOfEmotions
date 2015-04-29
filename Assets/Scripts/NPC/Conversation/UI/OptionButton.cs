using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Conversation
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Button))]
	public class OptionButton : MonoBehaviour
	{
		Button btn;

		Text txt;

		Option option;

		public Option Option
		{
			get { return option; }

			set
			{
				option = value;
				Text = value;
			}
		}

		public string Text
		{
			get { return txt.text; }
			set { txt.text = value; }
		}

		public Converser Converser
		{
			get;
			set;
		}

		void Awake()
		{
			this.CheckComponent(ref btn);
			this.CheckComponentInChildren(ref txt);
		}

		void Start()
		{
			btn.onClick.AddListener(this.Clicked);
		}

		void Update() { }

		public void Clicked()
		{
			Converser.OptionChosen(option);
		}
	}
}

using UnityEngine;
using System.Collections;

namespace Conversation
{
	/// <summary>
	/// A converser holds a conversation, updating visuals as necessary.
	/// </summary>
	public class Converser : MonoBehaviour
	{
		[Tooltip("The container holding the conversation to have")]
		public Container conversation;

		/// <summary>
		/// The root of the conversation that we get from the container.
		/// </summary>
		Point root;

		/// <summary>
		/// The current location of the conversation
		/// </summary>
		Point currentLocation;

		/// <summary>
		/// The text to manage dialogue because Kevin's event system is broken.
		/// </summary>
		public string texter;

		public Option[] options
		{
			get { return currentLocation.options; }
		}

		public string text
		{
			get { return currentLocation.text; }
		}

		[Tooltip("The material to set when presenting the converser in the UI.")]
		public Sprite visual;

		/// <summary>
		/// The name to set when presenting the converser in the UI.
		/// Leave blank to have the AtomNPC set it up
		/// </summary>
		[Tooltip("The name to set when presenting the converser in the UI.\n" +
		"Leave blank to have the AtomNPC set it up")]
		public string Name;

		[Tooltip("The UI component to the conversation.\n" +
		"Leave blank to search for \"Conversation UI\"")]
		public UI ui;

		void Awake()
		{
            if (!conversation)
            {
                if (!(conversation = GetComponent<Container>()))
                {
                    Debug.LogError("Couldn't find Conversation container!");
                }
            }

            root = conversation;
            currentLocation = root;

			if (!visual) Debug.LogWarning("No visual for Converser, is this intentional?");

			if (!ui)
			{
				var go = GameObject.Find("Convesation UI");
				if (!go)
				{
					Debug.LogError("No UI for Converser and couldn't find one when looking.");
				}
				else
				{
					ui = go.GetComponent<UI>();
					//FIXME: check here too. Lazy.
				}
			}

		}

		void Start() { }

		void Update() 
		{
		}

		public void EnableConversation()
		{
			ui.EnableConversation(this);
		}

		public void LeaveConversation()
		{
			ui.LeaveConversation();
		}
		public void ResetConversation()
		{
			currentLocation = root;
		}
		public void OptionChosen(Option op)
		{
			op.whenChosen.Invoke();
			var next = currentLocation[op];
			string me = op.ToString ();
			texter = me;
			if (next == null)
			{
				Debug.LogError("Got option I don't know about: " + op);
				return;
			}

			currentLocation = next;

			EnableConversation();
		}

		public void UpdateConversation()
		{
			root = conversation;
			currentLocation = root;
		}
	}
}
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

		public Option[] options
		{
			get { return currentLocation.options; }
		}

		public string text
		{
			get { return currentLocation.text; }
		}

		[Tooltip("The material to set when presenting the converser in the UI.")]
		public Material visual;

		[Tooltip("The name to set when presenting the converser in the UI.")]
		public string Name;

		[Tooltip("The UI component to the conversation.\n" +
		"Leave blank to search for \"Conversation UI\"")]
		public UI ui;

		void Awake()
		{
			if (!conversation)
				if (!(conversation = GetComponent<Container>()))
				{
					Debug.LogError("Couldn't find Conversation container!");
				}

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

		void Update() { }

		public void EnableConversation()
		{
			ui.EnableConversation(this);
		}

		public void LeaveConversation()
		{
			ui.LeaveConversation();
		}

		public void OptionChosen(Option op)
		{
			var next = currentLocation[op];

			if (next == null)
			{
				Debug.LogError("Got option I don't know about: " + op);
				return;
			}

			currentLocation = next;

			EnableConversation();
		}
	}
}
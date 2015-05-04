using UnityEngine;
using UnityEngine.Events;
using System;
using Conversation;

namespace NPC
{
	/// <summary>
	/// Sentiment towards player
	/// </summary>
	public enum Sentiment
	{
		NeverMet,
		Met,
		Trusting
	}

	/// <summary>
	/// An atom's state
	/// </summary>
	public enum State
	{
		Neutral,
		Excited,
		InLove
	}

	[Serializable]
	public class AtomStateChanged : UnityEvent<AtomNPC, State> { }

	[Serializable]
	public class AtomSentimentChanged : UnityEvent<AtomNPC, Sentiment> { }

	public class AtomNPC : MonoBehaviour
	{
		// Name of the NPC
		public string NPC_Name = "A Generic Atom";

		#region State
		[SerializeField]
		private State state;
		public AtomStateChanged onStateChange;
		public State State
		{
			get { return state; }
			set
			{
				state = value;
				onStateChange.Invoke(this, value);
			}
		}
		#endregion

		#region Sentiment
		[SerializeField]
		private Sentiment sentiment;
		public AtomSentimentChanged onSentimentChange;
		public Sentiment Sentiment
		{
			get { return sentiment; }
			set
			{
				sentiment = value;
				onSentimentChange.Invoke(this, value);
			}
		}
		#endregion

		// working around the broken conversation code for now...
		string option;

        Converser convo;

        public SpeechBubbleChanger sbc;

		void Start()
		{
			// if convo, set name
            this.CheckComponent(ref convo);
				if (convo.Name.Length == 0) convo.Name = NPC_Name;

                this.CheckComponentInChildren(ref sbc);
		}

		void Update()
		{
			// Edited for the sake of having a conversation system
			option = convo.texter;
		}

        void OnTriggerEnter(Collider col)
        {
            if (col.IsPlayer())
            {
                sbc.gameObject.SetActive(true);
            }
        }

        void OnTriggerStay(Collider col)
        {
            if (col.IsPlayer() && Input.GetKeyDown(KeyCode.E))
            {
                convo.EnableConversation();
                col.gameObject.GetComponent<Character>().EnableConversation();
            }

			conversationManager ();

        }

        void OnTriggerExit(Collider col)
        {
			if (col.IsPlayer ()) {
				convo.LeaveConversation ();
				convo.ResetConversation ();
				col.gameObject.GetComponent<Character> ().LeaveConversation ();
				sbc.gameObject.SetActive (false);
				convo.UpdateConversation();
			}
		}

		// Only made because I needed a quick workaround to "When Chosen" - Sung
		void conversationManager()
		{
			// Conversation placeholder, manages state change
			if (option == "Yeah that's right") {
				sentiment = Sentiment.Met;
			} 
			else if (option == "Study of matter")
			{
				sentiment = Sentiment.Trusting;
			} 
			else if (option == "Antoine Lavoisier")
			{
				sentiment = Sentiment.Trusting;
			}

			// Change the conversations around
			if (sentiment == Sentiment.Met && this.gameObject.name == "Atom") {
				convo.conversation = GameObject.Find ("Quiz").GetComponent<Container> ();
			}
			else if (sentiment == Sentiment.Met && this.gameObject.name == "Atom2") 
			{
				convo.conversation = GameObject.Find ("Quiz2").GetComponent<Container> ();
			}
			if (sentiment == Sentiment.Trusting)
			{
				sbc.SetAlternating(SpeechBubbleState.Exclaim,"F");
				convo.conversation = GameObject.Find("Generic Trust").GetComponent<Container>();
			}
		}
	}
}
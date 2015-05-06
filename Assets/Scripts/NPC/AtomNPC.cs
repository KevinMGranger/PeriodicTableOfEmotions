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
		public State state;
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
		public Sentiment sentiment;
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
				
		bool isTrusting = false;
		public bool convoOpen = false;

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
			if (col.IsPlayer () && Input.GetKeyDown (KeyCode.E) && convoOpen == false) {
				convo.EnableConversation ();
				col.gameObject.GetComponent<Character> ().EnableConversation ();
				convoOpen = true;
			}
			else {
				if (option == "Bye" && col.IsPlayer()) 
				{
					convo.LeaveConversation ();
					col.gameObject.GetComponent<Character>().LeaveConversation();
					convo.ResetConversation ();
					convo.UpdateConversation();
					convoOpen = false;
					convo.texter = null;
				}
				else if (option == "Great!" && col.IsPlayer()) 
				{
					convo.LeaveConversation ();
					col.gameObject.GetComponent<Character>().LeaveConversation();
					convo.ResetConversation ();
					convo.UpdateConversation();
					convoOpen = false;
					convo.texter = null;
				}
			}
			conversationManager ();


        }

        void OnTriggerExit(Collider col)
        {
			if (col.IsPlayer () && convoOpen == true) {
				convo.LeaveConversation ();
				convo.ResetConversation ();
				col.gameObject.GetComponent<Character> ().LeaveConversation ();
				convo.UpdateConversation();
				convoOpen = false;
			}
			if(state != State.InLove)
			{
				sbc.gameObject.SetActive (false);
			}
		}

		// Only made because I needed a quick workaround to "When Chosen" - Sung
		void conversationManager()
		{
			// Conversation placeholder, manages state change
			// This way is abhorant but it's the best we can do right now. 
			if (option == "Study of Matter")
			{
				sentiment = Sentiment.Trusting;
			} 
			else if (option == "Antoine Lavoisier") 
			{
				sentiment = Sentiment.Trusting;
			}
			else if (option == "Electrons, Neutrons, and Protons")
			{
				sentiment = Sentiment.Trusting;
			} 
			else if (option == "Hydrogen") 
			{
				sentiment = Sentiment.Trusting;
			}

			// Change the conversations around
			if (sentiment == Sentiment.Trusting) 
			{
				if(isTrusting == false)
				{
					sbc.SetAlternating(SpeechBubbleState.Exclaim, "F");
					convo.conversation = GameObject.Find("Generic Trust").GetComponent<Container>();
					isTrusting = true;
				}
			}
		}

		public void Match()
		{
			sbc.SetState (SpeechBubbleState.Heart);
			sbc.text = "";
			sbc.gameObject.SetActive(true);
		}
	}
}

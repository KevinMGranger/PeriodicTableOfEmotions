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
        }

        void OnTriggerExit(Collider col)
        {
            if (col.IsPlayer())
            {
                convo.LeaveConversation();
                col.gameObject.GetComponent<Character>().LeaveConversation();
                sbc.gameObject.SetActive(false);
            }
        }

	}
}
/*
convo = new ConversationPoint("Hey! I'm " + npcName,
	new ResponseTree {
		{ "Hi, I'm Adom!", new ConversationPoint("So you want to try and match me right? Well here's the deal, I'll quiz you on chemistry. If you can get three correct answers, I'll give you more information on my nature. (Coming Soon)"
			)}});
		//new ResponseTree { 
		//    {"Sure", new ConversationPoint("Okay cool! Here's your first question: Who is the father of modern Chemistry?",
		//          new ResponseTree{
		//            {"Queen Elizabeth II", new ConversationPoint("She's too busy ruling England")},
		//            {"Sir Francis Drake", new ConversationPoint("No, I think not")},
		//            {"Antoine Lavoisier", new ConversationPoint("He is widely considered the founder of Modern Chemistry. You're correct!",
		//          //})}})}});
		//              new ResponseTree{
		//                  {"Great!", new ConversationPoint("Okay now on to the real thing",
		//                      new ResponseTree{
		//                      {"Let's do this",new ConversationPoint("What is an atom composed of?",
		//                              new ResponseTree{ 
		//                              {"Bits, Bytes, and Gigabytes", new ConversationPoint("'Fraid not.")},
		//                              {"Potatoes. Lots of Potatoes", new ConversationPoint("No.")},
		//                              {"Protons, Electrons, and Neutrons", new ConversationPoint("That's Correct! Now what is the definition of chemistry?", 
		//                                      new ResponseTree{
		//                                      {"Romance!!!", new ConversationPoint("Yes!!! But also no!!")},
		//                                      {"Study of Matter", new ConversationPoint("Yeah, that's basically it!")},
		//                                      {"The study of kittens", new ConversationPoint ("I don't think we're on the same page here.")}

			//})}})}});
		//    })}})}})}})}})}})}});
convoTrust = new ConversationPoint("Hey Adom!",
	  new ResponseTree {
		{ "Sup" + npcName, new ConversationPoint("nm, u?")}
	  });
convoWin = new ConversationPoint ("Congratulations!!! You've matched all of the elements together!");
*/
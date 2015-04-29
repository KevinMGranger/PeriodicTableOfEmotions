using UnityEngine;
using System.Collections;

namespace Conversation
{
	/// <summary>
	/// A Container is a component that holds a conversation separately,
	/// for serialization and organization reasons.
	/// 
	/// It should not be modified-- rather, a Converser should just
	/// get the root from this.
	/// </summary>
	public class Container : MonoBehaviour
	{
		public Point root;

		void Start() { }

		void Update() { }

		public static implicit operator Point(Container con)
		{
			return con.root;
		}
	}
}
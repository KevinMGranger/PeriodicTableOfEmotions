using UnityEngine;
using System.Collections;

namespace Conversation
{
	public class OptionContainer : MonoBehaviour
	{
		public OptionButton prefab;

		void Awake()
		{
			if (!prefab) this.LogMissingComponent<OptionButton>();
		}

		void Start() { }

		void Update() { }

		public void SetOptions(Converser con, Option[] options)
		{
			clearChildren();
			foreach (var op in options) {
				var optionButton = Instantiate(prefab) as OptionButton;
				optionButton.Option = op;
				optionButton.Converser = con;

				optionButton.transform.SetParent(this.transform, false);
			}
		}

		public void clearChildren()
		{
			foreach (Transform potato in transform)
			{
				Destroy(potato.gameObject);
			}
		}
	}
}
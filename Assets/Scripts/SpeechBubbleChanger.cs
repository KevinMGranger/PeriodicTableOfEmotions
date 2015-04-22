using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Shader))]
public class SpeechBubbleChanger : MonoBehaviour {

	public Material blank, ellipsis, exclaim, heart;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.U))
		{
			if (renderer.sharedMaterial == exclaim)
			{
				renderer.sharedMaterial = ellipsis;
			}
			else
			{
				renderer.sharedMaterial = exclaim;
			}
		}
	}
}
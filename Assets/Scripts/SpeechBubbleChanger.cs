﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum SpeechBubbleState
{
	Blank,
	Ellipsis,
	Exclaim,
	Heart,
	AlternatingText
}

[RequireComponent(typeof(Shader))]
public class SpeechBubbleChanger : MonoBehaviour
{

	/// <summary>
	/// Materials to use when switching.
	/// </summary>
	public Material blank, ellipsis, exclaim, heart;

	#region State

	public SpeechBubbleState initialState;

	/// <summary>
	/// The current state of the behavior of the speech bubble
	/// </summary>
	SpeechBubbleState state;

	/// <summary>
	/// The other state to alternate to if the main state is in alternating mode
	/// </summary>
	SpeechBubbleState alternate;

	/// <summary>
	/// Change state, altering the material as needed
	/// </summary>
	SpeechBubbleState State
	{
		get
		{
			return state;
		}
		set
		{
			state = value;
			SetMaterial(value);
		}
	}

	#endregion

	/// <summary>
	/// The text component to control if in Alternating mode.
	/// If not given, it will search for the first Text component in all children
	/// </summary>
	[ContextMenu("The text component to control if in Alternating mode.\n" +
	"If not given, it will search for the first Text component in all children.")]
	public Text uiText;

	string text
	{
		get
		{
			return uiText.text;
		}
		set
		{
			uiText.text = value;
		}
	}

	/// <summary>
	/// Is the text currently being displayed?
	/// </summary>
	bool onText;

	/// <summary>
	/// How often to switch between the laternate and the text
	/// </summary>
	float timeInterval;

	/// <summary>
	/// When should we next switch?
	/// </summary>
	float nextTime;

	/// <summary>
	/// Set material based off of given state.
	/// </summary>
	/// <param name="state"></param>
	void SetMaterial(SpeechBubbleState state)
	{
		switch (state)
		{
			case SpeechBubbleState.Blank:
				renderer.material = blank;
				break;
			case SpeechBubbleState.Ellipsis:
				renderer.material = ellipsis;
				break;
			case SpeechBubbleState.Exclaim:
				renderer.material = exclaim;
				break;
			case SpeechBubbleState.Heart:
				renderer.material = heart;
				break;
			case SpeechBubbleState.AlternatingText:
			default:
				break;
		}
	}

	void SetMaterial(Material material)
	{
		renderer.material = material;
	}

	// Set to alternating mode
	public void SetAlternating(SpeechBubbleState alternate, string text, float timeInterval)
	{
		this.State = SpeechBubbleState.AlternatingText;
		this.alternate = alternate;
		this.text = text;
		this.timeInterval = timeInterval;

		onText = false;
		SetMaterial(alternate);
		nextTime = Time.time + timeInterval;
	}

	void Start()
	{
		if (!uiText) uiText = GetComponentInChildren<Text>();

		State = initialState;
	}

	void Update()
	{
		// THIS IS SO UGLY KEVIN
		// YOU SHOULD FEEL BAD
		if (state == SpeechBubbleState.AlternatingText)
		{
			if (nextTime < Time.time)
			{
				if (onText)
				{
					onText = false;
					uiText.enabled = false;
					SetMaterial(alternate);
				}
				else
				{
					// not on text
					onText = true;
					uiText.enabled = true;
					SetMaterial(blank);
				}

				nextTime = Time.time + timeInterval;
			}
		}
	}

	public void SetDemoAlternating()
	{
		SetAlternating(SpeechBubbleState.Ellipsis, "F", 1.5f);
	}
}
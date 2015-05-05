using UnityEngine;
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

[ExecuteInEditMode]
[RequireComponent(typeof(Shader))]
public class SpeechBubbleChanger : MonoBehaviour
{
	/// <summary>
	/// Materials to use when switching.
	/// </summary>
	public Material blank, ellipsis, exclaim, heart;

	#region State

	/// <summary>
	/// The current state of the behavior of the speech bubble
	/// </summary>
	public SpeechBubbleState state;

	/// <summary>
	/// The other state to alternate to if the main state is in alternating mode
	/// </summary>
	public SpeechBubbleState alternate;

	/// <summary>
	/// Change state, altering the material as needed
	/// </summary>
	public void SetState(SpeechBubbleState newState)
	{
		state = newState;
		SetMaterial(newState);
	}

	#endregion

	/// <summary>
	/// The text component to control if in Alternating mode.
	/// If not given, it will search for the first Text component in all children
	/// </summary>
	[ContextMenu("The text component to control if in Alternating mode.\n" +
	"If not given, it will search for the first Text component in all children.")]
	public Text uiText;

	[ContextMenu("Text to use to alternate if not specified when called")]
	public string initialText;

	public string text
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
	bool onText
	{
		get
		{
			return uiText.enabled;
		}
		set
		{
			uiText.enabled = value;
		}
	}

	/// <summary>
	/// How often to switch between the alternate and the text
	/// </summary>
	[ContextMenu("How often to switch between the alternate and the text")]
	public float timeInterval;

	/// <summary>
	/// When should we next switch?
	/// </summary>
	public float nextTime;

	/// <summary>
	/// Set material based off of given state.
	/// </summary>
	/// <param name="state"></param>
	public void SetMaterial(SpeechBubbleState state)
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
	public void SetAlternating(SpeechBubbleState alternate)
	{
		SetAlternating(alternate, initialText);
	}

	public void SetAlternating(SpeechBubbleState alternate, string text)
	{
		//Debug.Log("sb alternating");
		this.state = SpeechBubbleState.AlternatingText;
		this.alternate = alternate;
		this.text = text;

		initialText = text;

		onText = false;
		SetMaterial(alternate);
		nextTime = Time.time + timeInterval;
	}

	void Start()
	{
		if (!uiText) uiText = GetComponentInChildren<Text>();

		SetState(state);
		text = initialText;
	}

	void toggle()
	{
		onText = !onText;
		SetMaterial((onText) ? SpeechBubbleState.Blank : alternate);
	}

	void Update()
	{
		if (state == SpeechBubbleState.AlternatingText)
		{
			if (nextTime < Time.time)
			{
				toggle();

				nextTime = Time.time + timeInterval;
			}
		}
		SetMaterial(state);
	}

	public void SetDemoAlternating()
	{
		Debug.Log("setting demo");
		SetAlternating(SpeechBubbleState.Ellipsis, "F");
	}
}

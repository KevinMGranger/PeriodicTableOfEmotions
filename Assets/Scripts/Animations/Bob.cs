using UnityEngine;
using System.Collections;

public class Bob : MonoBehaviour
{
	public float magnitude;

	public float period;

	[ContextMenuItem("Reset offset to half-height", "SetOffsetToBottom")]
	public float offset;

	[HideInInspector]
	public Vector3 startingPosition;

	public enum SinusoidalFunction { InverseCos, Cos, Sin }
	public delegate float SinusoidalFunctionDel(float angle);

	public SinusoidalFunction sinusoidalFunction;
	public SinusoidalFunctionDel sinusoidalFunctionDel;

	// Use this for initialization
	void Start()
	{
		startingPosition = gameObject.transform.position;

		switch (sinusoidalFunction)
		{
			case SinusoidalFunction.Cos:
				sinusoidalFunctionDel = Mathf.Cos;
				break;
			case SinusoidalFunction.Sin:
				sinusoidalFunctionDel = Mathf.Sin;
				break;
			case SinusoidalFunction.InverseCos:
				sinusoidalFunctionDel = (x) => -Mathf.Cos(x);
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
		DrawLines();
		float theta = (Mathf.PI * 2) * (Time.time / period);
		float amplitude = sinusoidalFunctionDel(theta);
		float ampWithMag = magnitude * amplitude;
		float y = startingPosition.y + ampWithMag;

		setY(y);
	}

	void setY(float y)
	{
		gameObject.transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}

	void DrawLines()
	{
		DrawStartingPosLine();
	}

	void DrawStartingPosLine()
	{
		Vector3 left = startingPosition;
		Vector3 right = startingPosition;

		left.x -= transform.localScale.x;
		right.x += transform.localScale.x;

		Debug.DrawLine(left, right, Color.red);
	}

	[ContextMenu("Reset offset to half-height")]
	void SetOffsetToBottom()
	{
		offset = transform.localScale.y / 2;
	}
}
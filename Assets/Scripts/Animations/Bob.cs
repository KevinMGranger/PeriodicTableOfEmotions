using UnityEngine;
using System.Collections;

public class Bob : MonoBehaviour
{
	public float magnitude;

	public float period;

	[HideInInspector]
	public Vector3 startingPosition;
	Vector3 nextPosition;

	public enum SinusoidalFunction { InverseCos, Cos, Sin }
	public delegate float SinusoidalFunctionDel(float angle);

	public SinusoidalFunction sinusoidalFunction;
	public SinusoidalFunctionDel sinusoidalFunctionDel;

	// Use this for initialization
	void Start()
	{
		startingPosition = gameObject.transform.localPosition;
		nextPosition = startingPosition;

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
		float theta = (Mathf.PI * 2) * (Time.time / period);
		float rawAmplitude = sinusoidalFunctionDel(theta);
		float ampWithMag = magnitude * rawAmplitude;
		float y = startingPosition.y + magnitude + ampWithMag;

		setY(y);
	}

	void setY(float y)
	{
		nextPosition.y = y;
		gameObject.transform.localPosition = nextPosition;
	}
}
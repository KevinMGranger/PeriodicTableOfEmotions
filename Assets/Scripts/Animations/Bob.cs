using UnityEngine;

namespace Animation
{
	/// <summary>
	/// Bob up and down in place sinusoidally.
	/// </summary>
	public class Bob : MonoBehaviour
	{

		public float magnitude = 0.17f;

		[Tooltip("How many seconds it takes for a cycle to complete")]
		public float period = 4;

		[HideInInspector]
		Vector3 startingPosition;

		// the next position to pass to the associated gameobject.
		// kept as a member to avoid unneeded allocation each update
		// (or do structs not work that way? TODO)
		Vector3 nextPosition;

		#region Sinusoidal Function Selection
		// Internally, we take the enum selected in the inspector and use it to hook up a function delegate.

		public enum SinusoidalFunction { InverseCos, Cos, Sin }

		[Tooltip("Which sinusoidal function's path to follow")]
		public SinusoidalFunction sinusoidalFunction;

		delegate float SinusoidalFunctionDel(float angle);
		SinusoidalFunctionDel sinusoidalFunctionDel;

		#endregion

		// Use this for initialization
		void Start()
		{
			startingPosition = gameObject.transform.localPosition;
			nextPosition = startingPosition;

			// Set up the right function based on the given preference
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

		void Update()
		{
			float theta = (Mathf.PI * 2) * (Time.time / period);
			float rawAmplitude = sinusoidalFunctionDel(theta);
			float ampWithMag = magnitude * rawAmplitude;
			float y = startingPosition.y + magnitude + ampWithMag;

			nextPosition.y = y;
			gameObject.transform.localPosition = nextPosition;
		}
	}
}
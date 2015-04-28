using UnityEngine;

public class ElectronShell : MonoBehaviour
{
	public float speed;

	Vector3 rotationToApply;

	void Start() { }

	void Update()
	{
		rotationToApply.z = speed * Time.deltaTime;

		transform.Rotate(rotationToApply);
	}
}

using UnityEngine;
using System.Collections;

public class ElectronShell : MonoBehaviour
{

	public float speed;

	Vector3 rotationToApply;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		rotationToApply.z = speed * Time.deltaTime;

		transform.Rotate(rotationToApply);

	}
}

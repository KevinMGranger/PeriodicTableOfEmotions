using UnityEngine;
using System.Collections;

public class LookAtMeMeow : MonoBehaviour {

    public Transform playa;

    public Transform whoToRotate;

    [Tooltip("How much one should dramatic chipmunk")]
    public float rotationSpeed;

    float startTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider col)
    {
        startTime = Time.time;
    }

    void OnTriggerStay(Collider col)
    {
        var meToYou = (playa.position - whoToRotate.position).normalized;

        var rotationTarget = Quaternion.LookRotation(meToYou);

        var time = (Time.time - startTime);

        whoToRotate.rotation = Quaternion.Slerp(whoToRotate.rotation, rotationTarget, rotationSpeed * time);
    }
}
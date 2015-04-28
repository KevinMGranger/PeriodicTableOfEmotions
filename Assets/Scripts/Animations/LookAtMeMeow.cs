using UnityEngine;
using System.Collections;

public class LookAtMeMeow : MonoBehaviour {

    public Transform playa;

    public Transform whoToRotate;

    [Tooltip("How much one should dramatic chipmunk")]
    public float rotationSpeed;

    float startTime;

	[Tooltip("How long after entering my zone should I turn to face you?")]
	public float delay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.tag != "Visual" && col.gameObject.tag != "Room") 
		{
			Debug.Log ("I collided with " + col.gameObject.tag);
			startTime = Time.time + delay;
		}
    }

    void OnTriggerStay(Collider col)
    {
		if (col.gameObject.tag != "Visual" && col.gameObject.tag != "Room") 
		{

			var meToYou = (playa.position - whoToRotate.position).normalized;
			
			meToYou.y = 0;
			
			var rotationTarget = Quaternion.LookRotation(meToYou);
			
			var time = (Time.time - startTime);
			
			whoToRotate.rotation = Quaternion.Slerp(whoToRotate.rotation, rotationTarget, rotationSpeed * time);
		}
    }
}
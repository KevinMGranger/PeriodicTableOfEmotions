using UnityEngine;

/// <summary>
/// Rotate to face the player as soon
/// </summary>
public class LookAtMeMeow : MonoBehaviour
{
	[Tooltip("What transform should be rotated to face the target?")]
	public Transform whoToRotate;

	[Tooltip("How much one should dramatic chipmunk")]
	public float rotationSpeed = 0.5f;

	/// <summary>
	/// The time at which we should turn to face the player.
	/// </summary>
	float startTime;

	[Tooltip("How long after entering my zone should I turn to face you?")]
	public float delay = 0.4f;

	void Awake()
	{
		if (whoToRotate == null) Debug.LogError("You must attach a transform to rotate!");
	}

	void Start() { }

	void Update() { }

	void OnTriggerEnter(Collider col)
	{
		if (col.IsPlayer())
			StartTurn();
	}

	void OnTriggerStay(Collider col)
	{
		if (col.IsPlayer())
			DoTurn(col.gameObject.transform);
	}

	/// <summary>
	/// Set the start time to start turning towards the player.
	/// </summary>
	void StartTurn()
	{
		startTime = Time.time + delay;
	}

	/// <summary>
	/// Rotate to face the given target
	/// </summary>
	/// <param name="player">The target</param>
	void DoTurn(Transform player)
	{
			var meToYou = (player.position - whoToRotate.position).normalized;

			meToYou.y = 0;

			var rotationTarget = Quaternion.LookRotation(meToYou);

			var time = (Time.time - startTime);

			whoToRotate.rotation = Quaternion.Slerp(whoToRotate.rotation, rotationTarget, rotationSpeed * time);
	}
}
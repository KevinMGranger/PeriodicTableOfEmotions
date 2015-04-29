using UnityEngine;
using System.Collections;

public static class ColliderExtensions
{
	/// <summary>
	/// Determine if the given collider is from the player.
	/// </summary>
	/// <param name="col">The collider whose gameObject we should check</param>
	/// <returns>True if it's the player, false if it's something else.</returns>
	public static bool IsPlayer(this Collider col)
	{
		return col.gameObject.tag == "Player";
	}
}
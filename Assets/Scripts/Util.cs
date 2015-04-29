using UnityEngine;
using System.Collections;
using System.Text;
using System;

public static class Util
{
	public static void LogMissingComponent<T>(this MonoBehaviour self) where T : Component
	{
		self.LogMissingComponent(typeof(T));
	}

	public static void LogMissingComponent(this MonoBehaviour self, Type T)
	{
		var sb = new StringBuilder("A ");
		sb.Append(self.GetType().Name);
		sb.Append(" requires a ");
		sb.Append(T.Name);
		sb.Append(" component, but one was not found.");

		Debug.LogError(sb);
	}
	
	public static void CheckComponent<T>(this MonoBehaviour value, ref T component) where T : Component
	{
		if (component) return;

		component = value.GetComponent<T>();

		if (!component) LogMissingComponent<T>(value);
	}

	public static void CheckComponentInChildren<T>(this MonoBehaviour value, ref T component) where T : Component
	{
		if (component) return;

		component = value.GetComponentInChildren<T>();

		if (!component) LogMissingComponent<T>(value);
	}
}

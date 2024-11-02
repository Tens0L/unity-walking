using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Extensions
{
	public static string GetFullPath(this GameObject obj)
	{
		return GetFullPath(obj.transform);
	}

	public static string GetFullPath(this Transform t)
	{
		string path = t.name;
		var parent = t.parent;
		while (parent)
		{
			path = $"{parent.name}_{path}";
			parent = parent.parent;
		}
		return path;
	}
}
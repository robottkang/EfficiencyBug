using UnityEditor;
using UnityEngine;

public class EditorUtils
{
	//[MenuItem("Utils/Active Tutorial")] // build error
	static public void ActiveTuto()
	{
		PlayerPrefs.SetInt("Tutorial", 0);
		PlayerPrefs.Save();
	}
}
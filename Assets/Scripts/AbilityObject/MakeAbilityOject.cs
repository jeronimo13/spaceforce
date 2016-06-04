using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MakeAbilityOject  {
	#if UNITY_EDITOR

	[MenuItem("Assets/Create/Ability Object")]
	public static void Create()
	{
		AbilityObject asset = ScriptableObject.CreateInstance<AbilityObject> ();
		AssetDatabase.CreateAsset (asset, "Assets/NewAbilityObject.asset");
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
	#endif

}

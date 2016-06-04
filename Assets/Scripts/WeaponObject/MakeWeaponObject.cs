using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MakeWeaponObject 
{
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Weapon Object")]
	public static void Create()
	{
		WeaponObject asset = ScriptableObject.CreateInstance<WeaponObject> ();
		AssetDatabase.CreateAsset (asset, "Assets/NewWeaponObject.asset");
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
	#endif
}
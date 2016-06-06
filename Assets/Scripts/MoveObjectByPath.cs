using UnityEngine;
using System.Collections;

public class MoveObjectByPath : MonoBehaviour {

	public string pathName;
	public float time;
	void Start () {
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", time, "orienttopath", true)); 	
	}
}

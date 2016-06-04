using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoadLevel : MonoBehaviour {

	public void LoadLevelByNumber(int level){

		SceneManager.LoadScene (level);
	}
}

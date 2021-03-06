﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoadLevelWithProgress: MonoBehaviour {

	public Slider loadingBar;
	public GameObject loadingImage;


	private AsyncOperation async;


	public void ClickAsync(int level)
	{
		loadingImage.SetActive(true);
		StartCoroutine(LoadLevelWithBar(level));
	}


	IEnumerator LoadLevelWithBar (int level)
	{
		async = SceneManager.LoadSceneAsync (level);;
		while (!async.isDone)
		{
			loadingBar.value = async.progress;
			yield return null;
		}
	}
}

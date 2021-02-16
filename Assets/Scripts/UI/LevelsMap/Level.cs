using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public LevelData data;

	public Animator levelLoader;
	public float maxWait = 1f;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(LoadClick);
	}
	private void LoadClick()
	{
		string levelName = data.levelName + data.levelN;
		SceneLoaderManager.Instance.LoadLevelByName(levelName);

		StartCoroutine(SceneWaiter());
	}

	private IEnumerator SceneWaiter()
	{
		levelLoader.SetTrigger("TransitionIn");

		float startTime = Time.time;
		float currentTime = Time.time - startTime;
		while(!SceneLoaderManager.Instance.IsLoadComplited)
		{
			yield return null;
		}
		currentTime = Time.time - startTime;

		while(currentTime < maxWait)
		{
			currentTime = Time.time - startTime;
			yield return null;
		}

		SceneLoaderManager.Instance.AllowLoadScene();
	}
}
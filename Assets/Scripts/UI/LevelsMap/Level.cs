using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public LevelData data;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(LoadClick);
	}
	private void LoadClick()
	{
		string levelName = data.levelName + data.levelN;
		SceneLoaderManager.Instance.LoadLevelByName();

		StartCoroutine(SceneWaiter());
	}

	private IEnumerator SceneWaiter()
	{
		while(!SceneLoaderManager.Instance.IsLoadComplited)
		{
			yield return null;
		}
		SceneLoaderManager.Instance.AllowLoadScene();
	}
}
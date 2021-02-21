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
		WindowLoading.Instance.Open();

		string levelName = data.levelName + data.levelN;

		SceneLoaderManager.Instance.LoadLevelByName(levelName);

		StartCoroutine(SceneWaiter());
	}

	private IEnumerator SceneWaiter()
	{
		while(!SceneLoaderManager.Instance.IsLoadComplited)
		{
			WindowLoading.Instance.FillAmount = SceneLoaderManager.Instance.Progress;
			yield return null;
		}
		WindowLoading.Instance.FillAmount = SceneLoaderManager.Instance.Progress;

		yield return new WaitForSeconds(1);
		WindowLoading.Instance.SetText("Completed!");
		WindowLoading.Instance.FillAmount = 1f;
		yield return new WaitForSeconds(2);

		SceneLoaderManager.Instance.AllowLoadScene();
	}
}
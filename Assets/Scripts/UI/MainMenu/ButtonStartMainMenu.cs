using System.Collections;
using UnityEngine;

public class ButtonStartMainMenu : MonoBehaviour
{
	public void StartClick()
	{
		SceneLoaderManager.Instance.LoadLevelsMap();

		StartCoroutine(SceneWaiter());
	}

    public void MainMenu()
    {
        SceneLoaderManager.Instance.LoadMainMenu();

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
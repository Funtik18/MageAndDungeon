using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    private static SceneLoaderManager instance;
    public static SceneLoaderManager Instance
	{
		get
		{
            if(instance == null)
			{
                instance = FindObjectOfType<SceneLoaderManager>();
                if(instance == null)
				{
                    instance = new GameObject("_ScenesManager").AddComponent<SceneLoaderManager>();
                    DontDestroyOnLoad(instance);
                }
			}
            return instance;
		}
	}

    private const string mainMenuSceneName = "MainMenu";
    private const string levelsMapSceneName = "LevelsMap";

    private string currentSceneOnLoad;

    private Coroutine loadCoroutine = null;
    public bool IsLoadingProcess => loadCoroutine != null;

    private AsyncOperation asyncOperation = null;
    public bool IsLoadComplited
	{
		get
		{
            if(asyncOperation != null)
			{
                return asyncOperation.progress >= 0.9f ? true : false;
			}
            return false;
		}
	}

    public void LoadMainMenu()
	{
        StartLoadScene(mainMenuSceneName);
    }
    public void LoadLevelsMap()
    {
        StartLoadScene(levelsMapSceneName);
    }
    public void LoadLevelByName(string levelName = "Level_0")
	{
        StartLoadScene(levelName);
    }


    public void AllowLoadScene()
	{
        asyncOperation.allowSceneActivation = true;
	}

    private void StartLoadScene(string scene)
	{
		if(!IsLoadingProcess)
		{
            currentSceneOnLoad = scene;
            loadCoroutine = StartCoroutine(LoadSceneAsync());
        }
	}
    private IEnumerator LoadSceneAsync()
    {
        asyncOperation = SceneManager.LoadSceneAsync(currentSceneOnLoad);
        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            yield return null;
        }

        StopLoadScene();
    }

	private void StopLoadScene()
	{
		if(IsLoadingProcess)
		{
            StopCoroutine(loadCoroutine);
            loadCoroutine = null;
        }
    }
}
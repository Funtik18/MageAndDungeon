using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
	{
		get
		{
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
	}

    private Wizard wizardTarget;
    public Wizard WizardTarget
	{
		get
		{
            if(wizardTarget == null)
			{
                wizardTarget = FindObjectOfType<Wizard>();
            }
            return wizardTarget;
		}
	}
}
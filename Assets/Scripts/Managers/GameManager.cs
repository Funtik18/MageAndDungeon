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

    private Transform playerTarget;
    public Transform PlayerTarget
	{
		get
		{
            if(playerTarget == null)
			{
                playerTarget = FindObjectOfType<Wizard>().transform;
            }
            return playerTarget;
		}
	}

    [SerializeField] private int moneyCount;
    [SerializeField] private int hpCount;
    [SerializeField] private int passiveErning;

	private void Awake()
	{
        UIManager.Instance.moneyChange(moneyCount);
        UIManager.Instance.hpChange(hpCount);
        InvokeRepeating("passiveMoneyIncrease", 0, 3);
    }

    public void moneyIncrease(int count)
    {
        moneyCount += count;
        UIManager.Instance.moneyChange(moneyCount);
    }

    public void moneyDepleated(int count)
    {
        moneyCount -= count;
        UIManager.Instance.moneyChange(moneyCount);
    }

    public void hpIncrese(int count)
    {
        hpCount += count;
        UIManager.Instance.hpChange(hpCount);
    }

    public void hpDecrease(int count)
    {
        hpCount -= count;
        UIManager.Instance.hpChange(hpCount);
    }

    void passiveMoneyIncrease()
    {
        moneyIncrease(passiveErning);
    }
}
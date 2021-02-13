[System.Serializable]
public class SaveData
{
    private static SaveData instance;
    public static SaveData Instance
	{
		get
		{
			if(instance == null)
			{
				instance = new SaveData();
			}
			return instance;
		}
	}

	public int currentGold;
	public int currentDiamonds;


	public SaveData StartValues()
	{
		currentGold = 100000;
		currentDiamonds = 0;

		return this;
	}


	public void RefreshData(SaveData data)
	{
		currentGold = data.currentGold;
		currentDiamonds = data.currentDiamonds;
	}
}
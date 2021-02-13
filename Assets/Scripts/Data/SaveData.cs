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

	public int[] currentLevelSpells = new int[3];

	public SaveData StartValues()
	{
		currentGold = 0;
		currentDiamonds = 0;

		for(int i = 0; i < currentLevelSpells.Length; i++)
		{
			currentLevelSpells[i] = 0;
		}
		
		return this;
	}

	public void RefreshData(SaveData data)
	{
		currentGold = data.currentGold;
		currentDiamonds = data.currentDiamonds;

		for(int i = 0; i < currentLevelSpells.Length; i++)
		{
			currentLevelSpells[i] = data.currentLevelSpells[i];
		}
	}
}
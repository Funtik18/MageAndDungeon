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

	public int[] statsLevels = new int[2];
	public int[] spellsLevels = new int[3];

	public SaveData StartValues()
	{
		currentGold = 1000;
		currentDiamonds = 0;

		for(int i = 0; i < statsLevels.Length; i++)
		{
			statsLevels[i] = 0;
		}
		for(int i = 0; i < spellsLevels.Length; i++)
		{
			spellsLevels[i] = 0;
		}
		
		return this;
	}

	public void RefreshData(SaveData data)
	{
		currentGold = data.currentGold;
		currentDiamonds = data.currentDiamonds;

		for(int i = 0; i < statsLevels.Length; i++)
		{
			statsLevels[i] = data.statsLevels[i];
		}
		for(int i = 0; i < spellsLevels.Length; i++)
		{
			spellsLevels[i] = data.spellsLevels[i];
		}
	}
}
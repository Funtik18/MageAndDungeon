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

	public int[] statsLevels = new int[7];
	//0 hp
	//1 dmg
	//2 dmg overtime
	//3 speed
	//4 radius
	//5 passice income
	//6 mob scalar

	public int[] spellsLevels = new int[3];
	//0 frost
	//1 fist
	//2 storm

	public SaveData StartValues()
	{
		currentGold = 0;
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

	public SaveData RefreshData(SaveData data)
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

		return this;
	}
}
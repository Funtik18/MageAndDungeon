[System.Serializable]
public class Variable { }
[System.Serializable]
public class VariableInt
{
	public int value;
	public VariableInt(int initValue)
	{
		value = initValue;
	}
}
[System.Serializable]
public class VariableFloat
{
	public float value;
	public VariableFloat(float initValue)
	{
		value = initValue;
	}
}
[System.Serializable]
public class VariableBoolean
{
	public bool value;
	public VariableBoolean(bool initValue)
	{
		value = initValue;
	}
}
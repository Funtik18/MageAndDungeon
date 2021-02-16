using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance;
	public static AudioManager Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<AudioManager>();
			}
			return instance;
		}
	}
}
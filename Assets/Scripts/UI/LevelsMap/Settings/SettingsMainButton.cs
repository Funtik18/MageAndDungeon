using UnityEngine;
using UnityEngine.UI;

public class SettingsMainButton : MonoBehaviour
{
	private RectTransform trans;
	public RectTransform Trans
	{
		get
		{
			if(trans == null)
			{
				trans = transform as RectTransform;
			}
			return trans;
		}
	}

	private Button button;
	public Button Button
	{
		get
		{
			if(button == null)
			{
				button = GetComponent<Button>();
			}
			return button;
		}
	}
}
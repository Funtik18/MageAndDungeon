using UnityEngine;
using DG.Tweening;

public class SettingsMenu : MonoBehaviour
{
	[Header("Main")]
	[SerializeField] private Vector2 spaceBtw;
	[Space]
	[SerializeField] private SettingsMainButton settingsButton;
	[SerializeField] private SettingsMenuItem[] buttons;

	[Header("Settings Button Rotation")]
	[SerializeField] private float expandRotationDuration;
	[SerializeField] private float collapseRotationDuration;
	[SerializeField] private Ease expandRotationEase;
	[SerializeField] private Ease collapseRotationEase;

	[Header("Animation")]
	[SerializeField] private float expandDuration;
	[SerializeField] private float collapseDuration;
	[SerializeField] private Ease expandEase;
	[SerializeField] private Ease collapseEase;

	[Header("Fading")]
	[SerializeField] private float expandFadeDuration;
	[SerializeField] private float collapseFadeDuration;


	private bool isClosed = false;

	private Vector2 settingsButtonPosition;

	private void Awake()
	{
		settingsButton.Button.onClick.AddListener(ToggleMenu);


		settingsButton.Trans.SetAsLastSibling();
		settingsButtonPosition = settingsButton.Trans.anchoredPosition;

		ResetPositions();
	}

	private void ResetPositions()
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			buttons[i].Trans.anchoredPosition = settingsButtonPosition;
		}
	}

	private void ToggleMenu()
	{
		isClosed = !isClosed;

		if(isClosed)//open
		{
			settingsButton.Trans.DORotate(-Vector3.forward * 180f, expandRotationDuration).From(Vector3.zero).SetEase(expandRotationEase);

			for(int i = 0; i < buttons.Length; i++)
			{
				//buttons[i].Trans.anchoredPosition = settingsButtonPosition + spaceBtw * (i + 1);
				buttons[i].Trans.DOAnchorPos(settingsButtonPosition + spaceBtw * (i + 1), expandDuration).SetEase(expandEase);
				buttons[i].Image.DOFade(1f, expandFadeDuration).From(0f);
			}
		}
		else//close
		{
			settingsButton.Trans.DORotate(Vector3.forward * 180f, collapseRotationDuration).From(Vector3.zero).SetEase(collapseRotationEase);

			for(int i = 0; i < buttons.Length; i++)
			{
				//buttons[i].Trans.anchoredPosition = settingsButtonPosition;
				buttons[i].Trans.DOAnchorPos(settingsButtonPosition, collapseDuration).SetEase(collapseEase);
				buttons[i].Image.DOFade(0f, collapseDuration);
			}
		}


	}


	private void OnDestroy()
	{
		settingsButton.Button.onClick.RemoveAllListeners();
	}
}
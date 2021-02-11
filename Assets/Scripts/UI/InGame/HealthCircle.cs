using UnityEngine;
using UnityEngine.UI;

public class HealthCircle : MonoBehaviour
{
	private float fillAmount = 1f;
    public float FillAmount 
	{
		set
		{
			fillAmount = value;
			circle.fillAmount = fillAmount;
			particleHolder.localRotation = Quaternion.Euler(new Vector3(0f, 0f, fillAmount * 360f));

			if(fillAmount == 0)
			{
				animator.enabled = false;
				brokenHeart.enabled = true;
			}
			else
			{
				animator.enabled = true;
				brokenHeart.enabled = false;
			}
		}
		get
		{
			return fillAmount;
		}
	}
	[SerializeField] private Animator animator;
	[SerializeField] private Image circle;
	[SerializeField] private Image brokenHeart;
	[SerializeField] private RectTransform particleHolder;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EducationLevel0 : MonoBehaviour
{
	[SerializeField] private EducationDialog dialogJoystick;
	[SerializeField] private EducationDialog dialogYou;
	[SerializeField] private EducationDialog dialogCreation;
	[SerializeField] private EducationDialog dialogEnemies;
	[SerializeField] private EducationDialog dialogProtect;
	[SerializeField] private EducationDialog dialogSpells;

	private bool wait = true;

	private void Awake()
	{
		UIManager.Instance.isEducation = true;
		UIManager.Instance.onStart = StartEducation;

		UIManager.Instance.hellishFrost.isAwakeOpen = false;
		UIManager.Instance.fireWall.isAwakeOpen = false;
		UIManager.Instance.thunderStorm.isAwakeOpen = false;
	}

	private void StartEducation()
	{
		StartCoroutine(Education());
	}
	private IEnumerator Education()
	{
		UIManager.Instance.joystick.isBlock = true;
		UIManager.Instance.hellishFrost.IsBlock = true;
		UIManager.Instance.fireWall.IsBlock = true;
		UIManager.Instance.thunderStorm.IsBlock = true;


		dialogYou.Animator.SetTrigger("Show");
		yield return new WaitForSeconds(2);
		dialogYou.Animator.SetTrigger("Hide");
		yield return new WaitForSeconds(1);
		dialogCreation.Animator.SetTrigger("Show");
		yield return new WaitForSeconds(2);
		dialogCreation.Animator.SetTrigger("Hide");
		yield return new WaitForSeconds(1);


		UIManager.Instance.joystick.onTap = delegate {
			dialogJoystick.Animator.SetTrigger("Accept");

			UIManager.Instance.joystick.onTap = null;

			wait = false;
		};

		dialogJoystick.Animator.SetTrigger("Show");

		UIManager.Instance.joystick.isBlock = false;
		
		while(wait)
		{
			yield return null;
		}


		SpawnManager.Instance.StartWaves();
		dialogEnemies.Animator.SetTrigger("Show");
		yield return new WaitForSeconds(3f);
		dialogEnemies.Animator.SetTrigger("Hide");
		dialogProtect.Animator.SetTrigger("Show");
		yield return new WaitForSeconds(3f);
		dialogProtect.Animator.SetTrigger("Hide");

		yield return new WaitForSeconds(6f);
		dialogSpells.Animator.SetTrigger("Show");


		UIManager.Instance.hellishFrost.onTap = delegate {

			dialogSpells.Animator.SetTrigger("Accept");

			UIManager.Instance.hellishFrost.onTap = null;
			UIManager.Instance.fireWall.onTap = null;
			UIManager.Instance.thunderStorm.onTap = null;
		};
		UIManager.Instance.fireWall.onTap = delegate {

			dialogSpells.Animator.SetTrigger("Accept");

			UIManager.Instance.hellishFrost.onTap = null;
			UIManager.Instance.fireWall.onTap = null;
			UIManager.Instance.thunderStorm.onTap = null;
		};
		UIManager.Instance.thunderStorm.onTap = delegate {

			dialogSpells.Animator.SetTrigger("Accept");

			UIManager.Instance.hellishFrost.onTap = null;
			UIManager.Instance.fireWall.onTap = null;
			UIManager.Instance.thunderStorm.onTap = null;
		};

		UIManager.Instance.hellishFrost.door.OpenDoor();
		UIManager.Instance.fireWall.door.OpenDoor();
		UIManager.Instance.thunderStorm.door.OpenDoor();

		UIManager.Instance.hellishFrost.IsBlock = false;
		UIManager.Instance.fireWall.IsBlock = false;
		UIManager.Instance.thunderStorm.IsBlock = false;
	}
}
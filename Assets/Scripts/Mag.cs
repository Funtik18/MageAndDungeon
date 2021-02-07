using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour
{
    public int lifeCount=3;

    public void TakeDamage()
	{
        if(lifeCount == 0)
            Time.timeScale = 0;
        lifeCount--;
        GameController.Instance.hpDecrease(1);
        Debug.Log("Жизней осталось " + lifeCount);
    }
}

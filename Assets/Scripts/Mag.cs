using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : MonoBehaviour
{
    public int LifeCount=3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (LifeCount == 0)
                Time.timeScale = 0;
            LifeCount--;
            Debug.Log("Жизней осталось " + LifeCount);
            Destroy(other.gameObject);
        }
    }
}

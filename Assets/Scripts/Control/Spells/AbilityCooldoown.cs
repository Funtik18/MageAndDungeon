using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldoown : MonoBehaviour
{
    public Image abilityImage;
    public JoyButton but;
    float cooldown;
    [HideInInspector]
    public bool isCooldown = false;

    void Start()
    {
        abilityImage.fillAmount = 0;
        cooldown = but.coolDown;
    }

    public void StartCooldown()
    {
        isCooldown = true;
        abilityImage.fillAmount = 1;
    }

    private void Update()
    {
        if (isCooldown)
        {
            abilityImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (abilityImage.fillAmount <= 0)
            {
                abilityImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

}

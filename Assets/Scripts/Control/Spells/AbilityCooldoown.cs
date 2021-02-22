using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityCooldoown : MonoBehaviour
{
    public UnityAction onCoolDownPassed;

    public Image abilityImage;
    public JoyButton but;
    float cooldown;
    [HideInInspector]
    public bool isCooldown = false;

    void Start()
    {
        abilityImage.fillAmount = 0;
    }

    public void StartCooldown()
    {
        cooldown = but.coolDown;
        isCooldown = true;
        abilityImage.fillAmount = 1;
    }

    private void Update()
    {
        if (isCooldown)
        {
            Debug.Log(cooldown);
            abilityImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (abilityImage.fillAmount <= 0)
            {
                abilityImage.fillAmount = 0;
                isCooldown = false;

                onCoolDownPassed?.Invoke();
            }
        }
    }

}

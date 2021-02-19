using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public enum SpellsName
{
    spellHellishFrost,
    spellPunishingFist,
    spellThunderStorm
}

public class JoyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public JoyButtonDoor door;

    [HideInInspector] public bool isAwakeOpen = true;

    public UnityAction onTap;

    public SpellsName spellName;
    public float spelDuration;
    public float coolDown;
    public float damage;
    public float radius;
    public bool haveSound;

    AudioSound mySound;

    AbilityCooldoown myCooldown;

    private bool isBlock = false;
    public bool IsBlock
    {
        set
        {
            isBlock = value;
            Fader.CanvasGroup.interactable = !isBlock;
            Fader.CanvasGroup.blocksRaycasts = !isBlock;
        }
        get
        {
            return isBlock;
        }
    }

    private Fader fader;
    public Fader Fader
    {
        get
        {
            if (fader == null)
            {
                fader = GetComponent<Fader>();
            }
            return fader;
        }
    }

    private PlayerStats stats;
    private PlayerStats Stats
    {
        get
        {
            if (stats == null)
            {
                stats = GameManager.Instance.Stats;
            }
            return stats;
        }
    }

    [SerializeField] private AudioJoyButton audioSound;

    
    public GameObject spel;
    private GameObject currentSpell;

    private bool Pressed;




    public void StartOpenButton()
    {
        if(isAwakeOpen)
            door.OpenDoor();

        Fader.FadeIn();
        IsBlock = isBlock;
        GetStats();
        if (haveSound)
            mySound = GetComponent<AudioSound>();
        myCooldown = GetComponentInChildren<AbilityCooldoown>();

        myCooldown.onCoolDownPassed = delegate { audioSound.SetAudioOnAmountReady(); audioSound.PlayAudio(); };
    }

    void GetStats()
    {
        switch (spellName)
        {
            case SpellsName.spellPunishingFist:
                spelDuration = Stats.SpellPunishingFist.durability;
                damage = Stats.SpellPunishingFist.damage;
                coolDown = Stats.SpellPunishingFist.cooldown;
                radius = Stats.Radius;
                break;
            case SpellsName.spellHellishFrost:
                spelDuration = Stats.SpellHellishFrost.durability;
                coolDown = Stats.SpellHellishFrost.cooldown;
                radius = Stats.Radius;
                break;
            case SpellsName.spellThunderStorm:
                spelDuration = Stats.SpellThunderStorm.durability;
                damage = Stats.SpellThunderStorm.damage;
                coolDown = Stats.SpellThunderStorm.cooldown;
                radius = Stats.Radius;
                break;
        }
    }


    private IEnumerator SpellCasting(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        foreach (var item in currentSpell.GetComponentsInChildren<ParticleSystem>())
        {
            item.Stop();
        }
        yield return new WaitForSecondsRealtime(sec);
        Destroy(currentSpell);
        if (haveSound)
            mySound.PauseAudio();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isBlock) return;
        if (!myCooldown.isCooldown)
        {

            audioSound.SetAudioOnClick();
            audioSound.PlayAudio();

            if (haveSound)
                mySound.PlayAudio();

            myCooldown.StartCooldown();
            Pressed = true;
            currentSpell = Instantiate(spel) as GameObject;
            SetStats();
            StartCoroutine(SpellCasting(spelDuration));

            onTap?.Invoke();
        }
    }

    void SetStats()
    {
        switch (spellName)
        {
            case SpellsName.spellPunishingFist:
                currentSpell.GetComponent<FireWall>().radius = radius;
                foreach (var item in currentSpell.GetComponentsInChildren<ParticleSystem>())
                {
                    var newShape = item.shape;
                    newShape.radius = radius;
                }

                break;
            case SpellsName.spellHellishFrost:
                currentSpell.GetComponent<HellishFrost>().radius = radius;
                currentSpell.GetComponent<HellishFrost>().spelDuration = spelDuration;
                break;
            case SpellsName.spellThunderStorm:
                currentSpell.GetComponent<LightingStrike>().maxStrikes = spelDuration;
                break;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isBlock) return;
        Pressed = false;
    }


    [ContextMenu("OpenButton")]
    private void OpenButton()
    {
        Fader.CanvasGroup.alpha = 1;
        Fader.CanvasGroup.interactable = true;
        Fader.CanvasGroup.blocksRaycasts = true;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
    [ContextMenu("CloseButton")]
    private void CloseButton()
    {
        Fader.CanvasGroup.alpha = 0;
        Fader.CanvasGroup.interactable = false;
        Fader.CanvasGroup.blocksRaycasts = false;
#if UNITY_EDITOR
        EditorUtility.SetDirty(gameObject);
#endif
    }
}
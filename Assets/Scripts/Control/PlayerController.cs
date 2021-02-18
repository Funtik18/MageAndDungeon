using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public UnityEvent onHit;

    [SerializeField] private Transform model;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private ParticleSystem deathParticle;

    private Transform wizard;
    private Transform Wizard
	{
		get
		{
            if(wizard == null)
			{
                Wizard w = GameManager.Instance.WizardTarget;
                w.onDeath += Death;
                w.onReborn += Reborn;
                wizard = w.transform;

            }
            return wizard;
		}
	}

    private PlayerStats stats;
    private PlayerStats Stats
	{
		get
		{
            if(stats == null)
			{
                stats = GameManager.Instance.Stats;
            }
            return stats;
		}
	}

    private AudioPlayerPunch audioSound;
    public AudioPlayerPunch AudioSound
	{
		get
		{
            if(audioSound == null)
			{
                audioSound = GetComponent<AudioPlayerPunch>();
			}
            return audioSound;
		}
	}

    private Joystick joyStick;
    private JoyButton joyButton;

    Vector3 mov;
    Vector3 oldPos;

    private void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();
    }

    void Update()
    {
        rb.velocity = Vector3.zero;

        oldPos = transform.position;

        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical);
        Vector3 newPos = transform.position + movement;

        Vector3 offset = newPos - Wizard.position;

        mov = Wizard.position + Vector3.ClampMagnitude(offset, Stats.Radius);

        oldPos.y = 0;
        mov.y = 0;

        transform.Translate((mov - oldPos) * Stats.Speed * Time.deltaTime);
    }

	private void OnCollisionEnter(Collision collision)
	{
        GameObject refCol = collision.gameObject;
        if(refCol.layer == 9)//layer enemy
		{
            onHit?.Invoke();

            refCol.transform.root.GetComponent<Entity>().TakeDamage(Stats.Damage);

            //Vector3 OriginalScale = model.localScale; 
            //DOTween.Sequence().Append(model.DOScale(new Vector3(OriginalScale.x + 0.5f, OriginalScale.y + 0.5f, OriginalScale.z + 0.5f), 0.2f).SetEase(Ease.Linear)).Append(model.DOScale(OriginalScale, 0.2f).SetEase(Ease.Linear));
        }
    }

    [ContextMenu("P")]
    private void Death()
	{
        deathParticle.Play();

        model.DOScale(0, 1).From(1);
    }
    [ContextMenu("R")]
    private void Reborn()
	{
        model.DOScale(1, 0.5f).From(0);
    }
}
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent onHit;

    private Transform wizard;
    private Transform Wizard
	{
		get
		{
            if(wizard == null)
			{
                wizard = GameManager.Instance.WizardTarget.transform;
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
    private Rigidbody rb;

    Vector3 mov;
    Vector3 oldPos;

    private void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();

        rb = GetComponent<Rigidbody>();
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
        }
	}
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerSetup playerSetup;
    private PlayerStats stats;
    public PlayerStats Stats
	{
		get
		{
            if(stats == null)
			{
                stats = new PlayerStats(playerSetup);
			}
            return stats;
		}
	}

    [SerializeField] private Wizard wizard;

    private Joystick joyStick;
    private JoyButton joyButton;
    private Rigidbody rb;

    Vector3 mov;
    Vector3 oldPos;

    private void Awake()
    {
        wizard = GameManager.Instance.WizardTarget;
    }

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

        Vector3 offset = newPos - wizard.transform.position;

        mov = wizard.transform.position + Vector3.ClampMagnitude(offset, Stats.Radius);

        oldPos.y = 0;
        mov.y = 0;

        transform.Translate((mov - oldPos) * Stats.Speed * Time.deltaTime);
    }
}
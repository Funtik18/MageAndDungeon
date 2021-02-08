using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Wizard wizard;

    Joystick joyStick;
    JoyButton joyButton;
    Rigidbody rb;

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
        Vector3 oldPos = transform.position;

        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical);
        Vector3 newPos = transform.position + movement;

        Vector3 offset = newPos - wizard.transform.position;
        Vector3 mov= wizard.transform.position + Vector3.ClampMagnitude(offset, wizard.radius);

        oldPos.y = 0;
        mov.y = 0;

        rb.velocity = (mov-oldPos)*speed*Time.fixedDeltaTime;
    }
}
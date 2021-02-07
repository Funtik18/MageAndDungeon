using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    Joystick joyStick;
    JoyButton joyButton;

    Rigidbody rb;

    private void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
        joyButton = FindObjectOfType<JoyButton>();

        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        rb.velocity = new Vector3(joyStick.Horizontal * speed * Time.deltaTime, rb.velocity.y, joyStick.Vertical * speed * Time.deltaTime);
    }
}

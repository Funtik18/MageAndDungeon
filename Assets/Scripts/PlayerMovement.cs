using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Transform centerPt;

    Joystick joyStick;
    JoyButton joyButton;
    public float radius;
    Rigidbody rb;

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

        Vector3 offset = newPos - centerPt.position;
        Vector3 mov= centerPt.position + Vector3.ClampMagnitude(offset, radius);

        oldPos.y = 0;
        mov.y = 0;

        rb.velocity = (mov-oldPos)*speed*Time.fixedDeltaTime;
    }

    private int vertexCount = 40;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        Vector3 oldPos = centerPt.position + (centerPt.right * radius);

        for (int i = 0; i < vertexCount + 1; i++)
        {
            Vector3 pos = new Vector3(radius * Mathf.Cos(theta),0, radius * Mathf.Sin(theta));
            Vector3 newPos = centerPt.position + pos;

            Gizmos.DrawLine(oldPos, newPos);

            oldPos = newPos;

            theta += deltaTheta;
        }
    }
}

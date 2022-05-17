using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float inputX;
    float inputY;
    public float speed;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        if (inputY != 0.0f || inputX != 0.0f)
        {
            Vector3 dir = inputY * transform.forward + inputX * transform.right;
            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        } else
        {
        }

        //move();
    }

    public void move()
    {
        float Y = Input.GetAxis("Vertical");
        float X = Input.GetAxis("Horizontal");

        if (Y > 0.0f)
        {
        } 
        else if (Y < 0.0f)
        {
        } 
        else
        {

        }
    }
}

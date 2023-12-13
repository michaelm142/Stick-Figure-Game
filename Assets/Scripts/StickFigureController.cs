using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StickFigureController : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    public float maxSpeed = 30.0f;

    private Animator animator;

    private SpriteRenderer sprite;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float speed = Mathf.Abs(horizontal);

        controller.Move(Vector3.right * walkSpeed * horizontal * Time.deltaTime);
        // transform.position += horizontal * Vector3.right * walkSpeed * Time.deltaTime;
        //isOnGround = Physics.Raycast(transform.position, Vector3.down, 1.0f);
        //if (isOnGround)
        //{
        //    Vector3 moveVector = Vector3.ClampMagnitude(Vector3.right * horizontal, maxSpeed);
        //    Debug.Log(moveVector);
        //    body.AddForce(moveVector, ForceMode.VelocityChange);
        //}

        animator.SetFloat("Speed", speed);
        if (horizontal < 0.0f)
            sprite.flipX = true;
        if (horizontal > 0.0f)
            sprite.flipX = false;
    }
}

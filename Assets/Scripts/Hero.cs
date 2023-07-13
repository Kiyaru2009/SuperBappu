using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15f;
    private bool isGrounded = false;
    private float isGroundedColliderRadius;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private TerrainCollider groundCollider;
    private BoxCollider2D heroCollider;

    public static Hero Instance { get; set; } 


    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        Debug.Log("I've waked up");
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        heroCollider = GetComponent<BoxCollider2D>();
        groundCollider = GetComponentInChildren<TerrainCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I've started");

        isGroundedColliderRadius = heroCollider.size.x / 2;
        Debug.Log($"Grounded collider is {isGroundedColliderRadius}");
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            State = States.idle;
        }
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Run()
    {
        if (isGrounded)
        {
            State = States.run;
        }

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void CheckGround()
    {
        //Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        Collider2D[] collider = Physics2D.OverlapCircleAll(groundCollider.transform.position, isGroundedColliderRadius);
        isGrounded = collider.Length > 1;

        if (!isGrounded)
        {
            State = States.jump;
        }
    }
    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }
}
public enum States
{
    idle = 0,
    run = 1,
    jump = 2,
}
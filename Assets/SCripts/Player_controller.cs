using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed= 5f;
    float horizontal_input;

    //Animation
    [SerializeField] private Animator anim;
    private enum State {Idle, Running, Jumping, Falling};
    private State state = State.Idle;

    [SerializeField] float jump_force = 5f;
    bool facing_right = true;
    bool jump_input = false;
    bool isgrounded = false;
    [SerializeField] LayerMask ground_layer;
    [SerializeField] Transform ground_check_collider;
    [SerializeField] float ground_check_radius;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal_input = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jump_input = true;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jump_input = false;
        }
    }

    //runs a fixed amount of times per second
    private void FixedUpdate()
    {
        Groundcheck();
        Move(horizontal_input);
    }

    void Move(float dir)
    {
        Jump();
        float xval = dir * speed;
        Vector2 new_v = new Vector2(xval,rb.velocity.y);
        rb.velocity = new_v;
        if ((facing_right && dir<0f)||(!facing_right && dir>0f))
        {
            transform.Rotate(0f, 180f, 0f);
            facing_right = !facing_right;
        }

        VelocityState();
        anim.SetInteger("State", (int)state);
    }

    void VelocityState()
    {
        if(state == State.Jumping){
            if(rb.velocity.y < 0.1f){
                state = State.Falling;
            }
        } else if (state == State.Falling){
            if(isgrounded){
                state = State.Idle;
            }
        } else if (Mathf.Abs(rb.velocity.x) >= 0.01f){
            //left = negative, right = positive
            state = State.Running;
        } else {
            //Idle
            state = State.Idle;
        }
    }

    void Jump()
    {
        if(isgrounded && jump_input)
        {
            Vector2 new_v = new Vector2(rb.velocity.x, jump_force);
            jump_input = false;
            rb.velocity = new_v;
            state = State.Jumping; //2
        }
    }

    void Groundcheck()
    {
        isgrounded=Physics2D.OverlapCircle(ground_check_collider.position, ground_check_radius, ground_layer);
    }
}

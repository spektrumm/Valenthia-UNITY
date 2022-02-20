using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigB;
    private Animator anim;
    private SpriteRenderer sprite;

    // INITIALIZE SCRIPT VARIABLES
    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    private bool weapDrawn = false;
    private bool sheathed = true;

    private enum MovementState { idle, running, jumping, falling, crouching, sliding, standing }
    private enum AttackState { sheathed, drawn, atk1, atk2, atk3 }
    

    // Start is called before the first frame update
    void Start()
    {
        rigB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        rigB.velocity = new Vector2(dirX * moveSpeed, rigB.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rigB.velocity = new Vector2(rigB.velocity.x, jumpForce);

        }

        // CHECK IF X HAS BEEN PRESSED AKA IF WEAPON WANTS TO BE DRAWN
        if (Input.GetKeyDown(KeyCode.X) && weapDrawn == false) // doesnt work?
        {
            weapDrawn = true;
            sheathed = false;
            Debug.Log("X Hit when sword Sheathed, Sword now Drawn.");

        } // CHECK IF X HAS BEEN PRESSED WHILST WEAPON IS DRAWN, AKA WEAPON TO BE SHEATHED
        else if (Input.GetKeyDown(KeyCode.X) && weapDrawn == true)
        {
            sheathed = true;
            weapDrawn = false;
            Debug.Log("X Hit when sword Drawn, Sword now Sheathed");

        }
        else // LOG FOR DEBUG PURPOSES
        {
            Debug.Log("Draw/Sheath check passsed, no changes.");
        }
        
        // CALL ANIMATION UPDATER
        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovementState mState;
        AttackState aState;

        // ATTACK STATE CODE
        if (weapDrawn == true){
            aState = AttackState.drawn;
        }else if (sheathed == true)
        {
            aState = AttackState.sheathed;
        }
        else
        {
            aState = AttackState.sheathed;
        }

        // RUNNING MOVE STATE CODE
        if (dirX > 0f)
        {
            mState = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            mState = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            mState = MovementState.idle;
        }

        //JUMPING MOVE STATE CODE
        if (rigB.velocity.y > 0.1f)
        {
            mState = MovementState.jumping;
        }else if (rigB.velocity.y < -0.1f)
        {
            mState = MovementState.falling;
        }

        // UPDATE STATES AT METHOD END
        anim.SetInteger("MoveState", (int)mState);
        anim.SetInteger("AtkState", (int)aState);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigB;
    
    // Start is called before the first frame update
    void Start()
    {
        rigB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        
        rigB.velocity = new Vector2(dirX * 6f, rigB.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
           rigB.velocity = new Vector2(rigB.velocity.x, 5f);
        }
        
    }
}

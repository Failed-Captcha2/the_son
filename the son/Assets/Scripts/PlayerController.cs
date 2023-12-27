using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D playerRB;
    public float playerSpeed;
    private Vector3 move;
    public Animator animator;
    public SpriteRenderer sprite;
    public Sprite defaultSprite;
    private bool walking;
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {   
        //key input
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //converst key input into vector

        //mouse input
         if(Input.GetMouseButton(0)){
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (worldPosition.x< player.transform.position.x-0.2){
                move.x = -1;
            }else if (worldPosition.x> player.transform.position.x+0.2){
                move.x = 1;
            }
            if (worldPosition.y< player.transform.position.y-0.2){
                move.y = -1;
            }else if (worldPosition.y> player.transform.position.y+0.2){
                move.y = 1;
            }
        }

        //animation 
        if((!move.Equals(Vector3.zero)||Input.GetMouseButton(0)) && !walking){
            animator.Play("walking",0,0);
            walking = true;

        }else if(move.Equals(Vector3.zero) && !Input.GetMouseButton(0) && walking){
            animator.Play("standing",0,0);
            sprite.sprite = defaultSprite;
            walking = false;
        }

        //flip sprite
        if(move.x<0 && sprite.flipX){
            sprite.flipX = false;
        }else if(move.x>0 && !sprite.flipX){
            sprite.flipX = true;
        }
        
        //change position
        move = move*playerSpeed;
        playerRB.velocity = move;
        player.transform.position = new Vector3(player.position.x,player.position.y,player.position.y);
            // move.x = player.position.x + move.x;
            // move.y = player.position.y + move.y;
            // move.z = move.y;
            // player.transform.position = move;
    }
}

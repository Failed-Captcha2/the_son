using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public Transform player;
    public float playerSpeed;
    private Vector2 move;
    public Animator animator;
    private bool walking;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //converst key input into vector

        if(!move.Equals(new Vector2(0,0)) && !walking){
            //animate player
            animator.Play("walking",0,0);
            walking = true;
        }else if(move.Equals(new Vector2(0,0)) && walking){
            animator.Play("standing",0,0);
            walking = false;
        }
        
        move = move*Time.deltaTime*playerSpeed;
            player.transform.position = new Vector3(player.position.x + move.x, player.position.y + move.y, player.position.y + move.y);
    }
}

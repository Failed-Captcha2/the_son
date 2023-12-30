using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public Sprite defaultSprite;
    public bool playSound;

    private Transform player;
    public Rigidbody2D playerRB;
    private Animator animator;
    private SpriteRenderer sprite;
    private AudioSource sound;

    private bool walking;
    private Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        player.transform.position = Vector3.zero;
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //key input
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //converst key input into vector

        //mouse input
        if (Input.GetMouseButton(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (worldPosition.x < player.position.x - 0.2) { move.x = -1; }
            else if (worldPosition.x > player.position.x + 0.2) { move.x = 1; }

            if (worldPosition.y < player.position.y - 0.2) { move.y = -1; }
            else if (worldPosition.y > player.position.y + 0.2) { move.y = 1; }
        }

        //animation 
        if ((!move.Equals(Vector3.zero) || Input.GetMouseButton(0)) && !walking)
        {
            animator.Play("walking", 0, 0);
            walking = true;

        }
        else if (move.Equals(Vector3.zero) && !Input.GetMouseButton(0) && walking)
        {
            animator.Play("standing", 0, 0);
            sprite.sprite = defaultSprite;
            walking = false;
        }

        if (playSound) { sound.Play(); } //walking audio

        //flip sprite
        if (move.x < 0 && sprite.flipX)
        {
            sprite.flipX = false;
        }
        else if (move.x > 0 && !sprite.flipX)
        {
            sprite.flipX = true;
        }

        //change position
        move = move * playerSpeed;
        playerRB.velocity = move;
        player.transform.position = new Vector3(player.position.x, player.position.y, player.position.y);

    }
}

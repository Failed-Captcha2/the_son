using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Sprite[]  birdSprites;
    private SpriteRenderer mainSprite;
    private Rigidbody2D birdRB;

    public  int birdNum;
    public float speed;
    public float maxPixels;
    private System.Random rand;
    private float  waitTime;
    private float  currentTime;

    // Start is called before the first frame update
    void Start()
    {        //randomize bird
        rand = new System.Random();
        birdNum = rand.Next(0, birdSprites.Length);
        mainSprite = GetComponent<SpriteRenderer>();
        mainSprite.sprite = birdSprites[birdNum];

        //get max pixel from sprite dimensions, use whichever dimension is greater
        if (mainSprite.sprite.rect.height > mainSprite.sprite.rect.width) {
            maxPixels = mainSprite.sprite.rect.height;
        } else{
            maxPixels = mainSprite.sprite.rect.width;
        }

        birdRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= waitTime)
        {
            birdRB.velocity = new Vector3(rand.Next(-2, 3), rand.Next(-2, 3), 0) * speed;
            currentTime = 0;
            waitTime = rand.Next(0, 13);
        }
    }
}

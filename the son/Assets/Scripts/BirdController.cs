using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Sprite[]  birdSprites;
    private int  birdNum;
    private SpriteRenderer mainSprite;

    // Start is called before the first frame update
    void Start()
    {
        //randomize bird
        System.Random rand = new System.Random();
        birdNum = rand.Next(0, birdSprites.Length - 1);
        mainSprite = GetComponent<SpriteRenderer>();
        mainSprite.sprite = birdSprites[birdNum];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

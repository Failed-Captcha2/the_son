using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BirdBook : MonoBehaviour
{
    public SpriteRenderer picture;
    public Sprite[] sprites;
    public TextMeshPro label;
    public GameObject leftButton;

    public string[] birdNames;
    public int birdNum;

    // Start is called before the first frame update
    void Start()
    {
        birdNum = 0;

        //create array of bird names
        birdNames = new string[sprites.Length];
        for (int i = 0; i < birdNames.Length; i++)
        {
            birdNames[i] = sprites[i].name;
        }

        picture.sprite = sprites[0];
        label.text = birdNames[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        updatePage(birdNum+1);
    }

    public void updatePage(int num)
    {
        //update image and label
        birdNum = num;
        picture.sprite = sprites[birdNum];
        label.text = birdNames[birdNum];

        //set right button active/inactive
        if (birdNum == sprites.Length - 1) { gameObject.SetActive(false); }
        else { gameObject.SetActive(true); }
        //set left button active/inactive
        if (birdNum == 0) { leftButton.SetActive(false); }
        else{ leftButton.SetActive(true); }
    }

}

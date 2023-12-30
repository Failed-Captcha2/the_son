using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdBookLeftButton : MonoBehaviour
{
    public BirdBook book;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        book.updatePage(book.birdNum - 1);
    }
}

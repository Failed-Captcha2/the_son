using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public BirdBook book;
    public CardDeckView cardView;
    public int direction; //-1 for left, +1 for right
    
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
        if (book != null)
        {
            book.updatePage(book.birdNum - 1);
        }
        else if (cardView != null){
            cardView.updatePage(direction);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cardDeckView : MonoBehaviour
{
    public GameObject card;

    private Transform cardDeckViewPos;
    private int totalCards;
    public GameObject[] cards;

    // Start is called before the first frame update
    void Start()
    {
        cardDeckViewPos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObtainCard(Sprite sprite, string name){
        //create new card
        GameObject newCard = Instantiate(card, cardDeckViewPos); //create card

        Transform newCardPos = newCard.GetComponent<Transform>(); //position card
        newCardPos.transform.localPosition = new Vector3(0, 0, -0.5f);
        newCardPos.transform.localScale = Vector3.one;

        newCard.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite; //change sprite
        newCard.gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = name; //change name
        Destroy(newCard.gameObject.transform.GetChild(3).gameObject); //remove obtained text
        totalCards++;

        // add new card to array
        GameObject[] cardsCopy = cards;
        cards = new GameObject[totalCards];
        for(int i=0; i<totalCards-1; i++){
            cards[i] = cardsCopy[i];
        }
        cards[totalCards-1] = newCard;
    }
}

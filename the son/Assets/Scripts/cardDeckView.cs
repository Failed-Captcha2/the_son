using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardDeckView : MonoBehaviour
{
    public GameObject card;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public int lastCard;
    public int num; // first bird card visible in book

    private Transform cardDeckViewPos;
    public int totalCards;
    private float[] xPositions = {-1.6f, -0.53f, 0.53f, 1.6f};
    public GameObject[] cards;

    // Start is called before the first frame update
    void Start()
    {
        cardDeckViewPos = GetComponent<Transform>();

        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
        card.SetActive(false);

        totalCards = cards.Length;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ObtainCard(Sprite sprite, string name){
        //create new card
        GameObject newCard = Instantiate(card, this.transform); //create card

        Transform newCardPos = newCard.GetComponent<Transform>(); //position card
        newCardPos.transform.localScale = Vector3.one;

        newCard.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite; //change sprite
        newCard.gameObject.transform.GetChild(0).GetComponent<Transform>().transform.localScale = new Vector3(45/sprite.rect.height,45/sprite.rect.height,1); //change sprite
        newCard.gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = name; //change name
        newCard.SetActive(true);
        totalCards++;

        // add new card to array
        GameObject[] cardsCopy = cards;
        cards = new GameObject[totalCards];
        for(int i=0; i<totalCards-1; i++){
            cards[i] = cardsCopy[i];
        }
        cards[totalCards-1] = newCard;
        updatePage(0);
    }

    public void updatePage(int direction){

        //define first and last card
        num+= direction;
        if(num+3>=totalCards){ lastCard = totalCards-1; }
        else{ lastCard = num+3; }

        //set first and last card active/innactive
        if (direction  == 1) {
            cards[num - 1].SetActive(false);
            cards[lastCard].SetActive(true);
        }
        if (direction == -1) {
            if (totalCards > lastCard+1)  {cards[lastCard + 1].SetActive(false);}
            cards[num].SetActive(true);
        }

        //position active cards
        for (int i=0;i<=lastCard-num;i++){
            cards[i+num].GetComponent<Transform>().transform.localPosition = new Vector3(xPositions[i], 0, -0.5f);
        }

        //set arrows active/inactive
        leftArrow.SetActive(num!=0);
        rightArrow.SetActive(lastCard!=num);
    }
}

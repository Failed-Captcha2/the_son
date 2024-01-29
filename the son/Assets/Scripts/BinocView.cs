using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BinocView : MonoBehaviour
{
    public Transform cameraPos;
    public float cameraSpeed;
    public Vector3 originalPos;
    public float maxDistance;
    private Transform binocViewPos;
    private Vector3 move;

    public BirdBook book;

    //bird related variables
    public GameObject bird;
    public  Material pixelate;
    private BirdController birdController;

    //card variables
    public GameObject card;
    public CardDeckView cardDeck;
    public bool cardObtained;
    private Transform cardPos;
    private SpriteRenderer cardImage;
    private Transform cardImageTransform;
    private TextMeshPro cardLabel;
    private TextMeshPro obtainedLabel;
    private AudioSource tada;
   
    // Start is called before the first frame update
    void Start()
    {
        //get components
        binocViewPos = GetComponent<Transform>();
        tada = GetComponent<AudioSource>();

        //bird components
        pixelate = bird.GetComponent<SpriteRenderer>().material;
        birdController = bird.GetComponent<BirdController>();

        //card components
        cardPos = card.GetComponent<Transform>();
        cardImage = card.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        cardImageTransform = card.gameObject.transform.GetChild(0).GetComponent<Transform>();
        cardLabel = card.gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        obtainedLabel = card.gameObject.transform.GetChild(3).GetComponent<TextMeshPro>();

        cardPos.transform.localScale = new Vector3(0, 0, 1);
        card.SetActive(false);
        originalPos = binocViewPos.position;
        gameObject.SetActive(false);
        pixelate.SetFloat("_Pixelate", 2);

    }

    // Update is called once per frame
    void Update()
    {
        binocViewPos.transform.position = cameraPos.position + new Vector3(0, 0, 2); //folow camera with binoc view screen

        //gradually scale up card
        if (cardObtained && cardPos.localScale.x < .5)
        {
            cardPos.transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime;
        }

        //move with key input
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!move.Equals(Vector3.zero)&&!cardObtained) { moveCamera(); }
    }

    //move with touchscreen/ mouse input
    void OnMouseDrag()
    {
        if (!cardObtained)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //get x direction
            if (worldPosition.x < binocViewPos.position.x - 0.2) { move.x = -1; }
            else if (worldPosition.x > binocViewPos.position.x + 0.2) { move.x = 1; }

            //get y direction
            if (worldPosition.y < binocViewPos.position.y - 0.2) { move.y = -1; }
            else if (worldPosition.y > binocViewPos.position.y + 0.2) { move.y = 1; }

            moveCamera();
        }
    }

    void moveCamera() //move binoc view
    {
        //if reached max or min x change don't move in y direction
        if (binocViewPos.position.x + move.x * cameraSpeed * Time.deltaTime - originalPos.x > maxDistance ||
                originalPos.x - binocViewPos.position.x - move.x * cameraSpeed * Time.deltaTime > maxDistance)
        { move.x = 0; }

        //if reached max or min  y don't move in y direction
        if (binocViewPos.position.y + move.y * cameraSpeed * Time.deltaTime - originalPos.y > maxDistance ||
        originalPos.y - binocViewPos.position.y - move.y * cameraSpeed * Time.deltaTime > maxDistance)
        { move.y = 0; }

        //move 
        move = move * Time.deltaTime * cameraSpeed;
        binocViewPos.transform.position = new Vector3(binocViewPos.position.x + move.x, binocViewPos.position.y + move.y, binocViewPos.position.z);
        cameraPos.transform.position = new Vector3(binocViewPos.position.x, binocViewPos.position.y, cameraPos.position.z);

    }

    public void ObtainCardAnimation(Sprite sprite, string name)
    {
        Debug.Log("ggs");
        if (!tada.isPlaying) { tada.Play(); } //play sound
        card.SetActive(true); //show card

        //update bird sprite on card
        cardImage.sprite = sprite;
        cardImageTransform.localScale = new Vector3(45 / cardImage.sprite.rect.height, 45 / cardImage.sprite.rect.height, 1);

        //update card and obtained text to show correct bird name
        cardLabel.text = name;
        obtainedLabel.text = name + " Card Obtained!";

        //position bird card correctly
        cardPos.transform.position = new Vector3(cameraPos.position.x, cameraPos.position.y, -9);
        cardPos.transform.localScale = new Vector3(0, 0, 1);
        cardObtained = true;

        card.gameObject.transform.GetChild(4).gameObject.SetActive(true);
        for (int i = 0; i < cardDeck.names.Length; i++) {
            if (cardDeck.names[i].Equals(name)) {
                card.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                break;
            }
        }

        cardDeck.ObtainCard(sprite, name);
    }

    //detect when bird is in or out of view
    void OnTriggerEnter2D(Collider2D other) { other.GetComponent<BirdController>().birdInView = true; }
    void OnTriggerExit2D(Collider2D other) { other.GetComponent<BirdController>().birdInView = false; }

}

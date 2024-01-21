using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocView : MonoBehaviour
{
    public Transform cameraPos;
    public Material pixelate;
    public float pixelationSpeed;
    public float cameraSpeed;
    public SpriteRenderer bird;
    public BirdBook book;
    public BirdController birdController;
    public GameObject card;

    private Vector3 move;
    public Vector3 originalPos;
    public bool birdInView;
    public float maxDistance;
    public bool cardObtained;

    private Transform cardPos;
    private AudioSource tada;
    private Transform binocViewPos;
    // Start is called before the first frame update
    void Start()
    {
        binocViewPos = GetComponent<Transform>();
        tada = GetComponent<AudioSource>();
        cardPos = card.GetComponent<Transform>();

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

        //increase pixels if bird is within view
        if (birdInView && pixelate.GetFloat("_Pixelate") < 40) { pixelate.SetFloat("_Pixelate", pixelate.GetFloat("_Pixelate") + pixelationSpeed); }
        //decrease pixels if bird is out of view
        else if (!birdInView && pixelate.GetFloat("_Pixelate") > 2) { pixelate.SetFloat("_Pixelate", pixelate.GetFloat("_Pixelate") - 2 * pixelationSpeed); }

        //when bird is fully visible(not pixelated) and bird book matches, obtain a bird card
        if (birdInView && pixelate.GetFloat("_Pixelate") >= 39 && book.birdNum == birdController.birdNum && !cardObtained)
        {
            Debug.Log("ggs");
            if (!tada.isPlaying) { tada.Play(); } //play sound
            card.SetActive(true); //show card
            cardPos.transform.position = new Vector3(cameraPos.position.x+ 0.296f, cameraPos.position.y - 0.069f, -9);
            cardObtained = true;
        }

        //gradually scale up card
        if (cardObtained && cardPos.localScale.x < 1)
        {
            cardPos.transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime;
        }

        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //key input
        if (!move.Equals(Vector3.zero)) { moveCamera(); }
    }

    void OnMouseDrag() //touchscreen/mouse input
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

    void OnTriggerEnter2D(Collider2D other) { birdInView = true; }
    void OnTriggerExit2D(Collider2D other) { birdInView = false; }
}

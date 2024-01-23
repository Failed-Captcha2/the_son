using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularController : MonoBehaviour
{
    public GameObject player;
    public GameObject card;
    public Transform cameraTransform;
    public Camera mainCamera;
    public GameObject binocViewScreen;
    public BinocView binocViewScript;
    public BirdBook book;
    public Sprite[] sprites; //[0] is binocs.png, [1] is eye.png

    private Transform binocPos;
    private Transform playerPos;
    private SpriteRenderer viewSprite;
   public bool binocView;

    public float defaultSize = 1.5f;
    public float binocSize = 0.5f;
    public Vector3 defaultPos = new Vector3(2.75f, -1.15f, 1f);
    public Vector3 binocViewPos = new Vector3(1.17f, -0.68f, 1f);
    // Start is called before the first frame update
    void Start()
    {
        binocPos = GetComponent<Transform>();
        viewSprite = GetComponent<SpriteRenderer>();
        playerPos = player.GetComponent<Transform>();

        //set camera to default size
        mainCamera.orthographicSize = defaultSize;
        cameraTransform.localScale = new Vector3(defaultSize, defaultSize, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //move binoculars with camera (if made child of camra OnMouseDown stops working)
        if (!binocView)
        {
            binocPos.transform.position = cameraTransform.position + defaultPos;
        }
        else
        {
            binocPos.transform.position = cameraTransform.position + binocViewPos;
        }
    }

    void OnMouseDown()
    {

        if (binocView) { DefaultViewOn(); }
        else{ BinocViewOn(); }
    }

    public void DefaultViewOn(){
        viewSprite.sprite = sprites [0]; //change to binoc sprite

        //zoom out to default view
        mainCamera.orthographicSize = defaultSize;
        cameraTransform.localScale = new Vector3(defaultSize, defaultSize, 1f);

        player.SetActive(true);//show player
        card.SetActive(false);//hide card
        binocViewScreen.SetActive(false);//hide binocular view screen
        binocView = false;
        binocViewScript.cardObtained = false;
    }

    public void BinocViewOn(){
        player.SetActive(false);//hide player
        binocViewScreen.SetActive(true);//show binocular view screen
        viewSprite.sprite = sprites [1];//change to eye sprite
        book.updatePage(0);

        //zoom in to binoc view
        mainCamera.orthographicSize = binocSize;
        cameraTransform.localScale = new Vector3(binocSize, binocSize, 1f);
        cameraTransform.transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
        binocViewScript.originalPos = new Vector3(cameraTransform.position.x, cameraTransform.position.y, 0f);
        binocView = true;
    }
}

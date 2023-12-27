using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binnocularController : MonoBehaviour
{
    public GameObject player;
    public Transform cameraTransform;
    public Camera mainCamera;
    public Transform binnocPos;
    public SpriteRenderer viewSprite;
    public Sprite[] sprites; //[0] is binnocs.png, [1] is eye.png
    private float defaultSize = 1.5f;
    private float binnocSize = 0.5f;
    private bool binnocView;
    private Vector3 defaultPos = new Vector3(2.75f,-1.15f,1f);
    private Vector3 binnocViewPos = new Vector3(1.17f,-0.68f,1f);
    // Start is called before the first frame update
    void Start()
    {
        //set camera to default size
        mainCamera.orthographicSize = defaultSize;
        cameraTransform.localScale = new Vector3(defaultSize,defaultSize,1f);
    }

    // Update is called once per frame
    void Update()
    {   
        //move binnoculars with camera (if made child of camra OnMouseDown stops working)
        if(!binnocView){
            binnocPos.transform.position = cameraTransform.position + defaultPos;
        }else{
            binnocPos.transform.position = cameraTransform.position + binnocViewPos;
        }
    }

    void OnMouseDown(){
        
        if(binnocView){                         //switch to default view
            viewSprite.sprite = sprites[0]; //change to binnoc sprite

            //zoom out to default view
            mainCamera.orthographicSize = defaultSize;
            cameraTransform.localScale = new Vector3(defaultSize,defaultSize,1f);

            player.SetActive(true);//show player
            binnocView = false;

        }else{                                  //switch to binnoc view
            player.SetActive(false);//hide player
            viewSprite.sprite = sprites[1];//change to eye sprite

            //zoom in to binnoc view
            mainCamera.orthographicSize = binnocSize;
            cameraTransform.localScale = new Vector3(binnocSize,binnocSize,1f);
            binnocView = true;
        }
    }
}

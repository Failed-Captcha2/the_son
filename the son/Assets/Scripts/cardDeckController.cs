using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckController : MonoBehaviour
{
    public Vector3 defaultPos;
    public Transform cameraPos;
    public GameObject cardDeckView;
    public GameObject player;
    public GameObject binnocs;

    private Transform cardDeckPos;
    private Transform cardDeckViewPos;
    private bool cardDeckViewActive;
    // Start is called before the first frame update
    void Start()
    {
        cardDeckPos = GetComponent<Transform>();
        cardDeckViewPos = cardDeckView.GetComponent<Transform>();

        cardDeckView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cardDeckPos.transform.position = cameraPos.position + defaultPos;

    }

    void OnMouseDown(){
        if(!cardDeckViewActive)
        {
            cardDeckView.SetActive(true);
            cardDeckViewPos.transform.position = new Vector3(0, 0, 1.5f) + cameraPos.position;
            player.SetActive(false);
            binnocs.SetActive(false);

            cardDeckViewActive = true;
        }
        else
        {
            cardDeckView.SetActive(false);
            player.SetActive(true);
            binnocs.SetActive(true);

            cardDeckViewActive = false;
        }
    }
}

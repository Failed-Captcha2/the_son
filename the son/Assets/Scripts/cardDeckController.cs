using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardDeckController : MonoBehaviour
{
    public Vector3 defaultPos;
    public Transform cameraPos;
    private Transform cardDeckPos;
    // Start is called before the first frame update
    void Start()
    {
        cardDeckPos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        cardDeckPos.transform.position = cameraPos.position + defaultPos;

    }
}

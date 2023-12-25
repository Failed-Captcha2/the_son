using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform mainCamera;
    public float cameraSpeed;
    public Transform player;
    public float playerSpeed;
    private Vector3 move = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        mainCamera.transform.position += (move* Time.deltaTime * cameraSpeed);
        //move.z = move.y;
        player.transform.position += (move * Time.deltaTime * playerSpeed);
    }
}

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

    public Transform binocViewPos;
    private Vector3 move;
    public Vector3 originalPos;
    public bool birdInView;
    public float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        binocViewPos = GetComponent<Transform>();
        originalPos = binocViewPos.position;
        gameObject.SetActive(false);
        pixelate.SetFloat("_Pixelate", 2);
    }

    // Update is called once per frame
    void Update()
    {
        binocViewPos.transform.position = cameraPos.position + new Vector3(0, 0, 2); //folow camera with binoc view screen
        if (birdInView && pixelate.GetFloat("_Pixelate") < 40) { pixelate.SetFloat("_Pixelate", pixelate.GetFloat("_Pixelate") + pixelationSpeed); } //increase pixels if bird is within view
        else if (!birdInView && pixelate.GetFloat("_Pixelate") > 2) { pixelate.SetFloat("_Pixelate", pixelate.GetFloat("_Pixelate") - 2*pixelationSpeed); } //decrease pixels if bird is out of view

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

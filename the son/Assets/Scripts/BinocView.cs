using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocView : MonoBehaviour
{
    public Transform cameraPos;
    public Material pixelate;
    public float pixelationSpeed;

    private Transform binocViewPos;
    public bool birdInView;
    // Start is called before the first frame update
    void Start()
    {
        binocViewPos = GetComponent<Transform>();
        gameObject.SetActive(false);
        pixelate.SetFloat("_Pixelate", 2);
    }

    // Update is called once per frame
    void Update()
    {
        binocViewPos.transform.position = cameraPos.position + new Vector3(0, 0, 2); //folow camera with binoc view screen
        if (birdInView) { pixelate.SetFloat("_Pixelate", pixelate.GetFloat("_Pixelate") + pixelationSpeed); } //increase pixels if bird is within view
        else { pixelate.SetFloat("_Pixelate", pixelate.GetFloat("_Pixelate") - pixelationSpeed); } //decrease pixels if bird is out of view
    }

    void OnTriggerEnter2D(Collider2D other) { birdInView = true; }
    void OnTriggerExit2D(Collider2D other) { birdInView = false; }
}

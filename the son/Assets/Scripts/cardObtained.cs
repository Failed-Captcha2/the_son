using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObtained : MonoBehaviour
{
    public Transform cardTransform;
    public BinocularController binocController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown(){
        if(cardTransform.localScale.x >= 0.5){binocController.DefaultViewOn(); }
    }
}

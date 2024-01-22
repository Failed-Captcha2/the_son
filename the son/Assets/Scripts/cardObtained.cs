using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardObtained : MonoBehaviour
{
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
        binocController.DefaultViewOn();
    }
}

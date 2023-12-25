using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resolution : MonoBehaviour
{
    public Camera main_camera;
    // Start is called before the first frame update
    
    void Start()
    {
       Screen.SetResolution(2200,900,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

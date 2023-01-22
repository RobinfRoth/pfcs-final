using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Camera cam;
    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // on mouse button clicked
        if (Input.GetMouseButtonDown(0))
        {
            
        }

        // when holding the mouse button down
        if (Input.GetMouseButton(0))
        {
            
        }

        // on releasing the mouse button
        if (Input.GetMouseButtonUp(0)) 
        {
            
        }
    }
}

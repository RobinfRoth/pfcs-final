using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    Camera cam;
    LineRenderer lr;
    [SerializeField] AnimationCurve ac;

    Vector3 camOffset = new Vector3(0, -10, 10);

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // 0 = left mouse button

        // on mouse button clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (lr == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
            }
            lr.enabled = true;
            lr.widthCurve = ac;
            lr.material = new Material (Shader.Find("Sprites/Default"));
            lr.positionCount = 2;
            startPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) + camOffset);
            print("startPos = " + startPos);
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
            print("Mouse Down!");
            lr.numCapVertices = 10;
        }

        // when holding the mouse button down
        if (Input.GetMouseButton(0))
        {
            endPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) + camOffset);
            lr.SetPosition(1, endPos);
        }

        // on releasing the mouse button
        if (Input.GetMouseButtonUp(0)) 
        {
            lr.enabled = false;
        }
    }
}

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
    Player player;

    Vector3 camOffset = new Vector3(0, -10, 10);

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerIsAtStart = (player.transform.position == player.startPosition);
        // 0 = left mouse button

        // on mouse button clicked
        if (Input.GetMouseButtonDown(0) && playerIsAtStart)
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
            lr.SetPosition(0, startPos);
            lr.useWorldSpace = true;
            lr.numCapVertices = 10;
        }

        // when holding the mouse button down
        if (Input.GetMouseButton(0) && playerIsAtStart)
        {
            endPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) + camOffset);
            lr.SetPosition(1, endPos);
        }

        // on releasing the mouse button
        if (Input.GetMouseButtonUp(0) && playerIsAtStart) 
        {
            lr.enabled = false;
        }
    }
}

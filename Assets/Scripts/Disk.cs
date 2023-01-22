using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    private float dt;
    public Vector3 direction = new Vector3(0, 0, 0);
    public float mass = 1; // kg
    public float velocity = 0; // m/s

    // Start is called before the first frame update
    void Start()
    {
        dt = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // physics simulation
    void FixedUpdate()
    {        
        
    }
}

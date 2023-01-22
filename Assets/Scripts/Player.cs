using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float dt;
    public Vector3 startPosition = new Vector3(0, 0, 8);
    public Vector3 direction = new Vector3(1, 0, 0);
    public float mass = 2; // kg
    public float velocity = 10; // m/s
    public float charge = 10; // Coulomb

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

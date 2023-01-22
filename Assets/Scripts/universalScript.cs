using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class universalScript : MonoBehaviour
{
    float dt;
    Player player;
    Disk disk;
    Goal goal;

    DirectionIndicator directionIndicator;
    TextMeshProUGUI text;
    List<PointCharge1> allCharges;
    
    // Start is called before the first frame update
    void Start()
    {
        dt = Time.deltaTime;
        
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        disk = GameObject.FindWithTag("Disk").GetComponent<Disk>();
        goal = GameObject.FindWithTag("Goal").GetComponent<Goal>();

        directionIndicator = GameObject.FindWithTag("Direction Indicator").GetComponent<DirectionIndicator>();
        text = GameObject.FindWithTag("Win Text").GetComponent<TextMeshProUGUI>();

        PointCharge1 pc1 = GameObject.FindWithTag("Point charge positive 1").GetComponent<PointCharge1>();
        PointCharge1 pc2 = GameObject.FindWithTag("Point charge positive 2").GetComponent<PointCharge1>();
        PointCharge1 pc3 = GameObject.FindWithTag("Point charge negative 1").GetComponent<PointCharge1>();
        PointCharge1 pc4 = GameObject.FindWithTag("Point charge negative 2").GetComponent<PointCharge1>();


        allCharges = new List<PointCharge1> {pc1, pc2, pc3, pc4};
    }

    // physics simulation
    void FixedUpdate()
    {       
        // detect collision of Player and Disk 
        float distancePlayerToDisk = Vector3.Distance(player.transform.position, disk.transform.position);
        float distanceDiskToGoal = Vector3.Distance(disk.transform.position, goal.transform.position);

        // Collision player with puck
        if (distancePlayerToDisk < (player.GetComponent<Renderer>().bounds.size.x + disk.GetComponent<Renderer>().bounds.size.x)/2)
        {
            CollidePlayerWithDisk();
        }

        // disk enters Goal
        if (distancePlayerToDisk < (disk.GetComponent<Renderer>().bounds.size.x + goal.GetComponent<Renderer>().bounds.size.x)/2)
        {
            text.fontSize = 120;
            print("Goal");
        }

        UpdatePlayer();
        UpdateDisk();
    }

    // run once per frame
    void Update()
    {
        
    }

    private void CollidePlayerWithDisk()
    {
        // Velocity disk after collision
        disk.velocity = ((2*player.mass)/(player.mass + disk.mass)) * player.velocity;

        // Direction disk perpendicular to tangent at point of collision
        disk.direction = new Vector3(disk.transform.position.x - player.transform.position.x, 0, disk.transform.position.z - player.transform.position.z).normalized;
        Debug.Log("directionDisk: " + disk.direction);
            
        // Velocity player after the Collision (elastic)
        player.velocity = ((player.mass - disk.mass)/(player.mass + disk.mass)) * player.velocity;

        // new Direction player (perpendicular to disk direction)
        Vector3 axis = Vector3.Cross(disk.direction, Vector3.up);
        Debug.Log("axis: " + axis);
        player.direction = (Quaternion.AngleAxis(90, Vector3.up) * disk.direction).normalized;    
    } 

    private Vector3 CalculateCoulombForce(Player player, PointCharge1 pointCharge)
    {
        Vector3 coulombForce = new Vector3(0, 0, 0);
        float electricConstant = MathF.Pow(8.8541878128f, -12f);
        float scalarFactor = (player.charge * pointCharge.charge) / (4 * MathF.PI * electricConstant);
        float distancePlayerToCharge = Vector3.Distance(player.transform.position, pointCharge.transform.position);

        coulombForce = scalarFactor * (player.transform.position - pointCharge.transform.position).normalized / (distancePlayerToCharge * distancePlayerToCharge); 

        return coulombForce;
    }

    private void UpdatePlayer()
    { 
        var sumOfAllForces = new Vector3(0, 0, 0);

        foreach (PointCharge1 pc in allCharges) {
            var f = CalculateCoulombForce(player, pc);
            Debug.Log("PointCharge" + pc + ": " + f);
            sumOfAllForces += f;
        }

        Debug.Log(sumOfAllForces);

        player.transform.position += player.velocity * player.direction * (dt/2);
        player.direction += (sumOfAllForces / player.mass) * dt;
        player.transform.position += player.velocity * player.direction.normalized * (dt/2);
    }

    private void UpdateDisk() 
    {
        disk.transform.position += disk.velocity * disk.direction.normalized * dt;
    }
}
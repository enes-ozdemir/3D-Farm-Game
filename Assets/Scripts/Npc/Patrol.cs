using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float _waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int _randomSpot;

    private Rigidbody rb;

    private void Start()
    {
        _waitTime = startWaitTime;
        _randomSpot = Random.Range(0, moveSpots.Length);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var position = transform.position;
        
        Vector3 dir = position - moveSpots[_randomSpot].position;
        //transform.LookAt(dir* Time.deltaTime * 20f);
        position =
            Vector3.MoveTowards(position, moveSpots[_randomSpot].position, speed * Time.deltaTime);
        transform.position = position;

        this.transform.rotation = 
            Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(dir), 0.2f);
        
        
        //transform.rotation = Quaternion.LookRotation(rb.velocity);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);

        //&&transform.rotation = Quaternion.Slerp(Quaternion.Euler(moveSpots[_randomSpot].position.x, -90f,0f), Quaternion.LookRotation(moveSpots[_randomSpot].position), 2f * Time.deltaTime);
        // if (dir != Vector3.zero)
        // {
        //     transform.rotation = Quaternion.Slerp(
        //         transform.rotation,
        //         Quaternion.LookRotation(dir),
        //         Time.deltaTime * 1f
        //     );
        // }
        //transform.LookAt(transform.position + moveSpots[_randomSpot].position);
        // transform.Translate(dir * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(dir);
        // transform.rotation = Quaternion.LookRotation(Vector3.forward, dir.normalized);

        if (Vector3.Distance(transform.position, moveSpots[_randomSpot].position) < 0.4f)
        {
            if (_waitTime <= 0)
            {
                _randomSpot = Random.Range(0, moveSpots.Length);
                _waitTime = startWaitTime;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }
        //  transform.Translate(Vector3.forward * speed * Time.deltaTime);   

        // Vector3 movDir = new Vector3(moveSpots[_randomSpot].position.x, 0, moveSpots[_randomSpot].position.y);
        // movDir.Normalize();
        // transform.Translate(movDir * speed * Time.deltaTime, Space.World);
    }
}
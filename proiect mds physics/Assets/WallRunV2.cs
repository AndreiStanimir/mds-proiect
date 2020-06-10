using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunV2 : MonoBehaviour
{
    public bool isWallRunning = false;
    Vector3 wallDirection;
    RaycastHit rayInfo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(isWallRunning);
        if (isWallRunning)
            ComputeWallRuningImput();
        
    }

    private void ComputeWallRuningImput()
    {
        if (Input.GetKey(KeyCode.W))
            GetComponent<Rigidbody>().velocity -= wallDirection * Time.deltaTime * 100;
        if (Input.GetKey(KeyCode.S))
            GetComponent<Rigidbody>().velocity += wallDirection * Time.deltaTime * 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Physics.Raycast(transform.position, collision.transform.position, out rayInfo, 100);

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
            isWallRunning = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isWallRunning = true;
              print(rayInfo.point);
                      wallDirection = new Vector3(rayInfo.point.z, 4, rayInfo.point.x);
            wallDirection = new Vector3(rayInfo.normal.z, rayInfo.normal.y, rayInfo.normal.x);
            //print(wallDirection);
            //GetComponent<Rigidbody>().velocity += new Vector3 ;




        }
    }

}

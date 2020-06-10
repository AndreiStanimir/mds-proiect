using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] float suckPower = 35;
	Rigidbody Player;
    public float timeToWallRun = 1f;
    public float forwardBoost = 150f;
    public float upwardBoost = 100f;
    public float jumpForce = 1000f;
    float temp = 0;
    bool timeSwitch = true;
    bool isWallRunning = false;


    Vector3 v;
    Quaternion rot;

    //------------
    RaycastHit rayInfo;
    CameraMouse camera;
    //------------

    void Start()
	{
		Player = GetComponent<Rigidbody>();
        camera = transform.GetChild(0).GetComponent<CameraMouse>();
	}

    void Update()
    {
        Debug.Log(PlayerNewMovement.canGetVelocity);

        if (timeSwitch)
            temp = Time.time;
        
        if (PlayerNewMovement.isGrounded){
            timeSwitch = true;
            isWallRunning = false;
        }

        if (PlayerNewMovement.canGetVelocity){
            timeSwitch = true;
            isWallRunning = false;
        }

        print("isWallRunning: " + isWallRunning);
        
        if (isWallRunning)
        {


            rot = transform.GetChild(0).localRotation;
            if (Input.GetKey(KeyCode.A))
                camera.wallRunTilt-=0.7f;
            if (Input.GetKey(KeyCode.D))
                camera.wallRunTilt+=0.7f;
            camera.wallRunTilt = Mathf.Clamp(camera.wallRunTilt, -15f, 15f);


            if (Time.time < temp + timeToWallRun)
            {
                //directie = rayInfo.point - transform.position;
                
               // Player.velocity -= Vector3.Reflect(rayInfo.point - transform.position, rayInfo.normal) * Time.deltaTime * suckPower; //sugere






                //Player.velocity -= rayInfo.normal * Time.deltaTime * suckPower; //sugere

                if (timeSwitch)
                {
                    temp = Time.time;
                    v = transform.forward;
                }
                timeSwitch = false;
                Player.velocity += v * Time.deltaTime * forwardBoost + transform.up * Time.deltaTime * upwardBoost;
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A))
            {
                Player.velocity -= transform.right * Time.deltaTime * jumpForce + transform.up * Time.deltaTime * upwardBoost;

                isWallRunning = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D))
            {
                Player.velocity += transform.right * Time.deltaTime * jumpForce + transform.up * Time.deltaTime * upwardBoost;
                isWallRunning = false;

                
            }

            

        }
        else
        {
            if (camera.wallRunTilt >1.1f)
                camera.wallRunTilt--;
            if (camera.wallRunTilt <-1.1f)
                camera.wallRunTilt++;
        }

    }

    void OnCollisionEnter (Collision collision)
    {

        if (collision.collider.tag == "Wall" && !PlayerNewMovement.isGrounded)
        {
            isWallRunning = true;
        }
    }


}

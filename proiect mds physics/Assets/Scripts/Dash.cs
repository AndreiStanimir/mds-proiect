using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
	Rigidbody Player;
    float timeAfter1 = 0, timeAfter2 = 0;
    public static float dash1, dash2;
    public static float dashRate = 1.5f;
    [SerializeField] float dashPower = 4000;
    bool dashingForward = false;
    bool dashingBack = false;
    [SerializeField] GameObject dashParticle;



    // Start is called before the first frame update
    void Start()
    {
		Player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dash1 = Time.time - timeAfter1;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash1 > dashRate && dash2 > dashRate)
        {
            DASH();
            timeAfter1 = Time.time;
        }

        dash2 = Time.time - timeAfter2;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dash1 < dashRate && dash2 > dashRate)
        {
            DASH();
            timeAfter2 = Time.time;
        }
        if (dashingForward)
        {
            transform.GetChild(0).GetComponent<Camera>().fieldOfView -= 0.6f;
           // transform.GetChild(0).GetChild(0).transform.Translate(Vector3.back * 0.015f);

        }
        else
        {
            if (transform.GetChild(0).GetComponent<Camera>().fieldOfView <= 60)
            {
                transform.GetChild(0).GetComponent<Camera>().fieldOfView+=0.7f;
               // transform.GetChild(0).GetChild(0).transform.Translate(Vector3.back * 0.005f);

            }


        }
        if (dashingBack)
        {
            transform.GetChild(0).GetComponent<Camera>().fieldOfView += 0.6f;
            // transform.GetChild(0).GetChild(0).transform.Translate(Vector3.back * 0.015f);

        }
        else
        {
            if (transform.GetChild(0).GetComponent<Camera>().fieldOfView >= 60)
            {
                transform.GetChild(0).GetComponent<Camera>().fieldOfView -= 0.7f;
                // transform.GetChild(0).GetChild(0).transform.Translate(Vector3.back * 0.005f);

            }
            

        }

    }

    void DASH()
    {
        SoundManager.PlayCustom(PlayerNewMovement.dashSound);
        if (Input.GetKey(KeyCode.S))
        {
            Player.velocity -= transform.forward * Time.deltaTime * dashPower + transform.up * Time.deltaTime * 500;
            dashingBack = true;
            Invoke("UndoEffect", 0.2f);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            Player.velocity += transform.right * Time.deltaTime * dashPower + transform.up * Time.deltaTime * 500;
            dashingForward = true;
            Invoke("UndoEffect", 0.2f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Player.velocity -= transform.right * Time.deltaTime * dashPower + transform.up * Time.deltaTime * 500;
            dashingForward = true;
            Invoke("UndoEffect", 0.2f);
        }
        else
        {
            Player.velocity += transform.forward * Time.deltaTime * dashPower + transform.up * Time.deltaTime * 500;
            transform.GetChild(0).GetComponent<Camera>().fieldOfView = 70;
            dashingForward = true;
            Invoke("UndoEffect", 0.2f);
        }
        dashParticle.GetComponent<ParticleSystem>().Play();

    }

    void UndoEffect()
    {
        dashingForward = false;
        dashingBack = false;

    }
}
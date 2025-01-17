﻿using UnityEngine;

public class CustomFlow_Story : MonoBehaviour
{
    GameObject player;
    GameObject friend;
    bool ceva = true;
    bool over = false;
    bool screenDown = false;
    [SerializeField] string endMessage;
    float friendYrotation = -1;
    float animationBlendSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        friend = GameObject.Find("Friend");
        Invoke("InteractLater", 1.5f);

    }

    void InteractLater()
    {
        friend.GetComponent<Dialog>().Interact();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        print(animationBlendSpeed);
        if ((Vector3.Distance(player.transform.position, friend.transform.position) < 4) && (friend.transform.position.x > -75) && friend.GetComponent<Dialog>().currentString >= 4)
        {
            friend.transform.Translate(new Vector3(0, 0, 3f) * Time.fixedDeltaTime);
            animationBlendSpeed += 0.1f;
        }
        else
            animationBlendSpeed -= 0.15f;



        if (friend.GetComponent<Dialog>().currentString == 4)
        {
            if (friend.transform.eulerAngles.y > 270)
            {
                friend.transform.rotation = Quaternion.Euler(0, friendYrotation, 0);
                friendYrotation -= 3.5f;
                animationBlendSpeed += 0.01f;

            }
            else
            {
                friend.transform.rotation = Quaternion.Euler(0, -90f, 0);
            }

        }
        if ((Vector3.Distance(player.transform.position, friend.transform.position) < 5) && !ceva && !over)
        {
            ceva = true;
            friend.GetComponent<Dialog>().Interact();

        }
        else
            ceva = false;
        if (friend.GetComponent<Dialog>().currentString >= 31)
        {
            over = true;
        }
        if (over)
        {
            if (!screenDown)
            {
                screenDown = true;
                GameObject.FindObjectOfType<SceneTransition>().FadeIn(endMessage, 1);
                friend.transform.rotation = Quaternion.Euler(0, 90f, 0);

            }
            animationBlendSpeed += 0.01f;

            friend.transform.Translate(new Vector3(0, 0, 0f) * Time.fixedDeltaTime);
        }
        animationBlendSpeed = Mathf.Clamp(animationBlendSpeed, 0, 1);

        friend.GetComponent<Animator>().SetFloat("Blend", animationBlendSpeed);








    }
}

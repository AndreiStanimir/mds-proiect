using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    enum RepeatType { noRepeat, RepeatBomerang, normalRepeat };
    GameObject player;
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;
    [SerializeField] RepeatType repeatType;
    [SerializeField] float time;
    [SerializeField] float startDelay;
    [SerializeField] float maxDistance;

    bool started;
    Vector3 initPos;
    void Start()
    {
        started = false;
        if (repeatType != RepeatType.noRepeat)
            initPos = transform.position;
        Invoke("StartMove", startDelay);
        player = GameObject.Find("Player");

    }

    void StartMove()
    {
        started = true;
        if (repeatType == RepeatType.noRepeat)
            Invoke("Stop", time);
        if (repeatType == RepeatType.RepeatBomerang)
            Invoke("InvertDirection", time);
        else if (repeatType == RepeatType.normalRepeat)
            Invoke("Repeat", time);
    }

    void Stop()
    {
        enabled = false;
    }

    void Repeat()
    {
        transform.position = initPos;
        Invoke("Repeat", time);

    }

    void InvertDirection()
    {
        direction *= -1;
        Invoke("InvertDirection", time);
    }


    void Update()
    {
        if (started && Vector3.Distance(player.transform.position, transform.position) <= maxDistance)
            transform.Translate(direction * speed * Time.deltaTime);
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    static AudioSource RandomizePitch(AudioSource source, float min, float max)
    {
        source.pitch = Random.Range(min, max);
        return source;
    }

    public static void PlayCustom(AudioSource source, float interval = 0.08f, float deltaPitch = 0.0f)
    {
        RandomizePitch(source, 1 - interval + deltaPitch, 1 + interval + deltaPitch);
        source.Play(); 
    }



}

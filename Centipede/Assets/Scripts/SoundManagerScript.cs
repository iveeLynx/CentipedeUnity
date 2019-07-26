using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip shot, boom;
    static AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        shot = Resources.Load<AudioClip>("shot");
        Debug.Log("EAIJGNLIAEHGN<KSDAJGN " + shot);
        boom = Resources.Load<AudioClip>("boom");
    }


    // Play sound when player shoots or mushroom and centipede dies
    public static void Play(string clip)
    {
        switch (clip)
        {
            case "shot": source.PlayOneShot (shot); break;
            case "boom": source.PlayOneShot (boom); break;
        }
    }
}

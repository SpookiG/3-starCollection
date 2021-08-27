using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLooping : MonoBehaviour
{
    private static List<AudioSource> loopers;
    
    //public AudioClip loop;
    private AudioSource looper;

    private void Awake()
    {
        loopers = new List<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        looper = GetComponent<AudioSource>();

        if (!loopers.Contains(looper))
        {
            loopers.Add(looper);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int finCount = 0;

        foreach (AudioSource l in loopers)
        {
            if (!l.isPlaying)
            {
                finCount++;
            }
        }

        if (finCount == loopers.Count)
        {
            foreach (AudioSource l in loopers)
            {
                l.Stop();
                l.Play();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGroup : MonoBehaviour
{
    public AudioClip TwilightClip;
    public AudioClip PinkiePieClip;
    public AudioClip RarityClip;
    public AudioClip ApplejackClip;
    public AudioClip RainbowDashClip;
    public AudioClip FluttershyClip;
    public AudioClip EndingClip;

    public AudioSource Twilight;
    public AudioSource PinkiePie;
    public AudioSource Rarity;
    public AudioSource Applejack;
    public AudioSource RainbowDash;
    public AudioSource Fluttershy;
    public AudioSource EndingSource;

    public void AssignAudio()
    {
        Twilight.clip = TwilightClip;
        PinkiePie.clip = PinkiePieClip;
        Rarity.clip = RarityClip;
        Applejack.clip = ApplejackClip;
        RainbowDash.clip = RainbowDashClip;
        Fluttershy.clip = FluttershyClip;
        EndingSource.clip = EndingClip;
    }
}

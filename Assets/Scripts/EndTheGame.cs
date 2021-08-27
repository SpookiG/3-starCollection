using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTheGame : MonoBehaviour
{
    public static int CurrentStars;

    public int StarsToEnd = 1;
    public Camera Cam;
    public float EndSize = 50f;
    public float ZoomSpeed = 1f;

    public SoundLooping FinalAudioLooper;

    private TimeLerp<float> _timeLerp;
    private bool _finalAudio;

    // Start is called before the first frame update
    void Start()
    {
        CurrentStars = 0;

        float startSize = Cam.orthographicSize;
        _timeLerp = new TimeLerp<float>();
        _timeLerp.Prep(startSize, EndSize, ZoomSpeed, true, new SlowToStop());
        _finalAudio = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentStars >= StarsToEnd)
        {
            var progress = _timeLerp.Go();

            Cam.orthographicSize = progress.position;

            if (!_finalAudio && progress.progress >= 0.1)
            {
                FinalAudioLooper.enabled = true;
                _finalAudio = true;
            }

            if (progress.progress >= 1)
            {
                Application.Quit();
                Debug.Log("Game has ended!!");
            }
        }
    }
}

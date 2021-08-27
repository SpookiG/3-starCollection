using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTrail : MonoBehaviour
{
    private static int lineLength;

    public RPGControllerNew trail;
    public float Speed = 6;
    private int? placeInLine;

    private TimeLerp<Vector2> _timeLerp;

    public int PosX;
    public int PosY;


    private Collider2D _trigger;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        lineLength = 0;

        PosX = (int)transform.position.x;
        PosY = (int)transform.position.y;

        placeInLine = null;
        _timeLerp = new TimeLerp<Vector2>();
        _timeLerp.Prep(transform.position, new Vector2(PosX, PosY), Speed, true);

        _trigger = GetComponent<Collider2D>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (placeInLine != null)
        {
            if (PosX != trail.TrailX[(int)placeInLine] || PosY != trail.TrailY[(int)placeInLine])
            {
                PosX = trail.TrailX[(int)placeInLine];
                PosY = trail.TrailY[(int)placeInLine];

                _timeLerp.Prep(transform.position, new Vector2(PosX, PosY - 0.5f), Speed, true);
            }

            transform.position = _timeLerp.Go().position;

            _trigger.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lineLength++;
        placeInLine = lineLength;
        EndTheGame.CurrentStars++;

        _audioSource.spread = 180;
    }
}

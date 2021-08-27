using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGControllerNew : MonoBehaviour
{
    public float Speed = 7;
    public float FootstepTempo = 0.8f;
    public AudioClip[] SnowSteps;

    public int PosX;
    public int PosY;
    private bool _priorityX;
    private TimeLerp<Vector2> _timeLerp;

    private Animator _animator;
    private AudioSource _audioSource;

    [Range(1, 10)]
    public int TrailSize = 7;

    public int[] TrailX;
    public int[] TrailY;

    private float timeSinceLastFootstep;

    // Start is called before the first frame update
    void Start()
    {
        PosX = (int)transform.position.x;
        PosY = (int)transform.position.y;
        _priorityX = true;

        _timeLerp = new TimeLerp<Vector2>();
        _timeLerp.Prep(transform.position, new Vector2(PosX, PosY), Speed, true);

        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        TrailX = new int[TrailSize];
        TrailY = new int[TrailSize];

        timeSinceLastFootstep = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        if (Move())
        {
            if (timeSinceLastFootstep >= FootstepTempo)
            {
                timeSinceLastFootstep = 0f;
                _audioSource.Play();
            }
        }
        else
        {
            PrepMove();
        }


        if (TrailX[0] != PosX || TrailY[0] != PosY)
        {
            for (int i = TrailSize - 1; i >= 1; i--)
            {
                TrailX[i] = TrailX[i - 1];
                TrailY[i] = TrailY[i - 1];
            }

            TrailX[0] = PosX;
            TrailY[0] = PosY;
        }

        timeSinceLastFootstep += Time.deltaTime;


        //_animator.SetBool("xPriority", _priorityX);





        /*if (!_audioSource.isPlaying)
        {
            //_audioSource.clip = SnowSteps[Random.Range(0, SnowSteps.Length)];
            _audioSource.Play();
        }*/

        //stack of movements
        //key down add
        //key up remove
        //change index each time
        //move check for collider, halt if collider.isTrigger == false
        //if space pressed, check for cutscene and play it if there
    }



    private bool Move()
    {
        var progress = _timeLerp.Go();
        transform.position = progress.position;
        return progress.progress < 1;
    }

    private void PrepMove()
    {
        int horizontal = (int)Input.GetAxisRaw("Horizontal");
        int vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal == 0 && vertical == 0)
        {
            _animator.SetInteger("horizontal", 0);
            _animator.SetInteger("vertical", 0);
            return;
        }

        if (horizontal == 0)
        {
            _priorityX = false;
        }

        if (vertical == 0)
        {
            _priorityX = true;
        }

        Vector2 pos = (Vector2)transform.position - new Vector2(0, 0.5f);

        // check nothing in the way before preping a movement
        if (_priorityX)
        {
            if (Physics2D.Linecast(pos, pos + new Vector2(horizontal, 0), ~(1 << LayerMask.NameToLayer("Ignore Raycast"))).transform == null)
            {
                PosX += horizontal;
                _timeLerp.Prep(transform.position, new Vector2(PosX, PosY), Speed, true);
                _animator.SetInteger("vertical", 0);
                _animator.SetInteger("horizontal", horizontal);
            }
            else
            {
                _animator.SetInteger("horizontal", 0);
            }
        }
        else
        {
            if (Physics2D.Linecast(pos, pos + new Vector2(0, vertical), ~(1 << LayerMask.NameToLayer("Ignore Raycast"))).transform == null)
            {
                PosY += vertical;
                _timeLerp.Prep(transform.position, new Vector2(PosX, PosY), Speed, true);
                _animator.SetInteger("horizontal", 0);
                _animator.SetInteger("vertical", vertical);
            }
            else
            {
                _animator.SetInteger("vertical", 0);
            }
        }

        _priorityX = !_priorityX;
    }
}

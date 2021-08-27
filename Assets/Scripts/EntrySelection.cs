using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntrySelection : MonoBehaviour
{
    // initially disable player controls

    // what does this do?
    // start w/ wind?
    // fade text in
    // fade multi select in
    // switch multi select options on directions
    // on space fade out select
    // fade in to game
    // stop audio?
    // start player controls

    public TextMeshProUGUI Heading;
    public TextMeshProUGUI[] Options;
    public Image FadeScreen;
    public AudioGroup[] AudioOptions;

    public RPGControllerNew PlayerController;

    public float FadeSpeed = 1;

    private int _stage;
    private TimeLerp<float> _fades;

    private int _selectionIndex;
    private int _lastSelectionIndex;


    // Start is called before the first frame update
    void Start()
    {
        _stage = 0;
        _fades = new TimeLerp<float>();
        _fades.Prep(0, 1, FadeSpeed);
        _selectionIndex = 0;
        _lastSelectionIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        (float position, float progress) progress;

        switch (_stage)
        {
            case 0:
                progress = _fades.Go();

                Heading.alpha = progress.position;

                if (progress.progress >= 1)
                {
                    _fades.Prep(0, 1, FadeSpeed);
                    _stage++;
                }
                break;
            case 1:
                progress = _fades.Go();

                foreach (TextMeshProUGUI opt in Options)
                {
                    opt.alpha = progress.position;
                }

                if (progress.progress >= 1)
                {
                    _fades.Prep(1, 0, FadeSpeed);
                    _stage++;
                }
                break;
            case 2:
                // pick options
                if (Input.GetButtonDown("Jump"))
                {
                    AudioOptions[_selectionIndex].AssignAudio();
                    PlayerController.enabled = true;

                    _stage++;
                }

                if (Input.GetButtonDown("Vertical"))
                {
                    _selectionIndex -= (int)Input.GetAxisRaw("Vertical");
                    _selectionIndex = _selectionIndex >= 0 ? _selectionIndex % Options.Length : _selectionIndex + Options.Length;
                }

                if (_selectionIndex != _lastSelectionIndex)
                {
                    Options[_lastSelectionIndex].text = Options[_lastSelectionIndex].text.Trim('{').Trim('}').Trim(' ');
                    Options[_selectionIndex].text = "{  " + Options[_selectionIndex].text + "  }";

                    _lastSelectionIndex = _selectionIndex;
                }

                break;
            case 3:
                progress = _fades.Go();

                Heading.alpha = progress.position;
                foreach (TextMeshProUGUI opt in Options)
                {
                    opt.alpha = progress.position;
                }
                var fadeScreenCol = FadeScreen.color;
                fadeScreenCol.a = progress.position;
                FadeScreen.color = fadeScreenCol;

                if (progress.progress >= 1)
                {
                    _stage++;
                }

                // set audio & fade into game & enable player controls
                break;
        }
    }
}

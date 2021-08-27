using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public float xMargin = 1.0f;
    public float yMargin = 1.0f;
    public float maxX;
    public float minX;

    //public float pixelSize = 1;


    public Transform player;

    // Use this for initialization
    void Start ()
    {
    }

    void Update ()
    {
	horizontal ();
	vertical ();
    }

	void horizontal () {
		float poss = transform.position.x;

		if (Mathf.Abs (transform.position.x - player.position.x) > xMargin) {
			if ((transform.position.x - player.position.x) > xMargin) {
				poss = player.position.x + xMargin;
			} else {
				poss = player.position.x - xMargin;
			}
		}

	//poss = Mathf.Lerp(transform.position.x, player.position.x, 0.01f);

	//poss = (Mathf.Round (poss * pixelSize)) / pixelSize;

	transform.position = new Vector3(poss, transform.position.y, transform.position.z);
	}

	void vertical () {
		float poss = transform.position.y;

		if (Mathf.Abs (transform.position.y - player.position.y) > yMargin) {
			if ((transform.position.y - player.position.y) > yMargin) {
				poss = player.position.y + yMargin;
			} else {
				poss = player.position.y - yMargin;
			}
		}

		transform.position = new Vector3(transform.position.x, poss, transform.position.z);
	}

}

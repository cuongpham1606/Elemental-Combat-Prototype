using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour {
    PlayerController player;
	
    // Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();	
	}

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.currentPos, 0.1f);
    }

}

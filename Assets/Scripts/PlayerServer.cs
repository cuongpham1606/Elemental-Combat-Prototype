using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServer : MonoBehaviour {
    PlayerController player;
	
    // Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();	
	}

    public void UpdatePosition(Vector3 position)
    {
        player.currentPos = position;
    }
}

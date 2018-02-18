using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
    [SyncVar] public Vector3 currentPos;

    PlayerClient client;
    PlayerObserver observer;
    PlayerServer server;

    public float moveSpeed = 6;
    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float shootCooldownTime = .2f;

    Collider2D coll;
    Controller2D controller;
    public InputManager inputManager;
    
    // Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            client = gameObject.AddComponent<PlayerClient>();
        }
        else
        {
            observer = gameObject.AddComponent<PlayerObserver>();
        }
        if (isServer)
        {
            server = gameObject.AddComponent<PlayerServer>();
        }

	}
	
	[Command]
    public void CmdChangePos(Vector3 position)
    {
        server.UpdatePosition(position);
    }

}

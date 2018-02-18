using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerClient : MonoBehaviour {
    PlayerController player;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    Vector2 cursorPos;
    float velocityXSmoothing;
    bool canShoot = true;
    float maxShootRange = 9999;

    Collider2D coll;
    Controller2D controller;
    public InputManager inputManager;
    
    // Use this for initialization
	void Start () {
        inputManager = InputManager.Instance;
        player = GetComponent<PlayerController>();
        controller = GetComponent<Controller2D>();
        gravity = -(2 * player.jumpHeight) / Mathf.Pow(player.timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * player.timeToJumpApex;

        coll = GetComponent<Collider2D>();

        if (inputManager != null)
        {
            inputManager.OnInputXChanged += OnInputXChanged;
            inputManager.OnJumpButtonPressed += OnJumpPressed;
        }
	}

    private void OnJumpPressed()
    {
        if (controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }
    }

    private void OnInputXChanged(float inputX)
    {
        float targetVelocityX = inputX * player.moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
            (controller.collisions.below) ? player.accelerationTimeGrounded : player.accelerationTimeAirborne);
    }

    void FixedUpdate()
    {
        if (!coll.enabled) return;

        if (controller.collisions.above || (controller.collisions.below && velocity.y < 0))
        {
            velocity.y = 0;
        }

        velocity.y += gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
        player.CmdChangePos(transform.position);
    }

    public void Die()
    {
        print("Dead");
        coll.enabled = false;
        if (inputManager != null)
        {
            inputManager.OnInputXChanged -= OnInputXChanged;
            inputManager.OnJumpButtonPressed -= OnJumpPressed;
        }

        StartCoroutine(AnimateDead());
    }

    private IEnumerator AnimateDead()
    {
        float time = 0;
        while (time < 0.5f)
        {
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, Mathfx.Sinerp(0, 1, time / 0.5f));
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}

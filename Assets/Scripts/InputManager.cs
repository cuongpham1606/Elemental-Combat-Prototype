using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance { get; private set; }
    public KeyCode jumpButton;
    public KeyCode shootButton;

    public System.Action<float> OnInputXChanged;
    public System.Action OnJumpButtonPressed;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
        if (OnInputXChanged != null) OnInputXChanged(Input.GetAxisRaw("Horizontal"));
        if (Input.GetKeyDown(jumpButton) && OnJumpButtonPressed != null) OnJumpButtonPressed();
    }
}

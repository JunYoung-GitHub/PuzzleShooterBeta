using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)] //Executes before any other script

public class PlayerInput : MonoBehaviour {

    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }

    public bool sprintHeld { get; private set; }
    public bool jumpPressed { get; private set; }
    public bool activatePressed { get; private set; }
    public bool primaryShootPressed { get; private set; }
    public bool secondaryShootPressed { get; private set; }
    public bool weapon1Pressed { get; private set; }
    public bool weapon2Pressed { get; private set; }
    public bool weapon3Pressed { get; private set; }

    private bool _clear;

    //create singleton
    private static PlayerInput _instance;

    private void Awake() {
        _instance = this; //instance is not null

        if(_instance != null && _instance != this) {
            Destroy(this);
            return;
        }
        _instance = this;
    }

    public static PlayerInput GetInstance() {
        return _instance; 
    }
    //End of singleton

    // Update is called once per frame
    void Update() {
        ClearInput();
        ProcessInput();
    }

    void ProcessInput() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        sprintHeld = sprintHeld || Input.GetButton("Sprint");
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        activatePressed = activatePressed || Input.GetKeyDown(KeyCode.E);

        weapon1Pressed = weapon1Pressed || Input.GetKeyDown(KeyCode.Alpha1);
        weapon2Pressed = weapon2Pressed || Input.GetKeyDown(KeyCode.Alpha2);
        weapon3Pressed = weapon3Pressed || Input.GetKeyDown(KeyCode.Alpha3);

        primaryShootPressed = primaryShootPressed || Input.GetButtonDown("Fire1");
        secondaryShootPressed = secondaryShootPressed || Input.GetButtonDown("Fire2");
    }

    private void FixedUpdate() {
        _clear = true;
    }
    void ClearInput() {
        if (!_clear) return;

        horizontal = 0;
        vertical = 0;
        mouseX = 0;
        mouseY = 0;

        sprintHeld = false;
        jumpPressed = false;
        activatePressed = false;
        primaryShootPressed = false;
        secondaryShootPressed = false;

        weapon1Pressed = false;
        weapon2Pressed = false;
        weapon3Pressed = false;
    }
}

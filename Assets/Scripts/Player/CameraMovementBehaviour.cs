using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovementBehaviour : MonoBehaviour {

    PlayerInput _input;

    [Header("Player Camera Turn")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private bool _invertMouse;
    [SerializeField] private float _yTurnMin;
    [SerializeField] private float _yTurnMax;

    private float _camXRotation;
    // Start is called before the first frame update
    void Start() {
        _input = PlayerInput.GetInstance(); //singleton
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
    }


    // Update is called once per frame
    void Update() {
        RotateCamera();
    }
    void RotateCamera() {
        //Camera up/down movement
        _camXRotation += Time.deltaTime * _input.mouseY * _turnSpeed * (_invertMouse ? 1 : -1);
        _camXRotation = Mathf.Clamp(_camXRotation, _yTurnMin, _yTurnMax);
        transform.localRotation = Quaternion.Euler(_camXRotation, 0, 0);
    }
}

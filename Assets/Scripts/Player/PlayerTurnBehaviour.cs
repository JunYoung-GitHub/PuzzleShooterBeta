using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnBehaviour : MonoBehaviour {
    PlayerInput _input;

    [Header("Player Turn")]
    [SerializeField] private float _turnSpeed;
    // Start is called before the first frame update
    void Start() {
        _input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update() {
        RotatePlayer();
    }

    void RotatePlayer() {
        //Player turn movement
        transform.Rotate(Vector3.up * _turnSpeed * Time.deltaTime * _input.mouseX);
    }
}

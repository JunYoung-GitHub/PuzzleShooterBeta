using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementBehaviour))]
public class PlayerJumpBehaviour : Interactor {

    [Header("Jump")]
    [SerializeField] private float _jumpVelocity;

    private PlayerMovementBehaviour _movementBehaviour;

    private void Start() {
        _movementBehaviour = GetComponent<PlayerMovementBehaviour>();
        Debug.Log("MoveBehaviour is" + _movementBehaviour);
    }
    public override void Interact() {
        /*if (_movementBehaviour == null) {
            _movementBehaviour = GetComponent<PlayerMovementBehaviour>();
        }*/

        if (_input.jumpPressed && _movementBehaviour._isGrounded) {
            _movementBehaviour.SetYVelocity(_jumpVelocity);
        }
    }
}

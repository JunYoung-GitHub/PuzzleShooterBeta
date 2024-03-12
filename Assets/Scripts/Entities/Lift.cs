using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {

    [SerializeField] private float _moveDistance;
    [SerializeField] private float _liftSpeed;
    [SerializeField] private float _accuracy = 0.05f;
    [SerializeField] private bool _isUp;

    private bool _isMoving;
    Vector3 _targetPosition;

    public void ActivateLift() {
        if (_isMoving) return;

        if(_isUp ) {
            _targetPosition = transform.localPosition - new Vector3(0, _moveDistance, 0);
            _isUp = false;
        }

        else {
            _targetPosition= transform.localPosition + new Vector3(0, _moveDistance, 0);
            _isUp = true;
        }

        _isMoving = true;
    }


    private void FixedUpdate() {
        if (_isMoving) {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPosition, _liftSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.localPosition, _targetPosition) < _accuracy) {
            _isMoving = false;
        }
    }
}

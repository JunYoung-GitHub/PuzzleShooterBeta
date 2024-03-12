using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    [SerializeField] private MeshRenderer _doorMeshRenderer;
    [SerializeField] private Material _doorMaterial;
    [SerializeField] private Material _triggerMaterial;
    [SerializeField] private Animator _doorAnim;

    private bool _isLocked = true;

    private float _timer = 0;

    private float _waitTime = 1.0f;

    private void OnTriggerEnter(Collider other) {
        if (!_isLocked && other.CompareTag("Player")) {
            _timer = 0;
            _doorMeshRenderer.material = _triggerMaterial;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (_isLocked) return;
        if (!other.CompareTag("Player")) return;

        _timer += Time.deltaTime;

        if (_timer >= _waitTime) {
            _timer = _waitTime;
            _doorAnim.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        _doorAnim.SetBool("Open", false);
        _doorMeshRenderer.material = _doorMaterial;
    }

    public void LockDoor() {
        _isLocked = true;
    }

    public void UnlockDoor() {
        Debug.Log("Unlocked Door");
        _isLocked = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour {
    [SerializeField] private float _checkRadius;
    [SerializeField] private LayerMask _pickableLayer;

    public UnityEvent onCubePlaced;
    public UnityEvent onCubeRemoved;
    private void OnCollisionEnter(Collision collision) {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _checkRadius, _pickableLayer);
        Debug.Log(hitColliders.Length);

        foreach (var collider in hitColliders) {
            Debug.Log(collider.gameObject.name);

            if (collider.CompareTag("PickCube")) {
                onCubePlaced?.Invoke();
                break;
            }
        }
    }
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("PickCube")) {
            onCubeRemoved?.Invoke();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, _checkRadius);
    }

}
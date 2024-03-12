using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCube : MonoBehaviour, IPickable {

    Rigidbody _cubeRb;

    // Start is called before the first frame update
    void Start() {
        _cubeRb = GetComponent<Rigidbody>();
    }

    public void OnPicked(Transform attachTransform) {
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        _cubeRb.isKinematic = true;
        _cubeRb.useGravity = false;
    }

    public void OnDropped() {
        _cubeRb.isKinematic = false;
        _cubeRb.useGravity = true;
        transform.SetParent(null);
    }

}

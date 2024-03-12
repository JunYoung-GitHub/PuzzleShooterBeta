using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoor : MonoBehaviour {

    [SerializeField] private MeshRenderer _doorMeshRenderer;
    [SerializeField] private Material _doorMaterial;
    [SerializeField] private Material _triggerMaterial;
    [SerializeField] private Animator _doorAnim;

    private float _timer = 0;
    private float _waitTime = 1.0f;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            _timer = 0;
            _doorMeshRenderer.material = _triggerMaterial;
            Debug.Log("Entered normal");
        } 
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("Stay10");
        if (!other.CompareTag("Player")) return;

        _timer += Time.deltaTime;
        if(_timer >= _waitTime) {
            _timer = _waitTime;
            _doorAnim.SetBool("Opened", true);
        }
        Debug.Log("Stay20");
    }

    private void OnTriggerExit(Collider other) {
        _doorAnim.SetBool("Opened", false);
        _doorMeshRenderer.material = _doorMaterial;
    }
}

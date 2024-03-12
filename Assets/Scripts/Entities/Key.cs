using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    [SerializeField] private Animator _doorAnim;

    private void OnTriggerEnter(Collider other) {
        _doorAnim.SetBool("Unlocked", true);
        Destroy(gameObject);
    }
}

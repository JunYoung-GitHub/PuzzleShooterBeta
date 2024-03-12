using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactor : MonoBehaviour {
     protected PlayerInput _input;

    //singleton
    private void Awake() {
        _input = PlayerInput.GetInstance();
    }

    // Update is called once per frame
    void Update() {
        Interact();
    }

    public abstract void Interact();
}

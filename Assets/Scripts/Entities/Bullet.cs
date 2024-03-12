using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {
        //Debug.Log($"Bullet Collided with {collision.gameObject.name}");

        IDestroyable destroyable = collision.gameObject.GetComponent<IDestroyable>();

        if(destroyable!= null ) {
            destroyable.OnCollided();
        }
    }
}

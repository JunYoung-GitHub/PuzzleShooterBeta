using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour {

    [SerializeField] private float _maxHealth;
    public Action<float>  OnHealthUpdated;

    private float _health;
    public bool isDead { get; private set; }
    // Start is called before the first frame update
    void Start() {
        _health = _maxHealth;
        OnHealthUpdated?.Invoke(_health);
       
    }

    public void DeductHealth(float value) {
        if(isDead) return; //guard clause (no need to deduct player if they dead)

        _health -= value;
        OnHealthUpdated?.Invoke(_health);
    }

    public float GetHealth() { return _health; }
}

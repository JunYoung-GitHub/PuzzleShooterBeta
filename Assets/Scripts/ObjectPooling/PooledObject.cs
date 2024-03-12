using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour {
    [SerializeField] private UnityEvent _OnReset;

    private bool _setToDestroy = false;
    private float _timeToDestroy = 0;
    private float _timer;
    ObjectPool _associatedPool;

    public void SetObjectPool(ObjectPool pool) {
        _associatedPool = pool;
        _timer = 0;
        _timeToDestroy = 0;
        _setToDestroy = false;
    }

    // Update is called once per frame
    void Update() {
        if(_setToDestroy) {
            _timer += Time.deltaTime;
            if(_timer >= _timeToDestroy) {
                _setToDestroy = false;
                _timer = 0;
                Destroy();
            }
        }
    }

    public void Destroy() {
        if(_associatedPool != null) {
            _associatedPool.RestoreObject(this);
            //Object Pool should restore this pooled object
        }
    }

    public void Destroy(float time) {
        _setToDestroy = true;
        _timeToDestroy = time;
    }

    public void ResetObject() {
        _OnReset?.Invoke();
    }

}

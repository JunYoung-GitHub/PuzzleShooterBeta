using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public GameObject objectToPool;
    public int startSize;

    [SerializeField] private List<PooledObject> _objectPool = new List<PooledObject>(); 
    [SerializeField] private List<PooledObject> _usedPool = new List<PooledObject>();

    private PooledObject _tempObject;
    // Start is called before the first frame update
    void Start() {

    }

    private void Initialize() {
        for(int i = 0; i < startSize; i++) {
            AddNewObject(); //Add a new object to pool
        }
    }

    private void AddNewObject() {
        _tempObject = Instantiate(objectToPool, transform).GetComponent<PooledObject>();
        _tempObject.gameObject.SetActive(false);
        _tempObject.SetObjectPool(this);
        _objectPool.Add(_tempObject);
    }

    public PooledObject GetPooledObject() {
        PooledObject tempObjectToReturn;
        if(_objectPool.Count > 0) {
            tempObjectToReturn = _objectPool[0];
            _usedPool.Add(tempObjectToReturn);
            _objectPool.RemoveAt(0);
        }
        else {
            AddNewObject();
            tempObjectToReturn = GetPooledObject();
        }

        tempObjectToReturn.gameObject.SetActive(true);
        tempObjectToReturn.ResetObject();
        return tempObjectToReturn; 
    }
    
    public void DestroyPooledObject(PooledObject obj, float time = 0) {
        if(time == 0) {
            obj.Destroy();
        }
        else {
            obj.Destroy(time);
        }
    }

    public void RestoreObject(PooledObject obj) {
        obj.gameObject.SetActive(false);
        _usedPool.Remove(obj);
        _objectPool.Add(obj);
    }
}

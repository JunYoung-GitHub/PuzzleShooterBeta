using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShootStrategy : IShootStrategy {
    ShootInteractor _interactor;
    Transform _spawnPoint;
    Transform _spawnPointExtra;
    //Constructor
    public MultiShootStrategy(ShootInteractor interactor) {
        Debug.Log("Switched to Multi shooting mode!");

        _interactor = interactor;
        _spawnPoint = interactor.GetSpawnPoint();
        _spawnPointExtra = interactor.GetSpawnPointExtra();

        //Change Gun Color
        _interactor.gunRenderer.material.color = _interactor.multiGunColor;
    }
    public void Shoot() {

        PooledObject pooledBullet = _interactor._bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        PooledObject pooledBulletExtra = _interactor._bulletPool.GetPooledObject();
        pooledBulletExtra.gameObject.SetActive(true);

        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        Rigidbody bulletExtra = pooledBulletExtra.GetComponent<Rigidbody>();

        bullet.transform.position = _spawnPoint.position;
        bullet.transform.rotation = _spawnPoint.rotation;
        
        bulletExtra.transform.position = _spawnPointExtra.position;
        bulletExtra.transform.rotation = _spawnPointExtra.rotation;
       
        bullet.velocity = _spawnPoint.forward * _interactor.GetFinalShootVelocity();
        bulletExtra.velocity = _spawnPoint.forward * _interactor.GetFinalShootVelocity();

        Debug.Log("bullet: " + bullet.position);
        Debug.Log("bulletExtra: " + bulletExtra.position);

        _interactor._bulletPool.DestroyPooledObject(pooledBullet, 1.0f);
        _interactor._bulletPool.DestroyPooledObject(pooledBulletExtra, 1.0f);
    }
}

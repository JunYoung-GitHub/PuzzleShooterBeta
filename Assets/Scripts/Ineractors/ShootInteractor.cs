using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor {
    [Header("Gun")]
    public MeshRenderer gunRenderer;
    public Color bulletGunColor;
    public Color rocketGunColor;
    public Color multiGunColor;

    [Header("Shoot")]
    //[SerializeField] private Rigidbody _bulletPrefab;
    public ObjectPool _bulletPool;
    public ObjectPool _rocketPool;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _spawnPointExtra;
    [SerializeField] private float _shootForce;
    //[SerializeField] private ShootInputType _shootInput;
    [SerializeField] private PlayerMovementBehaviour _moveBehaviour;

    private float _finalShootVelocity;

    private IShootStrategy _currentStrategy; //Reference to our current shooting strategy
    /*public enum ShootInputType {
        Primary, //left click
        Secondary, //right click
    }*/ 

    public override void Interact() {
        /*if (_shootInput == ShootInputType.Primary && _input.primaryShootPressed
            || _shootInput == ShootInputType.Secondary && _input.secondaryShootPressed) {
            Shoot();
        }*/

        //Default shoot strategy
        if(_currentStrategy == null) {
            _currentStrategy = new BulletShootStrategy(this);
        }

        //Changes strategy based on User input (key 1 = bullet key 2 = rocket)
        if(_input.weapon1Pressed) {
            _currentStrategy = new BulletShootStrategy(this);
        }

        if (_input.weapon2Pressed) {
            _currentStrategy = new RocketShootStrategy(this);
        }

        if (_input.weapon3Pressed) {
            _currentStrategy = new MultiShootStrategy(this);
        }

        if (_input.primaryShootPressed && _currentStrategy != null) {
            _currentStrategy.Shoot();
        }
    }

    public Transform GetSpawnPoint() {
        return _spawnPoint;
    } 
    
    public Transform GetSpawnPointExtra() {
        return _spawnPointExtra;
    }

    public float GetFinalShootVelocity() {
        _finalShootVelocity = _moveBehaviour.GetForwardSpeed() + _shootForce;
        return _finalShootVelocity;
    }
}

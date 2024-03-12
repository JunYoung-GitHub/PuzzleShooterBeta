using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState {
    float _distanceToPlayer;
    Health _playerHealth;
    float _damagePerSecond = 20f;

    //Constructor
    public EnemyAttackState(EnemyController enemy) : base(enemy) {
        //Grab the health component of the player
        _playerHealth = enemy._player.GetComponent<Health>();
        Debug.Log("Player Health is: " + _playerHealth);

    }

    public override void OnStateEnter() {
        Debug.Log("Enemy Attack State Entered");
    }

    public override void OnStateExit() {
        Debug.Log("Enemy Attack State Exited");
    }

    public override void OnStateUpdate() {
        Attack();
        if (_enemy._player != null) {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);
            if (_distanceToPlayer > 2) {
                //Go back to follow
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }

            _enemy._agent.destination = _enemy._player.position;
        }
        else {
            //Go back to idle
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }

    void Attack() {
        if(_playerHealth != null) {
            _playerHealth.DeductHealth(_damagePerSecond * Time.deltaTime);
            Debug.Log("ATTACKING!!!");
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState {
    int _currentTarget = 0;
    //Constructor
    public EnemyIdleState(EnemyController enemy) : base(enemy) {

    }
    public override void OnStateEnter() {
        _enemy._agent.destination = _enemy._targetPoints[_currentTarget].position;
    }

    public override void OnStateExit() {
        Debug.Log("Enemy Idle State Exited");
    }

    public override void OnStateUpdate() {
        //Choose A random target point and move there if randomPoint Active is true
        if (_enemy._agent.remainingDistance < 0.1f) {
            if (_enemy._isRandomPointActive) {
                _currentTarget = Random.Range(0, _enemy._targetPoints.Length);
            }
            else {
                _currentTarget++;
            }


            if (_currentTarget >= _enemy._targetPoints.Length) {
                _currentTarget = 0;
            }

            _enemy._agent.destination = _enemy._targetPoints[_currentTarget].position;
        }

        //Check for Player
        if (Physics.SphereCast(_enemy._enemyEye.position, _enemy._checkRadius, _enemy.transform.forward, out RaycastHit hit, _enemy._playerCheckDistance)) {
            if (hit.transform.CompareTag("Player")) {
                Debug.Log("Player Found!");

                _enemy._player = hit.transform;
                _enemy._agent.destination = _enemy._player.position;

                //Move to Follow State
                _enemy.ChangeState(new EnemyFollowState(_enemy));
            }
        }
    }
}
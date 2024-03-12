using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyState {

    float _distanceToPlayer;
    //Constructor
    public EnemyFollowState(EnemyController enemy) : base(enemy) { //value from Enemy state  

    }
    public override void OnStateEnter() {
        Debug.Log("Enemy Follow State Entered");
    }

    public override void OnStateExit() {
        Debug.Log("Enemy Follow State Exited");
    }

    public override void OnStateUpdate() {
        if (_enemy._player != null) {
            _distanceToPlayer = Vector3.Distance(_enemy.transform.position, _enemy._player.position);
            if (_distanceToPlayer > 10) {
                //Go back to Idle
                _enemy.ChangeState(new EnemyIdleState(_enemy));
            }

            //Attack
            if (_distanceToPlayer < 2) {
                _enemy.ChangeState(new EnemyAttackState(_enemy));
            }

            _enemy._agent.destination = _enemy._player.position;
        }
        else {
            //Go back to idle
            _enemy.ChangeState(new EnemyIdleState(_enemy));
        }
    }

}

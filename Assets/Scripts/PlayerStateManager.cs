using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour {
    public PlayerBaseState CurrentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerRunState RunState = new PlayerRunState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerDeadState DeadState = new PlayerDeadState();
    
    private void Start() {
        SwitchState(IdleState);
    }

    private void Update() {
        CurrentState.UpdateState(this);
    }

    private void SwitchState(PlayerBaseState state) {
        CurrentState = state;
        CurrentState.EnterState(this);
    }
}

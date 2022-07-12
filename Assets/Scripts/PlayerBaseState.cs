public abstract class PlayerBaseState {
    public abstract void EnterState(PlayerStateManager state);
    public abstract void UpdateState(PlayerStateManager state);
    public abstract void OnCollisionEnter(PlayerStateManager state);
}

using UnityEngine;
using UnityEngine.AI;

public class CharacterView : MonoBehaviour
{
    private readonly int TakeHitKey = Animator.StringToHash("TakeHit");
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int IsJumpingKey = Animator.StringToHash("IsJumping");
    private readonly int IsDeadKey = Animator.StringToHash("IsDead");
    private readonly int InjuredLayerIndex = 1;
    private readonly int InjuredLayerWeight = 1;

    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Character _character;

    private void Update()
    {
        if (_agent.velocity.sqrMagnitude >= _character.DeadZone)
            StartRunning();
        else
            StopRunning();
    }
    public void Wounded() => _animator.SetLayerWeight(InjuredLayerIndex, InjuredLayerWeight);

    public void StartRunning() => _animator.SetBool(IsRunningKey, true);

    public void StopRunning() => _animator.SetBool(IsRunningKey, false);

    public void OnHitAnimation()
    {
        _animator.SetTrigger(TakeHitKey);
        _agent.isStopped = true;
    }

    public void StartJumping() => _animator.SetBool(IsJumpingKey, true);

    public void StopJumping() => _animator.SetBool(IsJumpingKey, false);

    public void OnDeathAnimation() => _animator.SetBool(IsDeadKey, true);

    public void OnMoveActivate() => _agent.isStopped = false;
}

using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int TakeHitKey = Animator.StringToHash("TakeHit");
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    //private readonly int IsJumpingKey = Animator.StringToHash("IsJumping");
    private readonly int IsDeadKey = Animator.StringToHash("IsDead");
    private readonly int InjuredLayerIndex = 1;
    private readonly int InjuredLayerWeight = 1;

    private Animator _animator;
    private CharacterController _characterController;
    //private NavMeshAgent _agent;
    //private Character _character;

    //public CharacterView(Animator animator)
    //{
    //    _animator = animator;
    //    //_agent = agent;
    //    //_character;
    //}

    //private void CharacterMove()
    //{
    //    if (_agent.velocity.sqrMagnitude >= _character.DeadZone)
    //        StartRunning();
    //    else
    //        StopRunning();
    //}
    public void Wounded() => _animator.SetLayerWeight(InjuredLayerIndex, InjuredLayerWeight);

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponentInParent<CharacterController>();
    }

    public void StartRunning() => _animator.SetBool(IsRunningKey, true);

    public void StopRunning() => _animator.SetBool(IsRunningKey, false);

    public void OnHitAnimation()
    {
        _animator.SetTrigger(TakeHitKey);
        _characterController.enabled = false;
    }

    //public void StartJumping() => _animator.SetBool(IsJumpingKey, true);

    //public void StopJumping() => _animator.SetBool(IsJumpingKey, false);

    public void OnDeathAnimation() => _animator.SetBool(IsDeadKey, true);

    public void OnMoveActivate() => GetComponentInParent<CharacterController>().enabled = true;
}

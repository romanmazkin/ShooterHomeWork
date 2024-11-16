using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDamageable
{
    private const int RightMouseButton = 1;

    [SerializeField] private float _startHealthValue;
    [SerializeField] private float _healthValueWhenWounded;
    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private CharacterView _view;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Health _health;
    [SerializeField] private Flag _flag;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AudioController _audioController;

    private Vector3 _input;
    private Camera _camera;
    private Coroutine _jumpCoroutine;

    public float DeadZone { get; set; } = 0.1f;

    private void Awake()
    {
        _camera = Camera.main;
        _health.Value = _startHealthValue;
    }

    private void Update()
    {
        if (_agent.isOnOffMeshLink)
        {
            if (_jumpCoroutine == null)
            {
                _jumpCoroutine = StartCoroutine(Jump(_animationDuration));
            }
            return;
        }

        if (Input.GetMouseButtonDown(RightMouseButton) == false)
            return;

        if (Physics.Raycast(GetRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, _mask.value))
        {
            if (_health.Value > 0)
            {
                _agent.SetDestination(hit.point);

                _audioController.PlayMovePointSound();

                Flag flag = Instantiate(_flag, hit.point, Quaternion.identity, null);
                flag.Destroy();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_health.Value <= _healthValueWhenWounded)
        {
            _view.Wounded();
        }

        if (_input.magnitude > DeadZone)
        {
            _view.StartRunning();
        }
        else
        {
            _view.StopRunning();
        }
    }

    public void TakeDamage(float damageValue)
    {
        _health.Damage(damageValue);

        if (_health.Value > 0)
        {
            _view.OnHitAnimation();
            Debug.Log(_health.Value);
        }
        else
        {
            _view.OnDeathAnimation();
        }
    }

    private Ray GetRay(Vector3 position) => _camera.ScreenPointToRay(position);

    private IEnumerator Jump(float duration)
    {
        _view.StartJumping();

        OffMeshLinkData data = _agent.currentOffMeshLinkData;

        Vector3 startPosition = _agent.transform.position;
        Vector3 finishPosition = data.endPos + Vector3.up * _agent.baseOffset;

        float progress = 0;

        while (progress < duration)
        {
            float yOffset = _jumpCurve.Evaluate(progress / duration);
            _agent.transform.position = Vector3.Lerp(startPosition, finishPosition, progress / duration) + yOffset * Vector3.up;
            transform.rotation = Quaternion.LookRotation(finishPosition-startPosition);
            progress += Time.deltaTime;
            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _view.StopJumping();
        _jumpCoroutine = null;
    }
}
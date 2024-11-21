using Cinemachine;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private Transform _characterSpawnPoint;
    [SerializeField] private CinemachineVirtualCamera _followCamera;

    private Character _character;

    private void Awake()
    {
        _character = Instantiate(_characterPrefab, _characterSpawnPoint.position, Quaternion.identity, null);
        _character.Initialize();

        _followCamera.Follow = _character.transform;
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        _character.MoveBehaviour();
    }
}

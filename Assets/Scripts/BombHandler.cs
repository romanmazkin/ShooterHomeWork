using System.Collections;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    //Не понимаю почему, но как будто бы бомба срабатывает 2 раза и хп у персонажа отнимаются 2 раза

    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private SphereCollider _triggerCollider;
    [SerializeField] private float _bombTriggerRadius = 5f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _timeToExplosion = 2f;
    [SerializeField] private float _damageValue = 30f;

    private bool _isAlreadyDetonate = false;

    private void Awake()
    {
        _triggerCollider.radius = _bombTriggerRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageableObject))
        {
            StartCoroutine(StartExplosion());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _bombTriggerRadius);
    }

    private IEnumerator StartExplosion()
    {
        _isAlreadyDetonate = true;

        while (_timeToExplosion > 0)
        {
            _timeToExplosion -= Time.deltaTime;
            yield return null;
        }

        Instantiate(_explosionParticle, transform.position, Quaternion.identity);

        Explode();

        _audioController.PlayExplosionSound();

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] explodedColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in explodedColliders)
            if (collider.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damageValue);
    }
}

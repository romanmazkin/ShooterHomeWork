using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private Transform _flagPrefab;
    [SerializeField] private float _destroyTime = 1f;

    public void Destroy()
    {
        Destroy(gameObject, _destroyTime);
    }
}

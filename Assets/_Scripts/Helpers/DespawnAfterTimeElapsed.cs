
using UnityEngine;

public class DespawnAfterTimeElapsed : MonoBehaviour
{
    public float timeToWait = 6;

    // Start is called before the first frame update

    void OnEnable()
    {
        Invoke("Despawn", timeToWait);
    }

    // Update is called once per frame
    void Despawn()
    {
        PoolManager.instance.Despawn(gameObject);
    }
}

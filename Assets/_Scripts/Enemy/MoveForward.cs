using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5;
    protected Transform _transform;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _transform.forward * speed;
    }
}

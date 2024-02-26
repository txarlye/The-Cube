using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Singleton<Player>
{
    public float movementSpeed = 3;
    public GameObject bulletPrefab; 
    public float jumpForce = 5;
    private Transform _transform;
    private Rigidbody _rb;
    private bool isJumping = false;
    private bool canMove = true;
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    private void Start()
    {
        //Metemos en la pool 25 objetos de cada uno
        PoolManager.instance.Load(bulletPrefab, 25); 
    }

    private void Update()
    {
        if (canMove)
        {
            var movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            
                    if (movement.magnitude > 0) {
                        movement = movement.normalized;
                        _rb.velocity = movement * movementSpeed;
                        _transform.forward = movement;
                    }
            
                    if (Input.GetMouseButtonDown(0)) {
                        PoolManager.instance.Spawn(bulletPrefab, _transform.position + new Vector3(0,0.15f,0), Quaternion.LookRotation(_transform.forward, Vector3.up));
                        SoundManager.instance.AudioShoot();
                    }
            
                    if (Input.GetKeyDown(KeyCode.Space) && !isJumping )
                    {
                        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        isJumping = true;
                    }
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    public void cantMove()
    {
        canMove = false;
    }

    public void setcanMove()
    {
        canMove = true;
    }
}
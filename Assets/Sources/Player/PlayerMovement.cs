using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _gravity = -9;
    [SerializeField] private float _friction = 2;

    [Header("Forward Speed")]
    [SerializeField] private float _maxSpeed = 4;
    [SerializeField] private float _accelerationSpeed = 10f;
    
    [Header("Backwars Speed")]
    [SerializeField] private float _maxBackSpeed = 2;
    [SerializeField] private float _accelerationBackSpeed = 5f;

    private CharacterController _characterController;
    private Vector3 _velocity;

    public Vector3 Velocity => _velocity;
    [field: SerializeField] public bool CanMove { get; set; } = false;

    private float _sum = 0;

    public void Stop()
    {
        _velocity = new Vector3();
        CanMove = false;
    }

    public void Move(float speed)
    {
        if (!CanMove) return;

        if (speed < 0)
        {
            _velocity = Vector3.MoveTowards(_velocity, new Vector3(0, 0, -_maxBackSpeed), -speed * _accelerationBackSpeed * Time.deltaTime);
            return;
        }

        _velocity = Vector3.MoveTowards(_velocity, new Vector3(0, 0, _maxSpeed), speed * _accelerationSpeed * Time.deltaTime);
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _characterController.Move((_velocity + new Vector3(0, _gravity, 0)) * Time.deltaTime);
        _velocity = Vector3.MoveTowards(_velocity, new Vector3(), _friction * Time.deltaTime);
        _sum += Velocity.z;
    }
}

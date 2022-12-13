using UnityEngine;

public enum CarMovingDirection
{
    Right, Left
}

[RequireComponent(typeof(CharacterController))]
public class Car : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    [SerializeField] private float _gravity = -9;
    [SerializeField] private float _destructionDistance = 20f;
    [SerializeField] private float _stopSpeed = 5;

    private bool _canMove = false;
    private Vector3 _velocity = new Vector3();
    private Vector3 _startingPoint;
    private CharacterController _characterController;

    public void Stop()
    {
        _canMove = false;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Init(CarMovingDirection movingDirection, float speed = 4)
    {
        _speed = speed;
        if(movingDirection == CarMovingDirection.Left) _speed = -speed;

        _canMove = true;
        _startingPoint = transform.position;
    }

    private void Move()
    {
        if (_canMove)
        {
            _velocity = new Vector3(_speed, _gravity, 0);
        }
        _characterController.Move(_velocity * Time.deltaTime);
        _velocity = Vector3.MoveTowards(_velocity, new Vector3(0, 0, 0), _stopSpeed * Time.deltaTime);
    }

    private void Destroy()
    {
        if (Vector3.Distance(_startingPoint, transform.position) < _destructionDistance) return;

        Destroy(gameObject);
    }

    private void Update()
    {
        Move();
    }
}
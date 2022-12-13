using System.Collections.Generic;
using UnityEngine;

public class DisplayBike : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private List<Transform> _wheels;
    [SerializeField] private Vector3 _rotationAxis = new(1, 0, 0);
    [SerializeField] private float _speed;

    private void SpinWheels(float speed)
    {
        foreach(var wheel in _wheels)
        {
            wheel.Rotate(_rotationAxis, speed * _speed * Time.deltaTime, Space.Self);
        }
    }

    private void Update()
    {
        SpinWheels(_movement.Velocity.z);
    }
}

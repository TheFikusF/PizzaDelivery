using UnityEngine;
using UnityEngine.Events;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField] private UnityEvent _onCarHit = new();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if(other.gameObject.TryGetComponent(out CarHitBox car))
        {
            car.Car.Stop();
            _onCarHit.Invoke();
        }
    }
}
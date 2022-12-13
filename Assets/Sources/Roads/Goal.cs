using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPlayerEnter;

    public void InitGoal(Vector3 position)
    {
        transform.position = position;
        FindObjectOfType<Player>().SetGoal(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            _onPlayerEnter.Invoke();
        }
    }
}

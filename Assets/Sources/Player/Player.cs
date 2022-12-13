using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Goal _goal;
    [Header("Events")]
    [SerializeField] private UnityEvent<Player, Goal> OnGoalSet = new();
    [SerializeField] private UnityEvent<float> OnGoalDistanceUpdate = new();

    public void SetGoal(Goal goal)
    {
        _goal = goal;
        OnGoalSet.Invoke(this, _goal);
        Debug.Log("goal set");
    }

    private void Update()
    {
        if (_goal == null) return;

        OnGoalDistanceUpdate.Invoke(Vector3.Distance(transform.position, _goal.transform.position));
    }
}

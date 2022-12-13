using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GoalCharacter : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private string _danceTringger;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartDancing()
    {
        _animator.SetTrigger(_danceTringger);
    }
}

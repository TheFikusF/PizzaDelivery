using UnityEngine;
using UnityEngine.Events;

public enum SectionType
{
    Grass, Road
}

public class RoadSection : MonoBehaviour
{
    [field: SerializeField] public SectionType SectionType { get; set; }
    [SerializeField] private UnityEvent<RoadSection> OnPlayerEnter = new();

    public void AddListenerOnPlayerEnter(UnityAction<RoadSection> action) => OnPlayerEnter.AddListener(action);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RoadActivator activator))
        {
            OnPlayerEnter.Invoke(this);
        }
    }
}

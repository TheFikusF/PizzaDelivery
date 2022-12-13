using UnityEngine;

public class BlowUpPlayer : MonoBehaviour
{
    [SerializeField] private float _blowUpForce;

    public void BlowUp()
    {
        foreach(MeshFilter filter in transform.GetComponentsInChildren<MeshFilter>())
        {
            BlowUpAction(filter);
        }
    }

    private void BlowUpAction(MeshFilter piece)
    {
        var collider = piece.gameObject.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.sharedMesh = piece.mesh;

        var rigidbody = piece.gameObject.AddComponent<Rigidbody>();
        rigidbody.AddForce((piece.transform.position - transform.position).normalized * _blowUpForce);
    }
}

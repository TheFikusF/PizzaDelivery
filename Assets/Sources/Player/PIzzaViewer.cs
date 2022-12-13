using DG.Tweening;
using UnityEngine;

public class PIzzaViewer : MonoBehaviour
{
    [SerializeField] private Transform _pizzaParent;
    [SerializeField] private Transform _pizza;
    [SerializeField] private Transform _boxPivot;

    public void OpenPizzaBox()
    {
        _boxPivot.DORotate(new Vector3(-220, 0, 0), 1, RotateMode.LocalAxisAdd).OnComplete(() =>
        {
            _pizza.DOMoveY(_pizza.transform.position.y + 0.6f, 1).SetEase(Ease.OutCubic);
            _pizza.DOMoveZ(_pizza.transform.position.z + 2.6f, 1).SetEase(Ease.OutCubic);
        });
    }
}

using UnityEngine;
using UnityEngine.Events;

public class PedalControl : MonoBehaviour
{
    [Header("Controls objects")]
    [SerializeField] private RectTransform _pedal;
    [SerializeField] private float _pedalRadius = 200;
    
    [Header("Controller settings")]
    [SerializeField] private float _maxRollSpeed = 30;
    [SerializeField] private UnityEvent<float> _onPedalRoll = new();

    private float _prevAngles = 0;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            var direction = (Input.GetTouch(0).position - new Vector2(transform.position.x, transform.position.y)).normalized;
            _pedal.transform.localPosition = direction * _pedalRadius;
            
            var angles = Vector2.Angle(Vector2.left, direction);
            if(_pedal.localPosition.y < 0) angles = 360 - angles;

            var outputSpeed = Mathf.Clamp(Extensions.InverseLerpUnclamped(0, _maxRollSpeed * Time.deltaTime, angles - _prevAngles), -1, 1);

            _prevAngles = angles;
            _onPedalRoll.Invoke(outputSpeed);
        }
    }
}

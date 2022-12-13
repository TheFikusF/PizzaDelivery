using System.Collections;
using System.Collections.Generic;using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalProgressView : MonoBehaviour
{
    [SerializeField] private Slider _goalSlider;

    [Header("Distance text settings")]
    [SerializeField, Range(0, 1)] private float _showTextAfter;
    [SerializeField] private TextMeshProUGUI _distanceText;

    public void SetupSlider(Player player, Goal goal)
    {
        _goalSlider.maxValue = Vector3.Distance(player.transform.position, goal.transform.position);
        _goalSlider.value = 0;
        _distanceText.text = "";
    }

    public void UpdateSliderWithDistance(float distance)
    {
        _goalSlider.value = _goalSlider.maxValue - distance;
        if(_goalSlider.maxValue * _showTextAfter > distance)
        {
            _distanceText.text = string.Format("{0:0.0}m", distance);
        }
    }
}

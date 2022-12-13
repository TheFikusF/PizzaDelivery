using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadsController : MonoBehaviour
{
    [SerializeField] private RoadSpawner _spawner;
    [SerializeField] private int _maxRoadsActive = 2;
    private List<RoadSection> _roadSections = new();

    private void UpdateRoadsEnabled(int index)
    {
        _roadSections.ForEach(x => x.gameObject.SetActive(false));

        for(int i = index; i < Math.Min(_roadSections.Count, index + _maxRoadsActive); i++)
        {
            _roadSections[i].gameObject.SetActive(true);
        }

        for(int i = index; i >= Math.Max(0, index - _maxRoadsActive); i--)
        {
            _roadSections[i].gameObject.SetActive(true);
        }
    }

    private void GenerateRoad()
    {
        _spawner.GenerateRoad(_roadSections, (section) => { UpdateRoadsEnabled(_roadSections.IndexOf(section)); });
    }

    private void Start()
    {
        GenerateRoad();
        UpdateRoadsEnabled(0);
    }
}

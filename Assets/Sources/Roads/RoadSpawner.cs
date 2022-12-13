using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private Transform _roadCenter;
    [SerializeField] private int _maxRoadsInRow = 3;
    [SerializeField] private float _roadWidth = 10;
    [SerializeField] private int _maximumRoads = 15;

    [Header("Goal")]
    [SerializeField] private Goal _goal;

    [Header("Sections Presets")]
    [SerializeField] private RoadSection _startingGround;
    [SerializeField] private RoadSection _finishingGround;
    [SerializeField] private List<RoadSection> _grassSectionPresets;
    [SerializeField] private List<RoadSection> _roadSectionPresets;

    private List<RoadSection> _roadSections = new();
    private int _roadsSpawned = 0;
    private RoadSection LastSection => _roadSections.Last(x => x.transform.position.z == (_roadSections.Max(x => x.transform.position.z)));

    public int RoadsSpwaned => _roadsSpawned;

    public void GenerateRoad(List<RoadSection> roadSections, UnityAction<RoadSection> playerEnterCallBack)
    {
        _roadSections = roadSections;

        InstantiateSection(_startingGround, new Vector3(), playerEnterCallBack);
        Debug.Log(_roadSections.First());

        while (_roadsSpawned < _maximumRoads)
        {
            SpawnSection(playerEnterCallBack);
        }

        InstantiateSection(_finishingGround, playerEnterCallBack);
        var goalPosition = LastSection.transform.position - Vector3.forward * _roadWidth * 0.25f;
        _goal.InitGoal(goalPosition); 
    }

    private void SpawnSection(UnityAction<RoadSection> playerEnterCallBack)
    {
        switch(LastSection.SectionType)
        {
            case SectionType.Grass:
                var section = _roadSectionPresets.GetRandom();
                for (int i = 0; i < Random.Range(0, _maxRoadsInRow); i++)
                {
                    InstantiateSection(section, playerEnterCallBack);
                }
                break;
            case SectionType.Road:
                InstantiateSection(_grassSectionPresets.GetRandom(), playerEnterCallBack);
                break;
        }
    }

    private RoadSection InstantiateSection(RoadSection section, UnityAction<RoadSection> playerEnterCallBack = null)
    {
        return InstantiateSection(section, LastSection.transform.localPosition + new Vector3(0, 0, _roadWidth), playerEnterCallBack);
    }

    private RoadSection InstantiateSection(RoadSection section, Vector3 position, UnityAction<RoadSection> playerEnterCallBack = null)
    {
        var inst = Instantiate(section, _roadCenter);
        inst.transform.localPosition = position;
        inst.transform.rotation = Quaternion.Euler(0, 90, 0);
        _roadsSpawned++;
        _roadSections.Add(inst);
        
        if (playerEnterCallBack != null)
        {
            inst.AddListenerOnPlayerEnter(playerEnterCallBack);
        }

        return inst;
    }

    private void Awake()
    {
        foreach (RoadSection section in _grassSectionPresets) section.SectionType = SectionType.Grass; 
        foreach (RoadSection section in _roadSectionPresets) section.SectionType = SectionType.Road; 
    }
}

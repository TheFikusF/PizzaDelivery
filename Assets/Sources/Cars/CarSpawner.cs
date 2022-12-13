using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float _xOffset = 8;
    [SerializeField] private float _maxCarDistance = 16;
    [SerializeField] private Vector2 _spawnDelayBounds = new Vector2(0.5f, 3);

    private bool _canSpawn = true;
    private Car _currentCar;
    private CarMovingDirection _movingDirection = CarMovingDirection.Left;
    private float _timer = 0;
    private float _spawnDelay = 0;

    private Vector3 SpawnPosition => transform.position + new Vector3(_xOffset, 0, 0);

    private void AllowSpawn()
    {
        if(_canSpawn == false && _timer < _spawnDelay)
        {
            _timer += Time.deltaTime;
            return;
        }
        _timer = 0;
        _canSpawn = true;
    }

    private void StopSpawn()
    {
        _canSpawn = false;
        _spawnDelay = Random.Range(_spawnDelayBounds.x, _spawnDelayBounds.y);
    }

    private void Start()
    {
        if(Random.Range(0, 10) > 5) 
        {  
            _xOffset = -_xOffset; 
            _movingDirection = CarMovingDirection.Right;
        }
        StopSpawn();
    }

    private void SpawnCar()
    {
        if (!(_currentCar == null && _canSpawn == true))
        {
            return; 
        }
        _currentCar = CarFactory.CreateCar(SpawnPosition, _movingDirection);
    }

    private void DeleteCar()
    {
        if (_currentCar == null) return;
        if (Vector3.Distance(_currentCar.transform.position, SpawnPosition) < _maxCarDistance) return;
        
        StopSpawn();
        Destroy(_currentCar.gameObject);
        _currentCar = null;
    }

    private void Update()
    {
        AllowSpawn();
        SpawnCar();
        DeleteCar();
    }
}
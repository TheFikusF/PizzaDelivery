using UnityEngine;
using System.Collections.Generic;

public class CarFactory : MonoBehaviour
{
    private static CarFactory _instance;
    [SerializeField] private List<GameObject> _carsModels;
    [SerializeField] private Vector2 _carSpeedBounds = new Vector3(3, 6);
    [SerializeField] private Vector3 _carColliderSize = new Vector3(4, 1, 1);

    private void Awake()
    {
        _instance = this;
    }

    public static Car CreateCar(Vector3 position, CarMovingDirection movingDirection)
    {
        return CreateCar(_instance._carsModels.GetRandom(), position, movingDirection);
    }

    public static Car CreateCar(GameObject car, Vector3 position, CarMovingDirection movingDirection)
    {
        var parent = new GameObject().transform;
        var hitboxTransform = new GameObject().transform;
        hitboxTransform.parent = parent;
        parent.transform.position = position;

        var graphics = SetupGraphics(parent, car, movingDirection);


        var carComponent = SetupCarComponent(parent, movingDirection);

        var hitbox = SetupCarHitbox(hitboxTransform, carComponent);

        return carComponent;
    }

    private static GameObject SetupGraphics(Transform carParent, GameObject car, CarMovingDirection movingDirection)
    {
        var graphics = Instantiate(car, carParent);

        if (movingDirection == CarMovingDirection.Right) graphics.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        if (movingDirection == CarMovingDirection.Left) graphics.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

        return graphics;
    }

    private static CarHitBox SetupCarHitbox(Transform hitboxTransform, Car carComponent)
    {
        var hitbox = hitboxTransform.gameObject.AddComponent<CarHitBox>();
        hitbox.Car = carComponent;

        var collider = hitboxTransform.gameObject.AddComponent<BoxCollider>();
        collider.center = new Vector3(0, 1, 0);
        collider.size = _instance._carColliderSize;

        return hitbox;
    }

    private static Car SetupCarComponent(Transform carParent, CarMovingDirection movingDirection)
    {
        var characterController = carParent.gameObject.AddComponent<CharacterController>();
        characterController.center = new Vector3(0, 1, 0);

        var carComponent = carParent.gameObject.AddComponent<Car>();
        carComponent.Init(movingDirection, Random.Range(_instance._carSpeedBounds.x, _instance._carSpeedBounds.y));

        return carComponent;
    }

}
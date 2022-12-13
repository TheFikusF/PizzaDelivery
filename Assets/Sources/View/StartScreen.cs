using UnityEngine;
using UnityEngine.Events;

public class StartScreen : MonoBehaviour 
{
    [SerializeField] private UnityEvent _onGameStart;

    public void StartGame()
    {
        _onGameStart.Invoke();
    }
}
using Managers;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CardsManager _cardsManager;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private FlowManager _flowManager;
    [SerializeField] private LevelManager _levelManager;

    [SerializeField] private AnimationQueue _animationQueue;

    private void Start()
    {
        _animationQueue.Init();
        _levelManager.Init(_animationQueue);
        _cardsManager.Init(_animationQueue);
        _enemyManager.Init(_animationQueue);
        _flowManager.Init(_animationQueue);
    }

}
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] private ActorStatsView statsView;
    [SerializeField] [SceneObjectsOnly] private GameObject modelPrefab;
    
    private float originalScale;
    private const float AttackScaleFactor = 1.2f;

    public void Init(FightPhaseActorInstance model)
    {
        originalScale = modelPrefab.transform.localScale.x;
    }

    private void ChangeCurrentHealth(int currentHealth)
    {
        statsView.ChangeCurrentHealth(currentHealth);
    }

    public void ShowAttackAnimation()
    {
        modelPrefab.transform.localScale = Vector3.one * (originalScale * AttackScaleFactor);
    }

    public void HideAttackAnimation()
    {
        modelPrefab.transform.localScale = Vector3.one * originalScale;
    }
}
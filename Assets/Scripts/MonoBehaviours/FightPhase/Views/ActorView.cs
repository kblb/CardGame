using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorView : MonoBehaviour
{
    [SerializeField] [SceneObjectsOnly] private ActorStatsView statsView;
    [SerializeField] private GameObject modelPrefab;
    private GameObject model;
    
    private float originalScale;
    private const float AttackScaleFactor = 1.2f;

    public void Init(FightPhaseActorInstance actor)
    {
        transform.localScale = Vector3.zero;
        statsView.Init(actor);
        model = Instantiate(modelPrefab, transform);
        originalScale = model.transform.localScale.x;
        transform.DOScale(Vector3.one, 0.5f);
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
using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class JewelsFrameView : MonoBehaviour
{
    [SerializeField, AssetsOnly] public JewelView jewelItemPrefab;
    [SerializeField, SceneObjectsOnly] public GameObject[] slots;

    public event Action<JewelView> OnJewelFinishedAnimatingToSlot;

    public void SpawnItemViewAndAnimateToSlot(int slotIndex, JewelInstance item, Vector3 spawnPosition)
    {
        spawnPosition.z = 0;

        JewelView jewelView = Instantiate(jewelItemPrefab, slots[slotIndex].transform);
        jewelView.transform.position = spawnPosition;
        jewelView.Initialize(item);
        jewelView.transform
            .DOMove(slots[slotIndex].transform.position, 2f)
            .OnComplete(() => { OnJewelFinishedAnimatingToSlot?.Invoke(jewelView); });
    }
}
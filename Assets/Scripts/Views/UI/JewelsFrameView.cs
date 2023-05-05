using System;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class JewelsFrameView : MonoBehaviour
{
    [SerializeField, AssetsOnly] public JewelView jewelItemPrefab;
    [SerializeField, SceneObjectsOnly] public GameObject[] slots;

    public readonly List<JewelView> jewelViews = new List<JewelView>();

    public event Action<JewelView> OnJewelFinishedAnimatingToSlot;

    public void SpawnItemViewAndAnimateToSlot(int slotIndex, JewelInstance item, Vector3 spawnPosition)
    {
        spawnPosition.z = 0;
        JewelView jewelView = Instantiate(jewelItemPrefab, slots[slotIndex].transform);
        jewelViews.Add(jewelView);
        jewelView.transform.position = spawnPosition;
        jewelView.Initialize(item);
        jewelView.transform
            .DOMove(slots[slotIndex].transform.position, 2f)
            .OnComplete(() => { OnJewelFinishedAnimatingToSlot?.Invoke(jewelView); });
    }

    public void ReturnAllJewelsToSlots()
    {
        foreach (GameObject slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                slot.transform.GetChild(0).transform.DOMove(slot.transform.position, 0.5f).SetEase(Ease.OutBack);
            }
        }
    }
}
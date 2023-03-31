using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class JewelsFrameView : MonoBehaviour
{
    [SerializeField, AssetsOnly] public ItemView jewelItemPrefab;
    [SerializeField, SceneObjectsOnly] public GameObject[] slots;

    public event Action<ItemView> OnJewelFinishedAnimatingToSlot;

    public void SpawnItemViewAndAnimateToSlot(int slotIndex, AInventoryItemInstance item, Vector3 spawnPosition)
    {
        spawnPosition.z = 0;

        ItemView itemView = Instantiate(jewelItemPrefab, slots[slotIndex].transform);
        itemView.transform.position = spawnPosition;
        itemView.Initialize(item);
        itemView.transform
            .DOMove(slots[slotIndex].transform.position, 2f)
            .OnComplete(() => { OnJewelFinishedAnimatingToSlot?.Invoke(itemView); });
    }
}
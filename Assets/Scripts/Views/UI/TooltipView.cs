using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipView : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;

    private void Update()
    {
        Vector2 pos = Input.mousePosition;
        pos.x++;
        pos.y--;
        transform.position = pos;

         GameObject g = RaycastUtilities.GetUIElement(Input.mousePosition);
         if (g != null &&  g.GetComponentInParent<ITooltipable>() != null)
         {
             ITooltipable tooltipable = g.GetComponentInParent<ITooltipable>();
             Show(tooltipable.GetTooltipText());
         }
         else
         {
             Hide();
         }
    }

    private void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Show(string text)
    {
        GetComponent<CanvasGroup>().alpha = 1;
        titleText.text = text;
    }
}

public static class RaycastUtilities
{
    public static GameObject GetUIElement(Vector2 screenPos)
    {
        GameObject hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject;
        //return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
    }
 
    public static GameObject UIRaycast (PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
 
        return results.Count < 1 ? null : results[0].gameObject;
    }
 
    static PointerEventData ScreenPosToPointerData (Vector2 screenPos)
        => new(EventSystem.current){position = screenPos};
}
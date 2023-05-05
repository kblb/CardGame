using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        if (g != null
            && g.GetComponentInParent<ITooltipable>() != null
            && Input.GetMouseButton(0) == false)
        {
            Show(g.GetComponentInParent<ITooltipable>().GetTooltipText());
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        GetComponent<Image>().enabled = false;
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public void Show(string text)
    {
        GetComponent<Image>().enabled = true;
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

    public static GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count < 1 ? null : results[0].gameObject;
    }

    static PointerEventData ScreenPosToPointerData(Vector2 screenPos)
        => new(EventSystem.current)
        {
            position = screenPos
        };
}
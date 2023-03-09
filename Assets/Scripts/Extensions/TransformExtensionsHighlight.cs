using UnityEngine;

public static class TransformExtensionsHighlight
{
    public static void Highlight(this Transform t)
    {
        t.localScale *= 1.2f;
    }

    public static void TurnOffHighlight(this Transform t)
    {
        t.localScale /= 1.2f;
    }
}

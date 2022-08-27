using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(menuName = "Create/Card")]
    public class Card : ScriptableObject
    {
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public float damage;
    }
}
using Cards.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(menuName = "Create/Card")]
    public class Card : SerializedScriptableObject
    {
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public ICardEffect[] effects;
    }
}
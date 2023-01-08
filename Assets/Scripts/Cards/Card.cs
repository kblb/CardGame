using System;
using Cards.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Cards
{
    [Flags]
    public enum PlayerEffect : byte
    {
        None = 0,
        Heal = 1 << 0,
        Buff = 1 << 1,
    }

    [Flags]
    public enum EnemyEffect : byte
    {
        None = 0,
        Damage = 1 << 0,
        Debuff = 1 << 1,
        Other = 1 << 2,
    }

    
    public struct Intent
    {
        public PlayerEffect PlayerEffect;
        public EnemyEffect[] EnemyEffects;

        public void Merge(Intent intent)
        {
            PlayerEffect |= intent.PlayerEffect;
            for (var i = 0; i < EnemyEffects.Length; i++)
            {
                EnemyEffects[i] |= intent.EnemyEffects[i];
            }
        }
    }
    
    [CreateAssetMenu(menuName = "Create/Card")]
    public class Card : SerializedScriptableObject
    {
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public ICardEffect[] effects;

        public Intent GetIntent()
        {
            var def = new Intent()
            {
                PlayerEffect = PlayerEffect.None,
                EnemyEffects = new EnemyEffect[5]
            };
            foreach (var effect in effects)
            {
                var intent = effect.Intents();
                def.Merge(intent);
            }
            return def;
        }
    }
}
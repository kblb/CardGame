using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Registries
{
    public class BattleIconRegistry : MonoBehaviour
    {
        private static BattleIconRegistry Instance { get; set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [SerializeField, AssetsOnly]
        private Sprite shieldIcon;
        public static Sprite ShieldIcon => Instance.shieldIcon;
        
        [SerializeField, AssetsOnly]
        private Sprite swordIcon;
        public static Sprite SwordIcon => Instance.swordIcon;
        
        [SerializeField, AssetsOnly]
        private Sprite healIcon;
        public static Sprite HealIcon => Instance.healIcon;
        
        [SerializeField, AssetsOnly]
        private Sprite fireIcon;
        public static Sprite FireIcon => Instance.fireIcon;
        
        [SerializeField, AssetsOnly]
        private Sprite unknownIcon;
        public static Sprite UnknownIcon => Instance.unknownIcon;
    }
}
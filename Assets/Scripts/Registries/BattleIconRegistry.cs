using Sirenix.OdinInspector;
using UnityEngine;

namespace Registries
{
    public class BattleIconRegistry : MonoBehaviour
    {

        [SerializeField] [AssetsOnly] private Sprite shieldIcon;

        [SerializeField] [AssetsOnly] private Sprite swordIcon;

        [SerializeField] [AssetsOnly] private Sprite healIcon;

        [SerializeField] [AssetsOnly] private Sprite fireIcon;

        [SerializeField] [AssetsOnly] private Sprite unknownIcon;
        private static BattleIconRegistry Instance { get; set; }
        public static Sprite ShieldIcon => Instance.shieldIcon;
        public static Sprite SwordIcon => Instance.swordIcon;
        public static Sprite HealIcon => Instance.healIcon;
        public static Sprite FireIcon => Instance.fireIcon;
        public static Sprite UnknownIcon => Instance.unknownIcon;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}
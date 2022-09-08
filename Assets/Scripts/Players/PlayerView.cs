using System.Globalization;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField, SceneObjectsOnly]
        private TMP_Text healthText;

        public void Init(PlayerModel model)
        {
            model.OnHealthChanged += OnHealthChanged;
        }
        
        private void OnHealthChanged(float newValue)
        {
            healthText.text = newValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
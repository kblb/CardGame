using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Cards
{
    public class DiscardPileView : MonoBehaviour
    {
        [SerializeField, SceneObjectsOnly] private TMP_Text statusText;
        private int count = 0; 
        
        public void AddCard()
        {
            count++;
            statusText.text = $"Discarded: {count}";    
        }
        
        public void Zero()
        {
            count = 0;
            statusText.text = $"Discarded: {count}";
        }
    }
}
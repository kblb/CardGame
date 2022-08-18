using UnityEngine;

namespace Cards
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private float spacing = 30f;
        private Transform _thisTransform;

        private void Awake()
        {
            _thisTransform = transform;
        }

        public void AddCard(CardView cardObject)
        {
            cardObject.transform.SetParent(_thisTransform);
            cardObject.SetOnExitDragNotificationListener(Redraw);
            Redraw();
        }

        /// <summary>
        ///     Places all children in horizontal layout starting from the center
        /// </summary>
        public void Redraw()
        {
            var i = 0;
            var childCount = _thisTransform.childCount;
            var offset = (childCount - 1) * spacing / 2;
            foreach (Transform child in _thisTransform)
            {
                child.localPosition = new Vector3(i * spacing - offset, 0, 0);
                i++;
            }
        }
    }
}
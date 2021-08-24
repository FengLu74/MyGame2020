using UnityEngine;


namespace Common.General
{
    public class CachedMono : MonoBehaviour
    {
        private GameObject _cachedGameObject_1 = null;
        private Transform _cachedTransform_1 = null;

        public GameObject CachedGameObject
        {
            get
            {
                if (_cachedGameObject_1 == null)
                    _cachedGameObject_1 = gameObject;
                return _cachedGameObject_1;
            }
        }

        public Transform CachedTransform
        {
            get
            {
                if (_cachedTransform_1 == null)
                    _cachedTransform_1 = transform;
                return _cachedTransform_1;
            }
        }
    }
}
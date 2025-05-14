using UnityEngine;

namespace Game.UI.Utils
{
    public class RectTransformRotator : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 90f;
        [SerializeField] private Vector3 _rotationAxis = Vector3.forward;
        [SerializeField] private RectTransform _rectTransform;

        private void Update()
        {
            _rectTransform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime);
        }
    }
}

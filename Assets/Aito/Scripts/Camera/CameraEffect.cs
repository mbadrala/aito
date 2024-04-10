using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aito
{
    public enum CameraEffectType
    {
        Shake
    }

    public class CameraEffect : MonoBehaviour
    {
        private Camera _camera;
        [SerializeField]
        private CameraEffectType _type;
        [SerializeField]
        private float _duration;
        [SerializeField]
        private float _t;

        private void Start()
        {
            _camera = GetComponent<Camera>();

            if (_camera.transform.parent == null)
            {
                Vector3 pos = _camera.transform.position;
                GameObject cameraParent = new GameObject("CameraParent");
                cameraParent.transform.position = pos;
                _camera.transform.SetParent(cameraParent.transform);
                _camera.transform.localPosition = Vector3.zero;
            }
        }

        private void Shake()
        {
            StartCoroutine(ShakeCor(_duration, _t));
        }

        private IEnumerator ShakeCor(float duration, float magnitude)
        {
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                _camera.transform.localPosition = transform.TransformDirection(
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * magnitude
                    );
                yield return null;
            }
            _camera.transform.localPosition = Vector3.zero;

        }

        public void Action()
        {
            switch (_type)
            {
                case CameraEffectType.Shake:
                    Shake();
                    break;
                default:
                    Debug.LogError("Camera effect type is not set");
                    break;
            }
        }
    }
}

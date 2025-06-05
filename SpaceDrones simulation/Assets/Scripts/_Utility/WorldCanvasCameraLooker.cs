using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class WorldCanvasCameraLooker : MonoBehaviour
{
    private Canvas _selfCanvas;
    private Transform _cachedTransform;

    private Camera _cachedCamera;
    private Transform _cachedCameraTransform;

    private void Awake()
    {
        _selfCanvas = GetComponent<Canvas>();
        _cachedTransform = transform;

        TryInitialize();
    }

    private void TryInitialize()
    {
        _cachedCamera = Camera.main;

        if (_cachedCamera != null)
        {
            _selfCanvas.worldCamera = _cachedCamera;
            _cachedCameraTransform = _cachedCamera.transform;   
        }
    }

    private void Update()
    {
        if (_cachedCamera == null)
        {
            TryInitialize();
            return;
        }

        _cachedTransform.LookAt(_cachedCameraTransform);
    }
}
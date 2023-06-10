using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Game
{
    public class GameInputReader : MonoBehaviour, IGameInputService
    {
        public event Action OnScreenTouchInput;
        private List<RaycastResult> _uiRaycastBuffer;

        private void Awake()
        {
            _uiRaycastBuffer = new List<RaycastResult>();
        }

        private void Update()
        {
            if (HasTouched() && NotOverUI())
                OnScreenTouchInput?.Invoke();
        }

        private bool NotOverUI()
        {
            var eventData = new PointerEventData(EventSystem.current);
#if UNITY_EDITOR
            eventData.position = Input.mousePosition;
#else
            eventData.position = Input.GetTouch(0).position;
#endif
            EventSystem.current.RaycastAll(eventData, _uiRaycastBuffer);
            
            return _uiRaycastBuffer.Count == 0;
        }

        private bool HasTouched()
        {
#if UNITY_EDITOR
            return Input.GetMouseButtonDown(0);
#endif
            return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        }
    }
}
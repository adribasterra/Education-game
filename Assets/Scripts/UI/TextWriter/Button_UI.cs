using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Utils
{
    public class Button_UI : MonoBehaviour, IPointerClickHandler
    {
        public Action onClickFunc = null;
        public Action<PointerEventData> OnPointerClickFunc;
               
        public virtual void OnPointerClick(PointerEventData eventData) {
            OnPointerClickFunc?.Invoke(eventData);
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    onClickFunc?.Invoke();
                }
            }
                
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                if (eventData.clickCount > 0)
                {
                    onClickFunc?.Invoke();
                }
            }
        }
    }
}
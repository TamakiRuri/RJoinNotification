using UdonSharp;
using UnityEngine;
using VRC.Udon;

namespace com.rurinya.joinnotification
{
    public class RStateReporter : UdonSharpBehaviour
    {
        [SerializeField] private UdonBehaviour targetBehavior;
        [SerializeField] private string targetEventName;

        void OnEnable() => SendEvent();
        void OnDisable() => SendEvent();

        private void SendEvent() => targetBehavior.SendCustomEvent(targetEventName);
    }
}


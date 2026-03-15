
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


namespace com.rurinya.joinnotification
{
    public class RStateReporter : UdonSharpBehaviour
    {
        [SerializeField] private UdonBehaviour targetBehavior;
        [SerializeField] private string eventName;
        void Start()
        {
            
        }
        void OnEnable()
        {
            targetBehavior.SendCustomEvent(eventName);
        }
        void OnDisable()
        {
            targetBehavior.SendCustomEvent(eventName);
        }
    }
}


using System;
using UnityEngine;
using UnityEngine.Events;

namespace GMessage
{
    public abstract class UnityListener<T> : MonoListener<T> where T : IDispatcher
    {
        [SerializeField]
        private Trigger[] _triggers;

        public override void Receive(IMessage message)
        {
            if (message.ID == UNITY_COMMAND)
            {
                var cmd = message.Content.ToString();
                for (int i = 0; i < _triggers.Length; i++)
                {
                    var trigger = _triggers[i];

                    if (string.Equals(trigger.command, cmd))
                    {
                        trigger.action.Invoke();
                    }
                }
            }
        }

        private void Awake() { Register(UNITY_COMMAND); }

        private void OnDestroy() { UnRegister(UNITY_COMMAND); }

        [Serializable]
        private class Trigger
        {
            public string command;
            public UnityEvent action;
        }

        /// <summary>
        /// Unity 内的消息派发命令
        /// </summary>
        [System.ComponentModel.Description("Unity 内的消息派发命令")]
        public const int UNITY_COMMAND = -1;
    }
}
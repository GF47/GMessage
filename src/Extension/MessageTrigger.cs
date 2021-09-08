using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GMessage
{
    public class MessageTrigger : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler,
        ISelectHandler,
        IDeselectHandler,
        IUpdateSelectedHandler,
        ISubmitHandler,
        ICancelHandler
    {

        public int moduleID = Dispatcher.GLOBAL;

        [Serializable]
        public struct MessageData : IEquatable<MessageData>
        {
            public EventTriggerType type;
            public int id;
            public string content;
            public Func<object> commonContent;

            public bool Equals(MessageData b)
            {
                return type == b.type && id == b.id && content == b.content && commonContent == b.commonContent;
            }
        }
        public List<MessageData> Triggers => _triggers;
        [SerializeField]
        private List<MessageData> _triggers = new List<MessageData>();

        private Dispatcher GetDispatcher()
        {
            return Dispatcher.Instance<Dispatcher>(moduleID);
        }

        #region 响应方法

        private void Execute(EventTriggerType type)
        {
            for (int i = 0; i < _triggers.Count; i++)
            {
                if (_triggers[i].type == type)
                {
                    new Message(_triggers[i].id, this,
                                _triggers[i].commonContent == null ? _triggers[i].content : _triggers[i].commonContent()
                    ).DispatchTo(GetDispatcher());
                }
            }
        }

        public void OnCancel(BaseEventData eventData)
        {
            Execute(EventTriggerType.Cancel);
        }

        public void OnDeselect(BaseEventData eventData)
        {
            Execute(EventTriggerType.Deselect);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Execute(EventTriggerType.PointerClick);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Execute(EventTriggerType.PointerDown);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Execute(EventTriggerType.PointerEnter);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Execute(EventTriggerType.PointerExit);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Execute(EventTriggerType.PointerUp);
        }

        public void OnSelect(BaseEventData eventData)
        {
            Execute(EventTriggerType.Select);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            Execute(EventTriggerType.Submit);
        }

        public void OnUpdateSelected(BaseEventData eventData)
        {
            Execute(EventTriggerType.UpdateSelected);
        }

        #endregion
    }
}

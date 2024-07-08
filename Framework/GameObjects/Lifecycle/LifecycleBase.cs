using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Lifecycle
{
    public abstract class LifecycleBase : MonoBehaviour
    {
        #region Action
        // ����
        protected void OnDeath()
        {
            deathEvent?.Invoke();
            // TODO�����ն���
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        // ����ɱ
        public void ForceKill()
        {
            Debug.Log("[Info] ǿ����ֹ����������");
            this.OnDeath();
        }

        // ��������
        public abstract void ResetLifecycle();
        #endregion

        #region Init
        private void OnEnable()
        {
            ResetLifecycle();
        }
        #endregion


        #region Event
        // �����ص��¼�
        private UnityEvent deathEvent;

        public void RegisterDeathEvent(UnityAction action)
        {
            deathEvent ??= new UnityEvent();
            deathEvent.AddListener(action);
        }
        public void RemoveDeathEvent(UnityAction action)
        {
            deathEvent.RemoveListener(action);
        }
        #endregion

    }

}




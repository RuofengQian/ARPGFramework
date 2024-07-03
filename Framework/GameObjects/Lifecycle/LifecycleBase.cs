using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Lifecycle
{
    public abstract class LifecycleBase<TDerived> : MonoBehaviour
        where TDerived : LifecycleBase<TDerived>
    {
        #region Death
        protected void Death()
        {
            deathEvent?.Invoke(gameObject); // DeathEvent������������Ч�ص���ҲҪ���������߼�
            // TODO�����ն���
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        #endregion

        #region Event
        private UnityEvent<GameObject> deathEvent;

        public void RegisterDeathEvent(UnityAction<GameObject> action)
        {
            deathEvent ??= new UnityEvent<GameObject>();
            deathEvent.AddListener(action);
        }
        public void RemoveDeathEvent(UnityAction<GameObject> action)
        {
            deathEvent.RemoveListener(action);
        }
        #endregion


        #region Poly
        // ��������
        public abstract void ResetLifecycle();

        public static TDerived Attach(GameObject gameObject, int value)
        {
            return gameObject.AddComponent<TDerived>();
        }
        public static TDerived Attach(GameObject gameObject, float value)
        {
            return gameObject.AddComponent<TDerived>();
        }
        #endregion

    }

}




using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Lifecycle
{
    public abstract class LifecycleBase : MonoBehaviour
    {
        #region Action
        // 死亡
        protected void OnDeath()
        {
            deathEvent?.Invoke();
            // TODO：回收对象
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        // 代码杀
        public void ForceKill()
        {
            Debug.Log("[Info] 强行终止了生命周期");
            this.OnDeath();
        }

        // 重置属性
        public abstract void ResetLifecycle();
        #endregion

        #region Init
        private void OnEnable()
        {
            ResetLifecycle();
        }
        #endregion


        #region Event
        // 死亡回调事件
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




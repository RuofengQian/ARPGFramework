using MyFramework.Manager.GameTick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Lifecycle
{
    // ������������
    public enum LifecycleType
    {
        // ʵ������
        EntityHealth = 0,
        // ������
        SimpleHealth = 1,
        // �ܻ�����
        DamagedTimes = 2,

        // ��������
        AttackedTimes = 3,
        
        // ����ʱ
        CountDown = 4,

    }

    // ����������𣺾�������ʽ
    public enum LifecycleGroup
    {
        // ���ڱ����˺���
        Passive,
        // ���������ж���
        Active,
        // ����ʱ���
        TimeBased,
    }
    
    // �������ڿ�����������ࣩ
    public class LifecycleController : MonoBehaviour
    {
        // ����ʽ��������
        #region Lifecycle.Passive
        // �����˺�����������
        private IDamageBasedLifecycle _damageBasedLifecycle;
        public IDamageBasedLifecycle damageLife { get => _damageBasedLifecycle; private set => _damageBasedLifecycle = value; }

        // �˺�������Ч��
        public bool damageLifeValid { get => (damageLife != null); }
        public bool damageLifeInvalid { get => (damageLife == null); }


        public bool TryTakeDamage(Damage damage)
        {
            return damageLifeValid && damageLife.allowDamage && damageLife.TakeDamage(damage);
        }
        public bool TryTakeHeal(Heal heal)
        {
            return damageLifeValid && damageLife.allowHeal && damageLife.TakeHeal(heal);
        }
        #endregion

        // ��������������
        #region Lifecycle.Active
        // �����ж�����������
        private IActionBasedLifecycle _actionBasedLifecycle;
        public IActionBasedLifecycle actionLife { get => _actionBasedLifecycle; private set => _actionBasedLifecycle = value; }

        // �ж�������Ч��
        public bool actionLifeValid { get => (actionLife != null); }
        public bool actionLifeInvalid { get => (actionLife == null); }


        public bool TryAction()
        {
            return actionLifeValid && actionLife.CostActionTime(1);
        }
        #endregion

        // ��ʱ����������
        #region Lifecycle.Time
        // ����ʱ�����������
        private ITimeBasedLifecycle _timeBasedLifecycle;
        public ITimeBasedLifecycle timeLife { get => _timeBasedLifecycle; private set => _timeBasedLifecycle = value; }

        // ʱ��������Ч��
        public bool timeLifeValid { get => (timeLife != null); }
        public bool timeLifeInvalid { get => (timeLife == null); }
        #endregion


        #region AddComponent
        // �����������ӳ��
        private static readonly Dictionary<LifecycleType, LifecycleGroup> groupMap = new()
        {
            { LifecycleType.EntityHealth, LifecycleGroup.Passive },
            { LifecycleType.SimpleHealth, LifecycleGroup.Passive },
            { LifecycleType.DamagedTimes, LifecycleGroup.Passive },

            { LifecycleType.AttackedTimes, LifecycleGroup.Active },

            { LifecycleType.CountDown, LifecycleGroup.TimeBased },
        };
        // �����������ӳ��
        private static readonly Dictionary<LifecycleType, UnityAction<GameObject, object>> actionMap = new()
        {
            // TODO���󶨸��� Lifecycle �Ĵ�����ʼ������
        };


        public static void AttachLifecycleComponent<T>(GameObject attachGameObject, LifecycleType type, T data)
        {
            LifecycleController lifeController = null;
            if(!attachGameObject.TryGetComponent<LifecycleController>(out lifeController))
            {
                lifeController = attachGameObject.AddComponent<LifecycleController>();
            }

            if( !groupMap.ContainsKey(type) )
            {
                Debug.LogError($"[Error] LifecycleController �в����� Key: {type} ��Ӧ�� Value: Group������Ӹü�ֵ��");
                return;
            }
            else if( groupMap[type] == LifecycleGroup.Passive && lifeController.damageLifeValid )
            {
                Debug.LogError($"[Error] ��� {type} ʧ�ܣ� damageLife�Ѵ���");
                return;
            }
            else if( groupMap[type] == LifecycleGroup.Active && lifeController.actionLifeValid )
            {
                Debug.LogError($"[Error] ��� {type} ʧ�ܣ�actionLife�Ѵ���");
                return;
            }
            else if( groupMap[type] == LifecycleGroup.TimeBased && lifeController.timeLifeValid )
            {
                Debug.LogError($"[Error] ��� {type} ʧ�ܣ�timeLife�Ѵ���");
                return;
            }

            // ִ�д�������
            actionMap[type].Invoke(attachGameObject, data);
        }
        #endregion

    }

}




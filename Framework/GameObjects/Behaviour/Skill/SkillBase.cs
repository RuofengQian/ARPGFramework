using MyFramework.GameObjects.Attribute;
using MyFramework.GameObjects.Group;
using MyFramework.GameObjects.Lifecycle;
using MyFramework.Manager.GameTick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour
{
    // SkillBase���۲� Entity �����ԣ�ʵ��ͨ�÷���
    public abstract class SkillBase : MonoBehaviour
    {
        #region Attribute
        // ��Ϸʱ�������
        protected static GameTickManager gameTickManager = GameTickManager.Instance;
        // ��Ӫ������
        protected static GroupManager GroupManager = GroupManager.Instance;


        // �۲�� Entity ����
        private EntityAttribute _entityAttr;
        protected EntityAttribute entityAttr { get => _entityAttr; private set => _entityAttr = value; }

        // ���ܴ�����ʽ
        private EventTriggerMode _triggerMode = EventTriggerMode.Once;
        protected EventTriggerMode triggerMode { get => _triggerMode; private set => _triggerMode = value; }


        public void SetTriggerMode(EventTriggerMode triggerMode = EventTriggerMode.Once)
        {
            this.triggerMode = triggerMode;
        }
        public void BindEntityAttr(EntityAttribute entityAttr)
        {
            this.entityAttr = entityAttr;
        }
        #endregion

        #region Init
        // �������Ż�������������������
        private LifeAttackedTime _lifeAttackTime;
        protected LifeAttackedTime lifeAttackTime { get => _lifeAttackTime; private set => _lifeAttackTime = value; }


        private void Start()
        {
            // ���û�б���ֵ��˵������һ����Ч�ļ��ܣ�ɾ�����������
            if (entityAttr == null)
            {
                Debug.LogError("[Error] ����û�б�����۲�� EntityAttribute ʵ��");
                Destroy(gameObject);
                return;
            }
            // ��ͼѰ�ҹ����������
            gameObject.TryGetComponent<LifeAttackedTime>(out _lifeAttackTime);
        }
        #endregion


        #region Action
        // ע�⣺������ܳ�Ϊ����ƿ��
        // OnTriggerEnter���������ܽ�����Ϊ���ɸ�����Ϸ����������
        private void OnTriggerEnter(Collider other)
        {
            // ֻ������1��
            if( contactedInstanceID.Contains(other.GetInstanceID()) )
            {
                return;
            }
            contactedInstanceID.Add(other.GetInstanceID());

            // �ڲ�ͬ��Ӫ��ʩ���˺�/DeBuff
            if ( InDiffGroup(other.gameObject) )
            {
                InteractDiffGroup(other.gameObject);
            }
            // ����ͬ��Ӫ��ʩ������/Buff
            else if( InSameGroup(other.gameObject) )
            {
                InteractSameGroup(other.gameObject);
            }
        }

        // �Ż�������ͨ����д��������������ȥ�����ֲ���Ҫ���߼�
        protected virtual void InteractDiffGroup(GameObject other)
        {
            // ʩ�� DeBuff
            if (this is IDeBuffSkill deBuffSkill && SkillBehaviour.AttachBuff(other, deBuffSkill.GetBuff()))
            {
                // TODO
            }
            // ʩ���˺�
            if (this is IDamageSkill damageSkill && SkillBehaviour.DealDamage(other, damageSkill.GetDamage()))
            {
                if (lifeAttackTime != null)
                {
                    lifeAttackTime.DoAttack();
                }
            }
        }
        protected virtual void InteractSameGroup(GameObject other)
        {
            bool flag = false;
            // ʩ�� Buff
            if (this is IBuffSkill buffSkill && SkillBehaviour.AttachBuff(other, buffSkill.GetBuff()))
            {
                flag = true;
            }
            // ʩ������
            if (this is IHealSkill healSkill && SkillBehaviour.DealHeal(other, healSkill.GetHeal()))
            {
                flag = true;
            }
            //return flag;
        }
        #endregion

        #region Group
        // �ж�λ�ڲ�ͬ��Ӫ
        protected bool InDiffGroup(GameObject other)
        {
            return (
                GroupManager.GetInstanceGroup(gameObject.GetInstanceID()) !=
                GroupManager.GetInstanceGroup(other.GetInstanceID())
                );
        }
        // �ж�λ����ͬ��Ӫ
        protected bool InSameGroup(GameObject other)
        {
            return (
                GroupManager.GetInstanceGroup(gameObject.GetInstanceID()) ==
                GroupManager.GetInstanceGroup(other.GetInstanceID())
                );
        }
        #endregion

        #region EffectIntv
        private HashSet<int> contactedInstanceID = new();

        private float effectIntv = 0.5f;

        public void SetEffectIntv(float val = 0.25f)
        {
            if( val < 0.1f || val > 1f )
            {
                return;
            }
            effectIntv = val;
        }

        private IEnumerator EffectCountDown(int instID)
        {
            yield return gameTickManager.WaitForGameSeconds(effectIntv);
            contactedInstanceID.Remove(instID);
        }
        #endregion

    }

}





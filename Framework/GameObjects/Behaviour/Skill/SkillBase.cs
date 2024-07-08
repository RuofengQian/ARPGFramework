using MyFramework.GameObjects.Attribute;
using MyFramework.GameObjects.Group;
using MyFramework.Manager.GameTick;
using MyProject.GameObjects.Tags;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // SkillBase���۲� Entity �����ԣ�ʵ��ͨ�÷���
    public abstract class SkillBase : MonoBehaviour
    {
        #region Attribute
        // ��Ϸʱ�������
        protected static GameTimeManager gameTickManager = GameTimeManager.Instance;
        // ��Ӫ������
        protected static GroupManager groupManager = GroupManager.Instance;


        // �۲�� Entity ����
        private EntityAttribute _entityAttr;
        protected EntityAttribute entityAttr { get => _entityAttr; private set => _entityAttr = value; }

        public void BindEntityAttr(EntityAttribute entityAttr)
        {
            this.entityAttr = entityAttr;
        }
        #endregion

        #region Init
        private void Start()
        {
            // ���û�б���ֵ��˵������һ����Ч�ļ��ܣ�ɾ�����������
            if (entityAttr == null)
            {
                Debug.LogError("[Error] ����û�б�����۲�� EntityAttribute ʵ��");
                Destroy(gameObject);
                return;
            }
        }
        #endregion


        #region Action
        // �Ӵ���ײ�� ID ����
        protected HashSet<int> contactedColliderIdSet = new();

        // OnTriggerEnter�����������߼�
        protected void OnTriggerEnter(Collider other)
        {
            if( contactedColliderIdSet.Contains(other.GetInstanceID()) )
            {
                return;
            }
            contactedColliderIdSet.Add(other.GetInstanceID()); // ��ֹ�ڱ�Ե��ײ��ʱ��δ��� OnEnter

            OnEnterArea(other.gameObject);
        }
        // OnDisable���ݴ��߼�
        protected void OnDisable()
        {
            // �����ײ��¼
            contactedColliderIdSet.Clear();
        }
        // OnDestroy�������߼�
        protected void OnDestroy()
        {
            contactedColliderIdSet.Clear();
            contactedColliderIdSet = null;
        }

        // ��������
        protected virtual void OnEnterArea(GameObject other)
        {
            // �ڲ�ͬ��Ӫ��ʩ���˺�/DeBuff
            if (InDiffGroup(other) && CheckAllowDiffGroupEffect(other))
            {
                OnDiffGroupEnter(other);
            }
            // ����ͬ��Ӫ��ʩ������/Buff
            else if (InSameGroup(other) && CheckAllowSameGroupEffect(other))
            {
                OnSameGroupEnter(other);
            }
        }
        #endregion

        #region Event
        // �Ż�������ͨ����д��������������ȥ�����ֲ���Ҫ���߼�
        protected virtual void OnDiffGroupEnter(GameObject other)
        {
            Debug.LogWarning("[Warning] �����˻��� SkillBase �ķ����汾");
            // ʩ���˺�
            if (this is IDamageSkill damageSkill)
            {
                SkillBehaviour.DealDamage(other, damageSkill.GetDamage());
            }
            // �����ʩ�� DeBuff �߼�����ֹ���������Ч��
        }
        protected virtual void OnSameGroupEnter(GameObject other)
        {
            Debug.LogWarning("[Warning] �����˻��� SkillBase �ķ����汾");
            // ʩ������
            if (this is IHealSkill healSkill)
            {
                SkillBehaviour.DealHeal(other, healSkill.GetHeal());
            }
            // �����ʩ�� Buff �߼�����ֹ���������Ч��
        }

        // ����ͨ����д��������������ʵ���ض��İ��������ʹ����߼�
        protected virtual bool CheckAllowDiffGroupEffect(GameObject other)
        {
            if( 
                other.CompareTag(GlobalTags.TAG_ENTITY) || 
                other.CompareTag(GlobalTags.TAG_INTACT_SKILL) ||
                other.CompareTag(GlobalTags.TAG_DESTABL_STRUCT)
                )
            {
                return true;
            }
            return false;
        }
        protected virtual bool CheckAllowSameGroupEffect(GameObject other)
        {
            if (
                other.CompareTag(GlobalTags.TAG_ENTITY) ||
                other.CompareTag(GlobalTags.TAG_INTACT_SKILL) ||
                other.CompareTag(GlobalTags.TAG_DESTABL_STRUCT)
                )
            {
                return true;
            }
            return false;
        }
        #endregion


        #region Group
        // �ж�λ�ڲ�ͬ��Ӫ
        protected bool InDiffGroup(GameObject other)
        {
            return (
                groupManager.GetInstanceGroup(gameObject.GetInstanceID()) !=
                groupManager.GetInstanceGroup(other.GetInstanceID())
                );
        }
        // �ж�λ����ͬ��Ӫ
        protected bool InSameGroup(GameObject other)
        {
            return (
                groupManager.GetInstanceGroup(gameObject.GetInstanceID()) ==
                groupManager.GetInstanceGroup(other.GetInstanceID())
                );
        }
        #endregion

    }

}





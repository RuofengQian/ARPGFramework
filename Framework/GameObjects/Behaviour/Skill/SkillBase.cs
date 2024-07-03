using MyFramework.GameObjects.Attribute;
using MyFramework.GameObjects.Group;
using MyFramework.GameObjects.Lifecycle;
using MyFramework.Manager.GameTick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour
{
    // SkillBase：观察 Entity 的属性，实现通用方法
    public abstract class SkillBase : MonoBehaviour
    {
        #region Attribute
        // 游戏时间管理器
        protected static GameTickManager gameTickManager = GameTickManager.Instance;
        // 阵营管理器
        protected static GroupManager GroupManager = GroupManager.Instance;


        // 观察的 Entity 属性
        private EntityAttribute _entityAttr;
        protected EntityAttribute entityAttr { get => _entityAttr; private set => _entityAttr = value; }

        // 技能触发方式
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
        // （缓存优化）攻击次数生命周期
        private LifeAttackedTime _lifeAttackTime;
        protected LifeAttackedTime lifeAttackTime { get => _lifeAttackTime; private set => _lifeAttackTime = value; }


        private void Start()
        {
            // 如果没有被赋值，说明这是一个无效的技能，删除并报告错误
            if (entityAttr == null)
            {
                Debug.LogError("[Error] 技能没有被赋予观察的 EntityAttribute 实例");
                Destroy(gameObject);
                return;
            }
            // 试图寻找攻击次数组件
            gameObject.TryGetComponent<LifeAttackedTime>(out _lifeAttackTime);
        }
        #endregion


        #region Action
        // 注意：这里可能成为性能瓶颈
        // OnTriggerEnter：触发技能交互行为，可根据游戏类型来定义
        private void OnTriggerEnter(Collider other)
        {
            // 只允许触发1次
            if( contactedInstanceID.Contains(other.GetInstanceID()) )
            {
                return;
            }
            contactedInstanceID.Add(other.GetInstanceID());

            // 在不同阵营：施加伤害/DeBuff
            if ( InDiffGroup(other.gameObject) )
            {
                InteractDiffGroup(other.gameObject);
            }
            // 在相同阵营：施加治疗/Buff
            else if( InSameGroup(other.gameObject) )
            {
                InteractSameGroup(other.gameObject);
            }
        }

        // 优化：可以通过重写以下两个方法，去除部分不需要的逻辑
        protected virtual void InteractDiffGroup(GameObject other)
        {
            // 施加 DeBuff
            if (this is IDeBuffSkill deBuffSkill && SkillBehaviour.AttachBuff(other, deBuffSkill.GetBuff()))
            {
                // TODO
            }
            // 施加伤害
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
            // 施加 Buff
            if (this is IBuffSkill buffSkill && SkillBehaviour.AttachBuff(other, buffSkill.GetBuff()))
            {
                flag = true;
            }
            // 施加治疗
            if (this is IHealSkill healSkill && SkillBehaviour.DealHeal(other, healSkill.GetHeal()))
            {
                flag = true;
            }
            //return flag;
        }
        #endregion

        #region Group
        // 判断位于不同阵营
        protected bool InDiffGroup(GameObject other)
        {
            return (
                GroupManager.GetInstanceGroup(gameObject.GetInstanceID()) !=
                GroupManager.GetInstanceGroup(other.GetInstanceID())
                );
        }
        // 判断位于相同阵营
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





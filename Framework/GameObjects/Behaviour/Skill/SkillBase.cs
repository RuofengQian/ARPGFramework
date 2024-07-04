using MyFramework.GameObjects.Attribute;
using MyFramework.GameObjects.Group;
using MyFramework.Manager.GameTick;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // SkillBase：观察 Entity 的属性，实现通用方法
    public abstract class SkillBase : MonoBehaviour
    {
        #region Attribute
        // 游戏时间管理器
        protected static GameTickManager gameTickManager = GameTickManager.Instance;
        // 阵营管理器
        protected static GroupManager groupManager = GroupManager.Instance;


        // 观察的 Entity 属性
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
            // 如果没有被赋值，说明这是一个无效的技能，删除并报告错误
            if (entityAttr == null)
            {
                Debug.LogError("[Error] 技能没有被赋予观察的 EntityAttribute 实例");
                Destroy(gameObject);
                return;
            }
        }
        #endregion


        #region Action
        // 接触碰撞箱 ID 集合
        protected HashSet<int> contactedColliderIdSet = new();

        // OnTriggerEnter：触发交互逻辑
        protected void OnTriggerEnter(Collider other)
        {
            if( contactedColliderIdSet.Contains(other.GetInstanceID()) )
            {
                return;
            }
            contactedColliderIdSet.Add(other.GetInstanceID());

            OnEnterArea(other.gameObject);
        }
        // OnDisable：暂存逻辑
        protected void OnDisable()
        {
            // 清除碰撞记录
            contactedColliderIdSet.Clear();
        }
        // OnDestroy：销毁逻辑
        protected void OnDestroy()
        {
            contactedColliderIdSet.Clear();
            contactedColliderIdSet = null;
        }

        // 进入区域
        protected virtual void OnEnterArea(GameObject other)
        {
            // 在不同阵营：施加伤害/DeBuff
            if (InDiffGroup(other))
            {
                OnDiffGroupEnter(other);
            }
            // 在相同阵营：施加治疗/Buff
            else if (InSameGroup(other))
            {
                OnSameGroupEnter(other);
            }
        }
        #endregion

        #region Event
        // 优化：可以通过重写以下两个方法，去除部分不需要的逻辑
        protected virtual void OnDiffGroupEnter(GameObject other)
        {
            Debug.LogWarning("[Warning] 调用了基类 SkillBase 的方法版本");
            // 施加伤害
            if (this is IDamageSkill damageSkill)
            {
                SkillBehaviour.DealDamage(other, damageSkill.GetDamage());
            }
            // 不添加施加 DeBuff 逻辑，防止造成永久性效果
        }
        protected virtual void OnSameGroupEnter(GameObject other)
        {
            Debug.LogWarning("[Warning] 调用了基类 SkillBase 的方法版本");
            // 施加治疗
            if (this is IHealSkill healSkill)
            {
                SkillBehaviour.DealHeal(other, healSkill.GetHeal());
            }
            // 不添加施加 Buff 逻辑，防止造成永久性效果
        }
        #endregion


        #region Group
        // 判断位于不同阵营
        protected bool InDiffGroup(GameObject other)
        {
            return (
                groupManager.GetInstanceGroup(gameObject.GetInstanceID()) !=
                groupManager.GetInstanceGroup(other.GetInstanceID())
                );
        }
        // 判断位于相同阵营
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





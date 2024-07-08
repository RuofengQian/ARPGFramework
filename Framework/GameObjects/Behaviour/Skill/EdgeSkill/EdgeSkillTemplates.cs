using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using MyProject.GameObjects.Tags;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) ��Χ���������Buff����
    // ʾ����������Q���ܡ�������������ѷ�ʩ��+������Buff���˳����ѷ��Ƴ���Buff
    public abstract class EdgeBuffSkill : EdgeSkillBase, IBuffSkill
    {
        protected override bool CheckAllowSameGroupEffect(GameObject other)
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
        protected override bool CheckAllowDiffGroupEffect(GameObject other)
        {
            return false;
        }

        protected override void OnSameGroupEnter(GameObject other)
        {
            SkillBehaviour.AttachBuff(other, this.GetBuff());
        }
        protected override void OnSameGroupExit(GameObject other)
        {
            SkillBehaviour.RemoveBuff(other, this.GetBuff());
        }
        protected override void OnDiffGroupEnter(GameObject other)
        {
            return;
        }
        protected override void OnDiffGroupExit(GameObject other)
        {
            return;
        }

        public abstract BuffInfo GetBuff();

    }

    // 2) ���⼼��
    // ʾ����ҹ��E����
    // ��⣺OnDiffGroupEnter����������ǡ����з���λ����������ϣ�OnDiffGroupExit���������Լ����еĵз���������˺�
    public abstract class EdgeDamageSkill : EdgeSkillBase, IDamageSkill
    {
        // ���⼼��û�й̶���ģ��ʵ�֣���Ҫȫ����д
        public abstract Damage GetDamage();

    }

}




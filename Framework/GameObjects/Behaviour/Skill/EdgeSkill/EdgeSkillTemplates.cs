using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;
using UnityEngine;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1) ��Χ���������Buff����
    // ʾ����������Q���ܡ�������������ѷ�ʩ��+������Buff���˳����ѷ��Ƴ���Buff
    public abstract class EdgeBuffSkill : EdgeSkillBase, IBuffSkill
    {
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




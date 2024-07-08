using MyFramework.GameObjects.Buff;
using MyFramework.GameObjects.Lifecycle;


namespace MyFramework.GameObjects.Behaviour.Skill
{
    // 1.������ Entity ������صļ��ܽӿ�
    // �˺�����
    public interface IDamageSkill
    {
        // ��ȡ�˺�ֵ
        Damage GetDamage();

    }
    // ���Ƽ���
    public interface IHealSkill
    {
        // ��ȡ������
        Heal GetHeal();

    }

    // 2.������ Entity �����޹صļ��ܽӿ�
    // ����Buff���ܣ�Ϊ����ʩ�� Buff��������ʱ��ֵЧ��
    public interface IBuffSkill
    {
        BuffInfo GetBuff();

    }

    public interface IDeBuffSkill
    {
        BuffInfo GetDeBuff();

    }

}




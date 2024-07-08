

namespace MyFramework.GameObjects.Lifecycle
{
    // �������ڰ�������ʽ��Ϊ���飺���ˡ��ж�������ʱ��
    // �����Ҫ��ͬ�����ʵ������߼����̳нӿڲ���������������Ӧ�߼�����
    
    
    // 1) �����˼�������������
    // �������飺TakeDamage & TakeHeal
    public interface IDamageBasedLifecycle
    {
        // �����˺�
        bool allowDamage { get; }
        // ��������
        bool allowHeal { get; }

        // �ܵ��˺�
        bool TakeDamage(Damage damage);
        // �ܵ�����
        bool TakeHeal(Heal heal);

    }

    // 2) �������ж�������������������
    // �������飺
    public interface IActionBasedLifecycle
    {
        // ��ʼ�ж�����
        int initActionTimes { get; }
        // ʣ���ж�����
        int leftActionTimes { get; }
        
        // �۳��ж�����
        bool CostActionTime(int costTimes);
        // �����ж�����
        bool AddActionTime(int addTimes);

    }

    // 3) ��ʱ���������������
    public interface ITimeBasedLifecycle
    {
        // ��ʼʱ��
        float initTime { get; }
        // ʣ��ʱ��
        float leftTime { get; }
        
        // �۳�ʣ��ʱ��
        bool CostLeftTime(float declineTime);
        // �ӳ�ʣ��ʱ��
        bool AddLeftTime(float extendTime);

    }

}


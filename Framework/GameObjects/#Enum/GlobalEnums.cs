

namespace MyFramework.GameObjects
{
    // ˵����
    // ��Ϸ�е������߼������Ա�����Ϊ���ࣺ��ֵ�仯���¼�������E-C-S��Ʒ�ʽ�е� ���Component/���� �� ϵͳSystem/�¼���
    // ��ˣ�����ߵ�������ģʽ����Ϊȫ����ͨ��
    

    // ��ֵ�仯ģʽ
    public enum ValueEffectMode
    {
        // ����ֵ�仯
        Absolute = 0,
        // ����ֵ/���ֵ�ٷֱȱ仯
        Percentage = 1,
        // ��ǰֵ�ٷֱȱ仯
        CurrValuePercentage = 2,

    }
    
    // �¼�����ģʽ
    public enum EventTriggerMode
    {
        // һ���Դ�������������
        Once = 0,
        // ���ش���������/�½���
        Edge = 1,

        // ��Ъ�Դ���������ʱ���Ъ��
        Intermit = 2,

        // ֡���´������� Update ��ִ���߼�
        FrameUpdate = 3,
        // ������´������� FixedUpdate ��ִ���߼�
        PhysicUpdate = 4,

    }

}





namespace MyFramework.GameObjects.Attribute
{
    // 1.��ֵ��
    // ������ͨ����ֵ��չ������{�̶�ֵ, �ɱ�ֵ, ����}
    // ����������ֵ������������������
    // ���������б�ԭʼֵ + ����ֵ + ���Ӱٷֱ�
    // ˵����ΪʲôҪ������ƣ�������һ���ֶΣ�rawValue��ֱ������ֵ�������Ƴ�������Ŀ
    public interface INumericAttr
    {
        float rawValue { get; }

        float extraValue { get; }
        float extraPer { get; }

        float per { get; }


        void AddExtraValue(float val);
        void RemoveExtraValue(float val);

        void AddExtraPer(float val);
        void RemoveExtraPer(float val);

        void SetRawValue(float val);

    }

    // 2.������
    // ������ӵ�в�ͬ����ֵ���Ա���ʵ�ֲ�ͬ��Ϊ
    // �����������ٶȡ��ƶ��ٶ�
    // ���������б�����ֵ + ���Ӱٷֱ�
    public interface IMagnAttr
    {
        float basicValue { get; }

        float extraPer { get; }

        float per { get; }


        void AddExtraPer(float val);
        void RemoveExtraPer(float val);

        void SetBasicValueByMul(float mul);

    }

    // 3.������
    // ���������ʴ������ɹ�������������Ա��ʵ�ֵ
    // ����������������ֵ
    // ���������б����� + ���Ӱٷֱ�
    public interface IProbAttr
    {
        float chance { get; }

        float basicValuePer { get; }
        float extraValuePer { get; }

        float valuePer { get; }

        // �Ƿ���Ч
        bool valid { get; }


        void AddChance(float val);
        void RemoveChance(float val);

        void AddExtraPer(float val);
        void RemoveExtraPer(float val);

        void SetBasicPer(float val);

    }

    // 4.��һ��
    // ��������һ�����Ե�ֱ�Ӳ���
    // ����������ֵ������Ϸ������صĶ������ԣ�������õȣ�
    public interface ISingleAttr
    {
        float value { get; }

        bool valid { get; }


        void AddValue(float val);
        void RemoveValue(float val);
        void ClearValue();

    }

}



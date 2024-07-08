

namespace MyFramework.GameObjects.Backpack.Item
{
    // ��Ʒ�ӿڶ���
    // ���򣺹�ע��ͬ����Ϊ�߼�������רע�ھ������Ʒ���ࣻ��Ʒ������ר�ŵ������ֶ�������
    // ���ӿ�-�����ֶ� ѡ�񷽷���


    // 1.װ��ϵͳ
    // ��װ����Ʒ�����Ρ���Ʒ
    public interface IEqupiableItem
    {
        // װ��
        void OnEquip();
        // ж��
        void OnUnEquip();

    }

    // 2.����ϵͳ���ֳ���Ʒ��������������
    public interface IHandheldItem
    {
        // �ֳ��߼�
        void OnHandle();
        // �����߼�
        void OnPutBack();

    }

    // 3.�ϳ�ϵͳ
    // �ɺϳ���Ʒ
    public interface ICraftableItem
    {
        // �ϳ��߼�
        void OnCraft();

    }

    // 4.�ɷ�����Ʒ
    // �ɷ�����Ʒ
    public interface IPlaceableItem
    {
        // �����߼�
        void OnPlace();

    }

}



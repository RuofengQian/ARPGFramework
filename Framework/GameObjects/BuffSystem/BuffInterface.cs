

namespace MyFramework.GameObjects.Buff
{
    // �ɵ��� Buff
    public interface IStackableBuff
    {
        int currLevel { get; }

        void IncLevel(BuffInfo buff);
        void DecLevel();

    }

    // 


}



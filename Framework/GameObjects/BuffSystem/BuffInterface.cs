

namespace MyFramework.GameObjects.Buff
{
    // ¿Éµþ¼Ó Buff
    public interface IStackableBuff
    {
        int currLevel { get; }

        void IncLevel(BuffInfo buff);
        void DecLevel();

    }

    // 


}



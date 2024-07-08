using MyFramework.GameObjects.Attribute;


namespace MyFramework.GameObjects.Buff
{
    // Buff ����
    public abstract class BuffBase
    {
        #region Init
        // �۲�� Entity ����
        private EntityAttribute entityAttr;

        public void BindEntityAttr(EntityAttribute entityAttr)
        {
            this.entityAttr = entityAttr;
        }
        #endregion


        // ʩ��Ч������
        public abstract void OnEffectEnable();
        // ȡ��Ч������
        public abstract void OnEffectDisable();

    }

}




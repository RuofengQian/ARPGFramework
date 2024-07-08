using MyFramework.GameObjects.Attribute;


namespace MyFramework.GameObjects.Buff
{
    // Buff 基类
    public abstract class BuffBase
    {
        #region Init
        // 观察的 Entity 属性
        private EntityAttribute entityAttr;

        public void BindEntityAttr(EntityAttribute entityAttr)
        {
            this.entityAttr = entityAttr;
        }
        #endregion


        // 施用效果方法
        public abstract void OnEffectEnable();
        // 取消效果方法
        public abstract void OnEffectDisable();

    }

}




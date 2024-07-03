using MyFramework.GameObjects.Attribute;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Buff
{
    public enum BuffType
    {
        // 临时的
        Temporary = 0,
        // 恒定的
        Constant = 1,
    }

    // 效果基类
    public abstract class BuffBase
    {
        // 种类
        private BuffType _type;

        public BuffType type
        {
            get => _type;
            protected set => _type = value;
        }



        private float duration = 1f;


        #region Construct
        public BuffBase()
        {

        }
        #endregion


        // 观察的 Entity 属性
        private EntityAttribute entityAttr;

        public void BindEntityAttr()
        {

        }


        // 施用效果方法
        public abstract void OnEnable();
        // 取消效果方法
        public abstract void OnDisable();

    }

}




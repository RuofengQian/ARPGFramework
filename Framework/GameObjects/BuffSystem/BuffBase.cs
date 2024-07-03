using MyFramework.GameObjects.Attribute;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace MyFramework.GameObjects.Buff
{
    public enum BuffType
    {
        // ��ʱ��
        Temporary = 0,
        // �㶨��
        Constant = 1,
    }

    // Ч������
    public abstract class BuffBase
    {
        // ����
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


        // �۲�� Entity ����
        private EntityAttribute entityAttr;

        public void BindEntityAttr()
        {

        }


        // ʩ��Ч������
        public abstract void OnEnable();
        // ȡ��Ч������
        public abstract void OnDisable();

    }

}




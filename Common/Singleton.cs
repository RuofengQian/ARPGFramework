using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common
{
    public class Singleton<T>
        where T : class
    {
        public static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance(typeof(T), true) as T;
                }
                return _instance;
            }
            private set { _instance = value; }
        }

        protected Singleton() { }

    }

}







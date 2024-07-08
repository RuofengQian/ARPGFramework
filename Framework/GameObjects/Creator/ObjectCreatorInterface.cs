using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyFramework.GameObjects.Creator
{
    public interface IObjectCreator
    {
        

        GameObject CreateObject(string desc);


    }

}



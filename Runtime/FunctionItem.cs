using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.EaseFunctions
{
    [System.Serializable]
    public class FunctionItem
    {
        public EasingType easing;
        public FunctionType function;

        public Func<float, float> method;

        public FunctionItem(EasingType easing, FunctionType function, Func<float, float> method)
        {
            this.easing = easing;
            this.function = function;
            this.method = method;
        }
    }
}
using HexTecGames.EaseFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.EasingFunctions.Example.UI
{
    public abstract class EaseObject : MonoBehaviour
    {
        [Space]
        [SerializeField] protected Easing easing = default;
        [SerializeField] protected Function function = default;
        [Space]
        [SerializeField] private Color redColor = default;
        [SerializeField] private Color greenColor = default;
        [SerializeField] private Color blueColor = default;

        protected Func<float, float> easeFunction;

        public static float speed = 0.5f;

        protected virtual void Awake()
        {
            easeFunction = EaseFunction.GetFunction(easing, function);
            
        }
        protected virtual void OnEnable()
        {
            StartCoroutine(Animate());
        }

        protected abstract IEnumerator Animate();
        protected string ToSentence(string input)
        {
            return new string(input.SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new[] { ' ', c } : new[] { c }).ToArray());
        }
        protected Color GetColor()
        {
            switch (easing)
            {
                case Easing.EaseIn:
                    return redColor;
                case Easing.EaseOut:
                    return greenColor;
                case Easing.EaseInOut:
                    return blueColor;
                default:
                    return Color.white;
            }
        }
    }
}
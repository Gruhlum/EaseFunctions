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
        [SerializeField] protected EaseFunction easeFunction = default;
        [Space]
        [SerializeField] private Color redColor = default;
        [SerializeField] private Color greenColor = default;
        [SerializeField] private Color blueColor = default;

        public static float speed = 0.5f;

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
            switch (easeFunction.easingType)
            {
                case EasingType.EaseIn:
                    return redColor;
                case EasingType.EaseOut:
                    return greenColor;
                case EasingType.EaseInOut:
                    return blueColor;
                default:
                    return Color.white;
            }
        }
    }
}
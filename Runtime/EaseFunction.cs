using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace HexTecGames.EaseFunctions
{
    [System.Serializable]
    public class EaseFunction
    {
        public EasingType easingType;
        public FunctionType functionType;

        private const float c1 = 1.70158f;
        private const float c2 = c1 * 1.525f;
        private const float c3 = c1 + 1f;
        private const float c4 = 2f * MathF.PI / 3f;
        private const float c5 = 2f * MathF.PI / 4.5f;

        const float amplitude = 6f;

        private const float n1 = 7.5625f;
        private const float d1 = 2.75f;

        private static readonly Dictionary<EasingType, Dictionary<FunctionType, Func<float, float>>> methodDict
            = new Dictionary<EasingType, Dictionary<FunctionType, Func<float, float>>>()
        {
            {
                EasingType.EaseIn, new Dictionary<FunctionType, Func<float, float>>()
                {
                    { FunctionType.Sine, EaseInSine },
                    { FunctionType.Quad, EaseInQuad },
                    { FunctionType.Cubic, EaseInCubic },
                    { FunctionType.Quart, EaseInQuart },
                    { FunctionType.Quint, EaseInQuint },
                    { FunctionType.Expo, EaseInExpo },
                    { FunctionType.Circ, EaseInCirc },
                    { FunctionType.Back, EaseInBack },
                    { FunctionType.Elastic, EaseInElastic },
                    { FunctionType.Bounce, EaseInBounce },
                    { FunctionType.Overshoot, EaseInOvershoot },
                    { FunctionType.Linear, Linear },
                }
            },
                {
                EasingType.EaseOut, new Dictionary<FunctionType, Func<float, float>>()
                {
                    { FunctionType.Sine, EaseOutSine },
                    { FunctionType.Quad, EaseOutQuad },
                    { FunctionType.Cubic, EaseOutCubic },
                    { FunctionType.Quart, EaseOutQuart },
                    { FunctionType.Quint, EaseOutQuint },
                    { FunctionType.Expo, EaseOutExpo },
                    { FunctionType.Circ, EaseOutCirc },
                    { FunctionType.Back, EaseOutBack },
                    { FunctionType.Elastic, EaseOutElastic },
                    { FunctionType.Bounce, EaseOutBounce },
                    { FunctionType.Overshoot, EaseOutOvershoot },
                    { FunctionType.Linear, Linear },
                }
            },
                {
                EasingType.EaseInOut, new Dictionary<FunctionType, Func<float, float>>()
                {
                    { FunctionType.Sine, EaseInOutSine },
                    { FunctionType.Quad, EaseInOutQuad },
                    { FunctionType.Cubic, EaseInOutCubic },
                    { FunctionType.Quart, EaseInOutQuart },
                    { FunctionType.Quint, EaseInOutQuint },
                    { FunctionType.Expo, EaseInOutExpo },
                    { FunctionType.Circ, EaseInOutCirc },
                    { FunctionType.Back, EaseInOutBack },
                    { FunctionType.Elastic, EaseInOutElastic },
                    { FunctionType.Bounce, EaseInOutBounce },
                    { FunctionType.Overshoot, EaseInOutOvershoot },
                    { FunctionType.Linear, Linear },
                }
            }
        };

        private Func<float, float> function;

        public EaseFunction() { }

        public EaseFunction(EasingType easingType, FunctionType functionType)
        {
            this.easingType = easingType;
            this.functionType = functionType;
        }

        private void SetFunction()
        {
            function = GetFunction(easingType, functionType);
        }
        public Func<float, float> GetFunction()
        {
            return GetFunction(easingType, functionType);
        }
        public float GetValue(float percent)
        {
            if (Application.isEditor)
            {
                return GetFunction()(percent);
            }
            else if (function == null)
            {
                SetFunction();
            }
            return function(percent);
        }

        public static EasingType GetReverseEasing(EasingType easing)
        {
            if (easing == EasingType.EaseIn)
            {
                return EasingType.EaseOut;
            }
            if (easing == EasingType.EaseOut)
            {
                return EasingType.EaseIn;
            }
            else return easing;
        }

        public static Func<float, float> GetFunction(EasingType easingType, FunctionType functionType)
        {
            if (methodDict.TryGetValue(easingType, out Dictionary<FunctionType, Func<float, float>> subDict))
            {
                if (subDict.TryGetValue(functionType, out Func<float, float> method))
                {
                    return method;
                }
            }
            Debug.Log($"Could not find method for {easingType} {functionType}");
            return null;
        }
        public static float GetValue(EasingType easingType, FunctionType functionType, float percent)
        {
            return GetFunction(easingType, functionType).Invoke(percent);
        }


        public static float EaseInOvershoot(float x)
        {
            // Time-reverse EaseOut and mirror vertically so it overshoots into the negative.
            float y = EaseOutOvershoot(1f - x);

            // y now goes from 1 → 0 with overshoot above 1.
            // We want 0 → 1 with overshoot below 0, so mirror around 0.5:
            return 1f - y;
        }
        public static float EaseOutOvershoot(float x)
        {
            float t = x;
            float oscillation =
                amplitude *
                MathF.Sin(t) *
                MathF.Exp(-t) *
                t * (1f - t);

            return x + oscillation;
        }
        public static float EaseInOutOvershoot(float x)
        {
            const float frequency = 3f;    // how many swings
                                           // Centered time: -1..+1
            float t = (x * 2f) - 1f;

            // Convert to 0..1 for envelope
            float u = (t + 1f) * 0.5f;

            // Oscillation that is guaranteed to be 0 at x=0 and x=1
            float oscillation =
                (amplitude / 2f) *
                MathF.Sin(t * frequency) *
                MathF.Exp(-MathF.Abs(t)) *
                u * (1f - u);   // <-- THIS fixes the endpoint issue
            return x + oscillation;
        }
        public static float EaseInSine(float x)
        {
            return 1f - MathF.Cos(x * MathF.PI / 2f);
        }
        public static float EaseOutSine(float x)
        {
            return MathF.Sin(x * MathF.PI / 2f);
        }
        public static float EaseInOutSine(float x)
        {
            return -(MathF.Cos(MathF.PI * x) - 1f) / 2f;
        }
        public static float Linear(float x)
        {
            return x;
        }
        public static float EaseInQuad(float x)
        {
            return x * x;
        }
        public static float EaseOutQuad(float x)
        {
            return 1f - ((1f - x) * (1f - x));
        }
        public static float EaseInOutQuad(float x)
        {
            return x < 0.5f ? 2f * x * x : 1f - (MathF.Pow((-2f * x) + 2f, 2f) / 2f);
        }
        public static float EaseInCubic(float x)
        {
            return x * x * x;
        }
        public static float EaseOutCubic(float x)
        {
            return 1f - MathF.Pow(1f - x, 3f);
        }
        public static float EaseInOutCubic(float x)
        {
            return x < 0.5f ? 4f * x * x * x : 1f - (MathF.Pow((-2f * x) + 2f, 3f) / 2f);
        }

        public static float EaseInQuart(float x)
        {
            return x * x * x * x;
        }
        public static float EaseOutQuart(float x)
        {
            return 1f - MathF.Pow(1f - x, 4f);
        }
        public static float EaseInOutQuart(float x)
        {
            return x < 0.5f ? 8f * x * x * x * x : 1f - (MathF.Pow((-2f * x) + 2f, 4f) / 2f);
        }

        public static float EaseInQuint(float x)
        {
            return x * x * x * x * x;
        }
        public static float EaseOutQuint(float x)
        {
            return 1 - MathF.Pow(1f - x, 5f);
        }
        public static float EaseInOutQuint(float x)
        {
            return x < 0.5f ? 16f * x * x * x * x * x : 1f - (MathF.Pow((-2f * x) + 2f, 5f) / 2f);
        }

        public static float EaseInExpo(float x)
        {
            return x == 0f ? 0f : MathF.Pow(2f, (10f * x) - 10f);
        }
        public static float EaseOutExpo(float x)
        {
            return x == 1f ? 1f : 1f - MathF.Pow(2f, -10f * x);
        }
        public static float EaseInOutExpo(float x)
        {
            return x == 0f
            ? 0f
            : x == 1f
            ? 1f
            : x < 0.5f ? MathF.Pow(2f, (20f * x) - 10f) / 2f
            : (2f - MathF.Pow(2f, (-20f * x) + 10f)) / 2f;
        }

        public static float EaseInCirc(float x)
        {
            return 1f - MathF.Sqrt(1 - MathF.Pow(x, 2f));
        }
        public static float EaseOutCirc(float x)
        {
            return MathF.Sqrt(1f - MathF.Pow(x - 1f, 2f));
        }
        public static float EaseInOutCirc(float x)
        {
            return x < 0.5f
            ? (1f - MathF.Sqrt(1f - MathF.Pow(2f * x, 2f))) / 2f
            : (MathF.Sqrt(1f - MathF.Pow((-2f * x) + 2f, 2f)) + 1f) / 2f;
        }

        public static float EaseInBack(float x)
        {
            return (c3 * x * x * x) - (c1 * x * x);
        }
        public static float EaseOutBack(float x)
        {
            return 1f + (c3 * MathF.Pow(x - 1f, 3f)) + (c1 * MathF.Pow(x - 1f, 2f));
        }
        public static float EaseInOutBack(float x)
        {
            return x < 0.5f
            ? MathF.Pow(2f * x, 2f) * (((c2 + 1f) * 2f * x) - c2) / 2f
            : ((MathF.Pow((2f * x) - 2f, 2f) * (((c2 + 1f) * ((x * 2f) - 2f)) + c2)) + 2f) / 2f;
        }

        public static float EaseInElastic(float x)
        {
            return x == 0f
            ? 0f
            : x == 1f
            ? 1f
            : -MathF.Pow(2f, (10f * x) - 10f) * MathF.Sin(((x * 10f) - 10.75f) * c4);
        }
        public static float EaseOutElastic(float x)
        {
            return x == 0f
            ? 0f
            : x == 1f
            ? 1f
            : (MathF.Pow(2f, -10f * x) * MathF.Sin(((x * 10f) - 0.75f) * c4)) + 1f;
        }
        public static float EaseInOutElastic(float x)
        {
            return x == 0f
              ? 0f
              : x == 1f
              ? 1f
              : x < 0.5f
              ? -(MathF.Pow(2f, (20f * x) - 10f) * MathF.Sin(((20f * x) - 11.125f) * c5)) / 2f
              : (MathF.Pow(2f, (-20f * x) + 10f) * MathF.Sin(((20f * x) - 11.125f) * c5) / 2f) + 1f;
        }

        public static float EaseInBounce(float x)
        {
            return 1f - EaseOutBounce(1f - x);
        }
        public static float EaseOutBounce(float x)
        {
            if (x < 1f / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2f / d1)
            {
                return (n1 * (x -= 1.5f / d1) * x) + 0.75f;
            }
            else if (x < 2.5f / d1)
            {
                return (n1 * (x -= 2.25f / d1) * x) + 0.9375f;
            }
            else
            {
                return (n1 * (x -= 2.625f / d1) * x) + 0.984375f;
            }
        }
        public static float EaseInOutBounce(float x)
        {
            return x < 0.5f
            ? (1f - EaseOutBounce(1f - (2f * x))) / 2f
            : (1f + EaseOutBounce((2f * x) - 1f)) / 2f;
        }


        public override bool Equals(object obj)
        {
            if (obj is EaseFunction other)
            {
                return this.easingType == other.easingType &&
                       this.functionType == other.functionType;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(easingType, functionType);
        }

        public static bool operator ==(EaseFunction a, EaseFunction b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(EaseFunction a, EaseFunction b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{easingType} {functionType}";
        }
    }
}
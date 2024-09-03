using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.EasingFunctions.Example.UI
{
    public class EaseSlider : EaseObject
    {
        [SerializeField] private TMP_Text textGUI = default;
        [SerializeField] private Slider slider = default;
        [Space]
        [SerializeField] private Image sliderFill = default;
        [SerializeField] private Image sliderBackground = default;

        private void Reset()
        {
            textGUI = GetComponentInChildren<TMP_Text>();
            slider = GetComponentInChildren<Slider>();
        }
        private void OnValidate()
        {
            gameObject.name = $"{function} {easing} Slider";
            textGUI.text = $"{function} {ToSentence(easing.ToString())}";
            sliderFill.color = GetColor();
            sliderBackground.color = DarkenColor(sliderFill.color);
        }

        private Color DarkenColor(Color col)
        {
            col.r /= 3f;
            col.g /= 3f;
            col.b /= 3f;
            return col;
        }
        protected override IEnumerator Animate()
        {
            while (true)
            {
                yield return FillSlider(0, 1);
                yield return new WaitForSeconds(0.5f);
                yield return FillSlider(1, 0);
                yield return new WaitForSeconds(0.5f);
            }
        }
        private IEnumerator FillSlider(float start, float end)
        {
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime * speed;
                timer = Mathf.Min(1, timer);
                slider.value = Mathf.Lerp(start, end, easeFunction(timer));
                yield return null;
            }
        }
    }
}
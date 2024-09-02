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
                float timer = 0;
                while (timer < 1)
                {
                    timer += Time.deltaTime * speed;
                    timer = Mathf.Min(1, timer);
                    slider.value = easeFunction(timer);
                    yield return null;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
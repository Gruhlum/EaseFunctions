using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.EasingFunctions.Example.UI
{
    public class SpeedSlider : MonoBehaviour
    {
        [SerializeField] private Slider slider = default;
        [SerializeField] private float minValue = 0.1f;
        [SerializeField] private float maxValue = 10f;


        private void Start()
        {
            slider.SetValueWithoutNotify(Mathf.Lerp(0, 1, EaseObject.speed / maxValue));
        }

        public void OnValueChanged(float value)
        {
            EaseObject.speed = Mathf.Lerp(minValue, maxValue, value);
        }   
    }
}
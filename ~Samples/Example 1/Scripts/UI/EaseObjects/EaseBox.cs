using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.EasingFunctions.Example.UI
{
    public class EaseBox : EaseObject
    {
        [SerializeField] private TMP_Text textGUI = default;
        [SerializeField] private Image img = default;

        private void Reset()
        {
            textGUI = GetComponentInChildren<TMP_Text>();
            img = GetComponent<Image>();
        }
        private void OnValidate()
        {
            img.color = GetColor();
            gameObject.name = $"{function} {easing} Box";
            textGUI.text = $"{function} {ToSentence(easing.ToString())}";
        }

        protected override IEnumerator Animate()
        {
            yield return null;
            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + Vector3.right * 16;

            while (true)
            {
                yield return MoveObject(startPos, endPos);
                yield return new WaitForSeconds(0.5f);
                yield return MoveObject(endPos, startPos);
                yield return new WaitForSeconds(0.5f);
            }
        }
        private IEnumerator MoveObject(Vector3 start, Vector3 end)
        {
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime * speed;
                timer = Mathf.Min(1, timer);
                gameObject.transform.position = Vector3.Lerp(start, end, easeFunction(timer));
                yield return null;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HexTecGames.EasingFunctions.Example.UI
{
    public class EaseLine : EaseObject
    {
        [SerializeField] private LineRenderer lineRenderer = default;
        [SerializeField] private Image animationPoint = default;
        [SerializeField] private TMP_Text textGUI = default;
        [Space]
        [SerializeField, Min(2)] private int totalPoints = 24;
        [SerializeField] private float distanceMultiplierX = 20f;
        [SerializeField] private float distanceMultiplierY = 20f;
        [SerializeField] private Vector2 offset = default;


        private void OnValidate()
        {
            if (lineRenderer != null) lineRenderer.startColor = GetColor();
            if (lineRenderer != null) lineRenderer.endColor = GetColor();
            if (animationPoint != null) animationPoint.color = GetColor();
            if (gameObject != null) gameObject.name = $"{easeFunction.functionType} {easeFunction.easingType} Box";
            if (textGUI != null) textGUI.text = $"{easeFunction.functionType} {ToSentence(easeFunction.easingType.ToString())}";
        }

        protected override IEnumerator Animate()
        {
            lineRenderer.enabled = true;
            Vector3[] positions = new Vector3[totalPoints];
            // Debug.Log(Camera.main.ScreenToWorldPoint(transform.position));
            for (int i = 0; i < totalPoints; i++)
            {
                //positions[i] = GetPoint(i / (float)totalPoints) + Camera.main.ScreenToWorldPoint(transform.position);
                positions[i] = transform.position + (Vector3)GetPoint(i / (float)totalPoints) + (Vector3)offset;
            }

            lineRenderer.SetPositions(positions);

            int direction = 1;
            int currentIndex = 0;
            Vector3 startPosition = positions[currentIndex];
            Vector3 currentTarget = positions[currentIndex + 1];
            animationPoint.transform.position = currentTarget;

            float segmentProgress = 0;


            while (true)
            {

                while (segmentProgress < 1)
                {
                    animationPoint.transform.position = Vector3.Lerp(startPosition, currentTarget, segmentProgress);
                    yield return null;
                    segmentProgress += Time.deltaTime * speed * totalPoints;
                }
                segmentProgress = 0;
                currentIndex += direction;
                if (currentIndex >= totalPoints)
                {
                    yield return new WaitForSeconds(0.25f);
                    direction = -direction;
                    currentIndex = totalPoints - 1;
                }
                if (currentIndex < 0)
                {
                    yield return new WaitForSeconds(0.25f);
                    direction = -direction;
                    currentIndex = 0;
                }
                //Debug.Log(positions.Length + " - " + currentIndex);
                startPosition = currentTarget;
                currentTarget = positions[currentIndex];
            }
        }


        private Vector2 GetPoint(float progress)
        {
            return new Vector2(progress * distanceMultiplierX, easeFunction.GetValue(progress) * distanceMultiplierY);
        }
    }
}
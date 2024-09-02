using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HexTecGames.EasingFunctions.Example.UI
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text textGUI = default;

        [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

        private int currentIndex;


        private void Start()
        {
            ActivateCurrentObject();
        }

        public void OnClicked()
        {
            currentIndex = WrapIndex(currentIndex);
            ActivateCurrentObject();
        }

        private void ActivateCurrentObject()
        {            
            textGUI.text = gameObjects[WrapIndex(currentIndex)].name;

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].SetActive(currentIndex == i);
            }
        }

        private int WrapIndex(int index)
        {
            index++;
            if (index >= gameObjects.Count)
            {
                index = 0;
            }
            return index;
        }
    }
}
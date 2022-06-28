using System;
using Player;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class TapToStart : MonoBehaviour
    {
        public TextMeshProUGUI tapText;
        public PlayerMovement playerMovement;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    StartGame();
                }
            }

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
#endif
        }

        private void StartGame()
        {
            tapText.gameObject.SetActive(false);
            playerMovement.MoveToNext();
            this.enabled = false;
        }
    }
}
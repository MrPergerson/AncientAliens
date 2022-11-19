using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AncientAliens.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] GameObject PauseMenu;
        [SerializeField] GameObject WinScreen;
        [SerializeField] GameObject LoseScreen;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void OpenPauseMenu()
        {
            CloseMenus();
            PauseMenu.SetActive(true);
        }

        public void OpenWinScreen()
        {
            CloseMenus();
            WinScreen.SetActive(true);

        }

        public void OpenLoseScreen()
        {
            CloseMenus();
            LoseScreen.SetActive(true);
        }

        public void CloseMenus()
        {
            PauseMenu.SetActive(false);
            WinScreen.SetActive(false);
            LoseScreen.SetActive(false);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void EndGame()
        {
            RestartGame();
        }
    }
}

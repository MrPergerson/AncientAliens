using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace AncientAliens.UI
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] GameObject PauseMenu;
        [SerializeField] GameObject WinScreen;
        [SerializeField] GameObject LoseScreen;


        AudioSource audioSource;
        [SerializeField] bool playsSound;
        [SerializeField] AudioMixerGroup SFX_Mixer;
        [SerializeField] AudioClip SFX_OpenCloseMenu;


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

            if(TryGetComponent(out AudioSource audioSource))
            {
                this.audioSource = audioSource;

                if (SFX_Mixer) this.audioSource.outputAudioMixerGroup = SFX_Mixer;
                playsSound = true;
            }

            PauseMenu.SetActive(false);
            WinScreen.SetActive(false);
            LoseScreen.SetActive(false);
        }

        public void OpenPauseMenu()
        {

            WinScreen.SetActive(false);
            LoseScreen.SetActive(false);

            PauseMenu.SetActive(true);
            if(playsSound && SFX_OpenCloseMenu)
            {
                audioSource.PlayOneShot(SFX_OpenCloseMenu);
            }
        }

        public void OpenWinScreen()
        {
            PauseMenu.SetActive(false);
            LoseScreen.SetActive(false);

            WinScreen.SetActive(true);
            if (playsSound && SFX_OpenCloseMenu)
            {
                audioSource.PlayOneShot(SFX_OpenCloseMenu);
            }

        }

        public void OpenLoseScreen()
        {
            PauseMenu.SetActive(false);
            WinScreen.SetActive(false);
            LoseScreen.SetActive(true);

            if (playsSound && SFX_OpenCloseMenu)
            {
                audioSource.PlayOneShot(SFX_OpenCloseMenu);
            }
        }

        public void CloseMenus()
        {
            PauseMenu.SetActive(false);
            WinScreen.SetActive(false);
            LoseScreen.SetActive(false);

            if (playsSound && SFX_OpenCloseMenu)
            {
                audioSource.PlayOneShot(SFX_OpenCloseMenu);
            }
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

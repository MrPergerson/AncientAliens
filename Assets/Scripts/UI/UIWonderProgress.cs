using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AncientAliens
{
    public class UIWonderProgress : MonoBehaviour
    {
        TextMeshProUGUI textObject;

        private void Awake()
        {
            textObject = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            textObject.text = GameManager.Instance.WonderBuildProgress + "%";
        }
    }
}

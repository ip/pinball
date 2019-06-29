﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball
{
    public class GameOverDialog : MonoBehaviour
    {
        private GameObject _content;

        private void Awake()
        {
            _content = transform.GetChild(0).gameObject;

            EventManager.instance.OnGameOver += () => _SetVisibility(true);
            EventManager.instance.OnGameStart += () => _SetVisibility(false);
        }

        private void _SetVisibility(bool visible)
        {
            _content.SetActive(visible);
        }
    }
}
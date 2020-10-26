﻿using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UICreationSystem
{
    public class SettingSelectorItem : MonoBehaviour
    {
        public Toggle toggle;
        public TextMeshProUGUI title;

        private bool m_IsInitialized;
        
        public void Init(bool _Selected, string _Text, UnityAction<string> _Select, ToggleGroup _ToggleGroup)
        {
            title.text = _Text;
            name = $"{_Text} Setting";
            toggle.isOn = _Selected;
            
            toggle.onValueChanged.AddListener(_IsOn =>
            {
                if (_IsOn && m_IsInitialized)
                    _Select?.Invoke(_Text);
            });

            toggle.group = _ToggleGroup;
            m_IsInitialized = true;
        }
    }
}
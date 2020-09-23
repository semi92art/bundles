﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UICreationSystem
{
    [CreateAssetMenu(fileName = "new_style", menuName = "Style", order = 1)]
    public class UIStyleObject : ScriptableObject
    {
        #region public fields

        [Header("Image:")]
        public Sprite sprite;
        public Color imageColor = Color.white;
        public bool raycastImageTarget = true;
        public Image.Type imageType = Image.Type.Simple;
        public bool useSpriteMesh;
        public bool preserveAspect = true;
        public float pixelsPerUnityMultyply;
        public Image.FillMethod fillMethod;
        [Range(0, 3)]
        public int fillOrigin;
        public bool fillClockwise = true;

        [Header("TMP Text")] public GameObject text;
        [Header("TMP Button")] public GameObject button;
        [Header("TMP Input Field:")] public GameObject inputField;
        [Header("Image")] public GameObject image;
        [Header("Toggle")] public GameObject toggle;
        [Header("Slider")] public GameObject slider;
        [Header("Scrollbar")] public GameObject scrollbar;

        #endregion
    }
}



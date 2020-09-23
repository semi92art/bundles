﻿using Clickers;
using Clickers.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UICreationSystem.Factories
{
    public class UiFactory
    {
        #region public methods

        public static Canvas UiCanvas(
            string _Id,
            RenderMode _RenderMode,
            bool _PixelPerfect,
            int _SortOrder,
            AdditionalCanvasShaderChannels _AdditionalCanvasShaderChannels,
            CanvasScaler.ScaleMode _ScaleMode,
            Vector2Int _ReferenceResolution,
            CanvasScaler.ScreenMatchMode _ScreenMatchMode,
            float _Match,
            float _ReferencePixelsPerUnit,
            bool _IgnoreReversedGraphics,
            GraphicRaycaster.BlockingObjects _BlockingObjects
        )
        {
            GameObject go = new GameObject();
            go.name = _Id;
            Canvas canvas = go.AddComponent<Canvas>();
            canvas.renderMode = _RenderMode;
            canvas.pixelPerfect = _PixelPerfect;
            canvas.sortingOrder = _SortOrder;
            canvas.additionalShaderChannels = _AdditionalCanvasShaderChannels;

            CanvasScaler canvasScaler = go.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = _ScaleMode;
            canvasScaler.referenceResolution = _ReferenceResolution;
            canvasScaler.screenMatchMode = _ScreenMatchMode;
            canvasScaler.matchWidthOrHeight = _Match;
            canvasScaler.referencePixelsPerUnit = _ReferencePixelsPerUnit;

            GraphicRaycaster graphicRaycaster = go.AddComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = _IgnoreReversedGraphics;
            graphicRaycaster.blockingObjects = _BlockingObjects;

            return canvas;
        }

        public static Image UiImage(
            RectTransform _Item,
            string _StyleName)
        {
            Image image = _Item.gameObject.AddComponent<Image>();
            UIStyleObject style = ResLoader.GetStyle(_StyleName);
            if (style == null)
                return image;

            image.sprite = style.sprite;
            image.color = style.imageColor;
            image.raycastTarget = style.raycastImageTarget;
            image.useSpriteMesh = style.useSpriteMesh;
            image.preserveAspect = style.preserveAspect;
            image.pixelsPerUnitMultiplier = style.pixelsPerUnityMultyply;
            image.type = style.imageType;
            image.fillMethod = style.fillMethod;
            image.fillOrigin = style.fillOrigin;
            image.fillClockwise = style.fillClockwise;
            return image;
        }
        
        public static RectTransform UiRectTransform(
            RectTransform _Parent,
            string _Id,
            UIAnchor _Anchor,
            Vector2 _AnchoredPosition,
            Vector2 _Pivot,
            Vector2 _SizeDelta,
            RectTransform _Item = null
        )
        {
            var item = _Item == null ? 
                new GameObject(_Id).AddComponent<RectTransform>() : _Item;
            
            item.SetParent(_Parent);
            item.anchorMin = _Anchor.Min;
            item.anchorMax = _Anchor.Max;
            item.anchoredPosition = _AnchoredPosition;
            item.pivot = _Pivot;
            item.sizeDelta = _SizeDelta;
            item.localScale = Vector3.one;

#if UNITY_EDITOR
            item.gameObject.AddComponentIfNotExist<RectTranshormHelper>();
#endif
            return item;
        }



        #endregion

    }
}


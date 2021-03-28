﻿using System.Linq;
using Entities;
using Extensions;
using Games.RazorMaze.Models;
using Games.RazorMaze.Prot;
using Games.RazorMaze.Views;
using Games.RazorMaze.Views.MazeItems;
using UnityEditor;
using UnityEngine;
using Utils;
using Utils.Editor;

namespace Games.RazorMaze.Editor
{
    [CustomEditor(typeof(LevelDesigner))]
    public class LevelDesignerEditor : UnityEditor.Editor
    {
        private LevelDesigner m_Des;
        private int m_LevelGroup = 1;
        private int m_LevelIndex = 1;

        private void OnEnable()
        {
            m_Des = (LevelDesigner) target;
        }

        public override void OnInspectorGUI()
        {
            EditorUtilsEx.HorizontalZone(() =>
            {
                GUILayout.Label("Size");
                m_Des.sizeIdx = EditorGUILayout.Popup(m_Des.sizeIdx, 
                    LevelDesigner.Sizes.Select(_S => $"{LevelDesigner.MazeWidth}x{_S}").ToArray());
            });

            m_Des.aParam = EditorGUILayout.Slider(
                "Fullness", m_Des.aParam, 0, 1);

            base.OnInspectorGUI();
            
            EditorUtilsEx.GuiButtonAction("Create", CreateLevel);
            EditorUtilsEx.HorizontalZone(() =>
            {
                EditorUtilsEx.GuiButtonAction("Create Default", CreateDefault);
                EditorUtilsEx.GuiButtonAction("Check for validity", CheckLevelOnSceneForValidity);
            });
            
            EditorUtilsEx.GUIColorZone(m_Des.valid ? Color.green : Color.red, 
                () => GUILayout.Label($"Level is {(m_Des.valid ? "" : "not")} valid"));
            
            EditorUtilsEx.DrawUiLine(Color.gray);
            
            if (m_Des.group != 0 && m_Des.index >= 0)
                GUILayout.Label($"Current: Group: {m_Des.group}, Index: {m_Des.index}");
            
            EditorUtilsEx.HorizontalZone(() =>
            {
                EditorUtilsEx.GuiButtonAction(LoadLevel, m_LevelGroup, m_LevelIndex + 1);
                EditorUtilsEx.GuiButtonAction(SaveLevel, m_LevelGroup, m_LevelIndex + 1);
            });
            
            m_LevelGroup = EditorGUILayout.IntField("Group:", m_LevelGroup);
            m_LevelIndex = EditorGUILayout.Popup("Index:", m_LevelIndex, new[] {"1", "2", "3"});
        }

        private void CreateLevel()
        {
            EditorUtilsEx.ClearConsole();
            ClearLevel();
            var size = new V2Int(LevelDesigner.MazeWidth, LevelDesigner.Sizes[m_Des.sizeIdx]);
            var parms = new MazeGenerationParams(
                size,
                m_Des.aParam,
                m_Des.pathLengths.ToArray());
            var info = LevelGenerator.CreateRandomLevelInfo(parms, out m_Des.valid);
            CreateObjects(info);
            FocusCamera(info.Size);
            //m_Des.valid = LevelAnalizator.IsValid(info, false);
        }

        private void CreateDefault()
        {
            EditorUtilsEx.ClearConsole();
            ClearLevel();
            var size = new V2Int(LevelDesigner.MazeWidth, LevelDesigner.Sizes[m_Des.sizeIdx]);
            var info = LevelGenerator.CreateDefaultLevelInfo(size, true);
            CreateObjects(info);
            FocusCamera(info.Size);
            //m_Des.valid = LevelAnalizator.IsValid(info, false);
        }

        private void CreateObjects(MazeInfo _Info)
        {
            var container = CommonUtils.FindOrCreateGameObject("Maze", out _).transform;
            container.gameObject.DestroyChildrenSafe();
            m_Des.maze = RazorMazePrototypingUtils
                .CreateMazeItems(_Info, container)
                .Cast<ViewMazeItemProt>()
                .ToList();
            m_Des.group = _Info.LevelGroup;
            m_Des.index = _Info.LevelIndex;
        }

        private void FocusCamera(V2Int _Size)
        {
            var converter = new CoordinateConverter();
            converter.Init(_Size);
            var bounds = new Bounds(converter.GetCenter(), GameUtils.GetVisibleBounds().size * 0.7f);
            EditorUtilsEx.FocusSceneCamera(bounds);
        }

        private void ClearLevel()
        {
            var items = m_Des.maze;
            if (items == null)
                return;
            foreach (var item in items.Where(_Item => _Item != null))
                item.gameObject.DestroySafe();
            items.Clear();
        }

        private void CheckLevelOnSceneForValidity()
        {
            var info = m_Des.GetLevelInfoFromScene();
            m_Des.valid = LevelAnalizator.IsValid(info, false);
        }

        private void LoadLevel(int _Group, int _Index)
        {
            var info = MazeLevelUtils.LoadLevel(1, _Group, _Index);
            CreateObjects(info);
            FocusCamera(info.Size);
        }

        private void SaveLevel(int _Group, int _Index)
        {
            var info = m_Des.GetLevelInfoFromScene(false);
            info.LevelGroup = _Group;
            info.LevelIndex = _Index;
            m_Des.group = _Group;
            m_Des.index = _Index;
            MazeLevelUtils.SaveLevel(1, info);
        }
    }
}
/// <summary>
/// 
/// StylesFunctions.cs
/// 
/// Kerbal Space Program Calendar Plugin :: Copyright (C) 2014 nuclearping
/// 
/// Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com
/// This project or the author is in no way associated with nor endorsed by Squad.
/// 
/// This Source Code and Plugin are released under the Creative Commons Public Domain Mark 1.0 license.
/// See http://creativecommons.org/publicdomain/mark/1.0/ for further details.
/// 
/// See Calendar.cs for additional information.
/// 
/// </summary>

using System;
using UnityEngine;
using KSP.UI.Screens;
using KSPCalendar.Toolbar;

namespace KSPCalendar
{
    public partial class Calendar
    {
        private GUIStyle
            styleCalendarWindow, styleConfigWindow,
            styleBoxWhite, styleBoxYellow,
            styleToggle, styleMinimalisticLabel,
            styleDialogButton;

        private IButton tbButton;
        private IButton tbButtonCfg;

        ApplicationLauncherButton launcherButton = null;

#if true
        // ****
        void removeLauncherButtons()
        {
            if (launcherButton != null)
            {
                removeApplicationLauncher();
            }
            if (tbButton != null)
            {
                tbButton.Destroy();
                tbButton = null;
            }
        }

        internal void removeApplicationLauncher()
        {
            GameEvents.onGUIApplicationLauncherReady.Remove(OnGUIApplicationLauncherReady);
            ApplicationLauncher.Instance.RemoveModApplication(launcherButton);
            launcherButton = null;
        }

        void OnGUIApplicationLauncherReady()
        {
            if (launcherButton == null && (!HighLogic.CurrentGame.Parameters.CustomParams<KSPCalendarSettings>().useBlizzyToolbar || !ToolbarManager.ToolbarAvailable))
            {
                Texture2D img = new Texture2D(38, 38, TextureFormat.ARGB32, false);
                img = GameDatabase.Instance.GetTexture("KSPCalendar/Icons/KSPC_Button", false);
                launcherButton = ApplicationLauncher.Instance.AddModApplication(showWindow, hideWindow, null, null, null, null, ApplicationLauncher.AppScenes.ALWAYS,
                   img);
            }
        }
        internal void showWindow()  // triggered by application launcher
        {
            doShowCalendarWindow = !doShowCalendarWindow;
            //launcherButton.SetTexture()
        }

        internal void hideWindow() // triggered by application launcher
        {
            doShowCalendarWindow = !doShowCalendarWindow;
        }
#endif
        /// <summary>
        /// Initializes our button for the Toolbar Plugin.
        /// </summary>
        private void initToolbarButtons()
        {
            if (ToolbarManager.ToolbarAvailable && HighLogic.CurrentGame.Parameters.CustomParams<KSPCalendarSettings>().useBlizzyToolbar && tbButton == null)
            {
                tbButton = ToolbarManager.Instance.add("Calendar", "KSPC_ToolbarButton");
                tbButton.TexturePath = "KSPCalendar/Icons/KSPC_Button";
                tbButton.ToolTip = "Toggle Calendar";
                tbButton.OnClick += (ClickEvent e) =>
                {
                    doShowCalendarWindow = !doShowCalendarWindow;
                    tbButton.TexturePath = doShowCalendarWindow ? "KSPCalendar/Icons/KSPC_Button_Enabled" : "KSPCalendar/Icons/KSPC_Button";
                };
            }
            else
            {
                OnGUIApplicationLauncherReady();
                GameEvents.onGUIApplicationLauncherReady.Add(OnGUIApplicationLauncherReady);
                
            }
        }


        /// <summary>
        /// Initializes the styles for our windows, boxes and buttons.
        /// </summary>
        private void initStyles()
        {
            GUI.skin = HighLogic.Skin;

            styleCalendarWindow = new GUIStyle(GUI.skin.window);
            styleCalendarWindow.fixedWidth = 235;
            styleCalendarWindow.fixedHeight = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalendarSettings>().doShowSystemTime && doDrawDefaultWindow ? 90 : 62;
            styleCalendarWindow.fontSize = 11;

            // ****

            styleConfigWindow = new GUIStyle(GUI.skin.window);
            styleConfigWindow.fixedWidth = 230;
            styleConfigWindow.fixedHeight = 240;
            styleConfigWindow.fontSize = 11;

            // ****

            styleMinimalisticLabel = new GUIStyle(GUI.skin.label);
            styleMinimalisticLabel.fixedWidth = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalendarSettings>().doShowSystemTime ? 300 : 150;
            styleMinimalisticLabel.fixedHeight = 30;

            // ****

            styleBoxWhite = new GUIStyle(GUI.skin.box);
            styleBoxWhite.normal.textColor = Color.white;
            styleBoxWhite.fontStyle = FontStyle.Normal;
            styleBoxWhite.fontSize = 11;
            styleBoxWhite.alignment = TextAnchor.MiddleCenter;
            styleBoxWhite.wordWrap = true;
            styleBoxWhite.fixedWidth = 90;
            styleBoxWhite.fixedHeight = 28;

            // ****

            styleBoxYellow = new GUIStyle(GUI.skin.box);
            styleBoxYellow.normal.textColor = Color.yellow;
            styleBoxYellow.fontStyle = FontStyle.Normal;
            styleBoxYellow.fontSize = 11;
            styleBoxYellow.alignment = TextAnchor.MiddleCenter;
            styleBoxYellow.wordWrap = true;
            styleBoxYellow.fixedWidth = 120;
            styleBoxYellow.fixedHeight = 28;

            // ****

            styleToggle = new GUIStyle(GUI.skin.toggle);
            styleToggle.normal.textColor = Color.white;
            styleToggle.fontStyle = FontStyle.Normal;
            styleToggle.fontSize = 11;
            styleToggle.wordWrap = false;

            // ****

            styleDialogButton = new GUIStyle(GUI.skin.button);
            styleDialogButton.normal.textColor = Color.white;
            styleDialogButton.hover.textColor = Color.yellow;
            styleDialogButton.fontStyle = FontStyle.Normal;
            styleDialogButton.fontSize = 11;
            styleDialogButton.alignment = TextAnchor.MiddleCenter;
            styleDialogButton.wordWrap = true;

        }
    }
}


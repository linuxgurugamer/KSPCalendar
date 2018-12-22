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
//using KSPCalendar.Toolbar;
using ToolbarControl_NS;
using ClickThroughFix;

namespace KSPCalendar
{
    public partial class Calendar
    {
        private static GUIStyle
            styleCalendarWindow, //styleConfigWindow,
            styleBoxWhite, styleBoxYellow,
            //styleToggle,
            styleMinimalisticLabel;
        //styleDialogButton;
        bool stylesInitted = false;
        bool oldSkin, oldBold, olddoShowSystemTime;
        int oldFontSize;
        // IButton tbButton;
        //private IButton tbButtonCfg;

        //ApplicationLauncherButton launcherButton = null;
        ToolbarControl toolbarControl;


        // ****
        void removeLauncherButtons()
        {
            Debug.Log("removeLauncherButtons");
            toolbarControl.OnDestroy();
            Destroy(toolbarControl);
            toolbarControl = null;
        }

        internal const string MODID = "KSPCalendar_NS";
        internal const string MODNAME = "KSPCalendar";

        void OnGUIApplicationLauncherReady()
        {
            toolbarControl = gameObject.AddComponent<ToolbarControl>();
            toolbarControl.AddToAllToolbars(showWindow, hideWindow,
                ApplicationLauncher.AppScenes.SPACECENTER | ApplicationLauncher.AppScenes.FLIGHT |
                ApplicationLauncher.AppScenes.MAPVIEW | ApplicationLauncher.AppScenes.VAB |
                ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.TRACKSTATION,
                MODID,
                "KSPCalendarButton",
                "KSPCalendar/PluginData/Icons/KSPC_Button_Enabled_32",
                "KSPCalendar/PluginData/Icons/KSPC_Button_32",
                "KSPCalendar/PluginData/Icons/KSPC_Button_Enabled_24",
                "KSPCalendar/PluginData/Icons/KSPC_Button_24",
                "KSPCalendar"
            );
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

        /// <summary>
        /// Initializes our button for the Toolbar Plugin.
        /// </summary>
        private void initToolbarButtons()
        {
            {
                OnGUIApplicationLauncherReady();

            }
        }


        /// <summary>
        /// Initializes the styles for our windows, boxes and buttons.
        /// </summary>
        private void initStyles()
        {
            if (HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().useKSPskin)
                GUI.skin = HighLogic.Skin;
            if (!stylesInitted || oldSkin != HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().useKSPskin ||
                oldFontSize != HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize ||
                oldBold != HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().bold ||
                olddoShowSystemTime != HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime)
            {
                stylesInitted = true;
                oldSkin = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().useKSPskin;
                oldFontSize = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize;
                oldBold = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().bold;
                olddoShowSystemTime = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime;

                styleCalendarWindow = new GUIStyle(GUI.skin.window);
                styleCalendarWindow.fixedWidth = 235 * HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize / 11;
                styleCalendarWindow.fixedHeight = (HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime && doDrawDefaultWindow ? 90 : 62) * HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize / 11;
                styleCalendarWindow.fontSize = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize;

                // ****

                styleMinimalisticLabel = new GUIStyle(GUI.skin.label);
                styleMinimalisticLabel.fixedWidth = (HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime ? 300 : 150) * HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize / 11;
                styleMinimalisticLabel.fixedHeight = 30 * HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize / 11;

                // ****

                styleBoxWhite = new GUIStyle(GUI.skin.box);
                styleBoxWhite.normal.textColor = Color.white;
                styleBoxWhite.fontStyle = FontStyle.Normal;
                styleBoxWhite.fontSize = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize;
                styleBoxWhite.alignment = TextAnchor.MiddleCenter;
                styleBoxWhite.wordWrap = true;
                styleBoxWhite.fixedWidth = 90 * HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize / 11;
                styleBoxWhite.fixedHeight = 28 * HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize / 11;

                // ****

                styleBoxYellow = new GUIStyle(GUI.skin.box);
                styleBoxYellow.normal.textColor = Color.yellow;
                styleBoxYellow.fontStyle = FontStyle.Normal;
                styleBoxYellow.fontSize = HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize;
                styleBoxYellow.alignment = TextAnchor.MiddleCenter;
                styleBoxYellow.wordWrap = true;
                styleBoxYellow.fixedWidth = 120 * HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize / 11;
                styleBoxYellow.fixedHeight = 28 * HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().fontSize / 11;
                
                // ****

                if (HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().bold)
                {
                    styleBoxWhite.fontStyle = styleBoxYellow.fontStyle = FontStyle.Bold;
                }
            }
        }
    }
}


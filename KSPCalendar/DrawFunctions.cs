/// <summary>
/// 
/// DrawFunctions.cs
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
using System.Globalization;
using UnityEngine;
using KSP.UI.Screens.Flight;
using METDisplay = KSP.UI.Screens.Flight.METDisplay;
using ClickThroughFix;

namespace KSPCalendar
{
    public partial class Calendar
    {
        // private bool isMinimalisticView = false;
        //private bool isKerbinTimeScale = false;
#if true
        private bool doShowConfigWindow = false;
        private bool doShowHelpWindow = false;
#endif
        // private bool doOverrideMETDisplay = false;
        private bool doShowCalendarWindow = false;
        //private bool doShowSystemTime = false;
        private bool doDrawDefaultWindow = true;

        private Rect posCalendarWindow;
        private Rect posMiniCalendarWindow;
        private Rect posConfigWindow;
        private Rect posHelpWindow = new Rect(0, 0, 400, 600);

        // private FlightGlobals flightUICtrl;

        /* WIP
        private TimeWarp mTimewarpObject;
        private int metState = 0;
        */

        // ****

        /// <summary>
        /// Draws all windows depending on settings.
        /// </summary>
        private void drawWindows()
        {
#if true
            if (doShowConfigWindow)
                posConfigWindow = ClickThruBlocker.GUILayoutWindow(3, posConfigWindow, drawSettingsWindow, "Settings", styleConfigWindow);
            if (doShowHelpWindow)
                posHelpWindow = ClickThruBlocker.GUILayoutWindow(35, posHelpWindow, drawHelpWindow, "Help");
#endif
            if (!doShowCalendarWindow)
                return;

            // Hide calendar window from Flight Scene, if Override MET option is enabled
            if (HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doOverrideMETDisplay && HighLogic.LoadedSceneIsFlight) {
                doDrawDefaultWindow = false;
            } else
                doDrawDefaultWindow = true;

            if (doDrawDefaultWindow || HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime) {
                if (HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().isMinimalisticView)
                    posMiniCalendarWindow = ClickThruBlocker.GUILayoutWindow (1, posMiniCalendarWindow, drawMiniCalendarWindow, "", styleMinimalisticLabel);
                else
                    posCalendarWindow = ClickThruBlocker.GUILayoutWindow (2, posCalendarWindow, drawCalendarWindow, "Kerbin Calendar", styleCalendarWindow);
            }

            /* WIP

            mTimewarpObject = TimeWarp.fetch;
            if (mTimewarpObject == null || mTimewarpObject.timeQuadrantTab == null)
                return;

            float scale = ScreenSafeUI.VerticalRatio * 900.0f / Screen.height;
            print ("KSPC: scale = " + scale.ToString ());
            Vector2 screenCoord = ScreenSafeUI.referenceCam.WorldToScreenPoint(mTimewarpObject.timeQuadrantTab.transform.position);
            print ("KSPC: screenCoord = " + screenCoord.ToString ());
            print ("KSPC: timeModeBtnUT_0.height = " + timeModeBtnUT_0.height.ToString ());
            Rect pos = new Rect(
                mTimewarpObject.timeQuadrantTab.transform.position.x, Screen.height - screenCoord.y,
                (mTimewarpObject.timeQuadrantTab.renderer.material.mainTexture.width - 39.3f) / scale, (timeModeBtnUT_0.height * 0.7f) / scale);
            print ("KSPC: pos = " + pos.ToString ());

            flightUICtrl.timeModeBtn.OnStatePress = new ScreenSafeUIStateButton.StateButtonPressCallback (this.metTimerBtnGUIClicked);

            switch (metState) {
                case 0: 
                    //GUI.DrawTexture(pos, timeModeBtnUT_0);
                    flightUICtrl.timeModeBtn.renderer.material.mainTexture = timeModeBtnUT_0;
                    break;
                case 1:
                    //GUI.DrawTexture(pos, timeModeBtnKT_0);
                    flightUICtrl.timeModeBtn.renderer.material.mainTexture = timeModeBtnKT_0;
                    break;
                case 2:
                    //GUI.DrawTexture(pos, timeModeBtnMET_0);
                    flightUICtrl.timeModeBtn.renderer.material.mainTexture = timeModeBtnMET_0;
                    flightUICtrl.met.text = dtKerbinCurrent.ToString(strDateTimeFormat);
                    break;
            }
            */
        }

        /* WIP
        private void metTimerBtnGUIClicked(int st)
        {
            switch (metState) {
                case 0:
                    flightUICtrl.displayUT = false;
                    metState = 1;
                    break;
                case 1:
                    flightUICtrl.displayUT = true;                    
                    metState = 2;
                    break;
                case 2:
                    flightUICtrl.displayUT = false;
                    metState = 0;
                    break;
                }
        } */

        private void LateUpdate()
        {
            if (HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doOverrideMETDisplay && HighLogic.LoadedSceneIsFlight)
            {
                //getCurrentKerbinDateTime();
                string kspDatetime = kspDateTimeFormatter.getDateTimeStr(strDateTimeFormat);
                METDisplay met = GameObject.FindObjectOfType<METDisplay>();
                if (met != null)
                {
                    // met.text.text = dtKerbinCurrent.ToString(strDateTimeFormat);
                    met.text.text = dtKerbinCurrent.ToString(kspDatetime);
                    //Debug.Log("met.text.test = " + dtKerbinCurrent.ToString(strDateTimeFormat));
                }
                //                flightUICtrl.met.text = dtKerbinCurrent.ToString (strDateTimeFormat);
            }
            else
                doDrawDefaultWindow = true;
        }

        /// <summary>
        /// Draws the calendar main window.
        /// </summary>
        private void drawCalendarWindow(int id) {
            //getCurrentKerbinDateTime ();
            string kspDatetime = kspDateTimeFormatter.getDateTimeStr(strDateTimeFormat);
            // ****

            DateTime dtSystemTime = DateTime.Now;

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            if (!doDrawDefaultWindow && HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime) {
                GUILayout.Box ("System Date", styleBoxWhite);
                GUILayout.Box (dtSystemTime.ToString (strDateTimeFormat), styleBoxYellow);
            } else {
                GUILayout.Box ("Kerbin Date", styleBoxWhite);
               // GUILayout.Box (dtKerbinCurrent.ToString (strDateTimeFormat), styleBoxYellow);

                GUILayout.Box(kspDatetime, styleBoxYellow);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // ****

            if (doDrawDefaultWindow && HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime) {
                GUILayout.BeginVertical ();
                GUILayout.BeginHorizontal ();
                GUILayout.Box ("System Date", styleBoxWhite);
                GUILayout.Box (dtSystemTime.ToString (strDateTimeFormat), styleBoxYellow);
                GUILayout.EndHorizontal ();
                GUILayout.EndVertical ();
            }

            // ****

            GUI.DragWindow();
        }

        /// <summary>
        /// Draws the minimalistic view calendar window.
        /// </summary>
        private void drawMiniCalendarWindow(int id)
        {
            //getCurrentKerbinDateTime ();
            string kspDatetime = kspDateTimeFormatter.getDateTimeStr(strDateTimeFormat);
            // ****

            DateTime dtSystemTime = DateTime.Now;
            
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            if (!doDrawDefaultWindow && HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime) {
                GUILayout.Box (dtSystemTime.ToString (strDateTimeFormat), styleBoxYellow);
            } else {
                GUILayout.Box(dtKerbinCurrent.ToString(strDateTimeFormat), styleBoxYellow);
                GUILayout.Box(kspDatetime, styleBoxYellow);
            }

            if (doDrawDefaultWindow && HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime) {
                GUILayout.Box (dtSystemTime.ToString (strDateTimeFormat), styleBoxYellow);
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUI.DragWindow();
        }
#if true
        /// <summary>
        /// Draws the settings window.
        /// </summary>
        private void drawSettingsWindow(int id)
        {
 
            if (GUI.Button(new Rect(posConfigWindow.width - 24, 4, 20, 20), "?", closeButtonStyle))
            {
                doShowHelpWindow = true;
            }

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Box("Initial Date", styleBoxWhite);
            strConfigInitialDateTime = GUILayout.TextField (strConfigInitialDateTime, 22, styleBoxYellow);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // ****

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Box("Display Format", styleBoxWhite);
            strConfigDateTimeFormat = GUILayout.TextField (strConfigDateTimeFormat, 22, styleBoxYellow);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // ****

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doOverrideMETDisplay = GUILayout.Toggle(HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doOverrideMETDisplay, "Override MET timer in flight mode", styleToggle, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().isMinimalisticView = GUILayout.Toggle(HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().isMinimalisticView, "Minimalistic view", styleToggle, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

#if false
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().isKerbinTimeScale = GUILayout.Toggle(HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().isKerbinTimeScale, "Kerbin time scale (6 hours day)", styleToggle, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
#endif

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime = GUILayout.Toggle(HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().doShowSystemTime, "Show system time", styleToggle, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            // ****
            try
            {
                var d = DateTime.ParseExact(strConfigInitialDateTime, strConfigDateTimeFormat, CultureInfo.InvariantCulture);
            }
            catch
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Initial date does not match specified date format", styleMessageLabel);
                GUILayout.EndHorizontal();
                GUI.enabled = false;
               
            }
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button ("OK", styleDialogButton, GUILayout.ExpandWidth (true))) {
                doShowConfigWindow = false;
                strDateTimeFormat = strConfigDateTimeFormat;
                //Debug.Log("strConfigInitialDateTime: " + strConfigInitialDateTime + ", strDateTimeFormat: " + strDateTimeFormat);
                dtKerbinInitial = DateTime.ParseExact(strConfigInitialDateTime, strDateTimeFormat, CultureInfo.InvariantCulture);
                saveConfig();
            }
            GUI.enabled = true;
            if (GUILayout.Button ("Cancel", styleDialogButton, GUILayout.ExpandWidth (true))) {
                doShowConfigWindow = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // ****

            GUI.DragWindow();
        }

        /// <summary>
        /// Toggles the config window.
        /// </summary>
        private void toggleConfigWindow()
        {
            strConfigInitialDateTime = dtKerbinInitial.ToString(strDateTimeFormat);
            doShowConfigWindow = !doShowConfigWindow;
        }
#endif
        }
}
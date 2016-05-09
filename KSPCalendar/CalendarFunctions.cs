/// <summary>
/// 
/// CalendarFunctions.cs
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
using KSP.IO;
using UnityEngine;

namespace KSPCalendar
{
    public partial class Calendar
    {

        /// <summary>
        /// Loads the saved config settings.
        /// </summary>
        public void loadConfig()
        {
            PluginConfiguration Config = PluginConfiguration.CreateForType<Calendar> ();
            Config.load ();

            double configPluginVersion = Config.GetValue<double> ("PluginVersion");
            if (configPluginVersion != dblPluginVersion)
                return;

            posCalendarWindow = Config.GetValue<Rect> ("CalendarWindowPosition");
            posMiniCalendarWindow = Config.GetValue<Rect> ("MiniCalendarWindowPosition");

            String strSavedDateTimeFormat = Config.GetValue<String> ("DateTimeFormat");
            if (strSavedDateTimeFormat != null) {
                strDateTimeFormat = strSavedDateTimeFormat;
            }
            strConfigDateTimeFormat = strDateTimeFormat;
            
            String strSavedKerbinDateTime = Config.GetValue<String> ("InitialKerbinDateTime");
            if (strSavedKerbinDateTime != null) {
                dtKerbinInitial = DateTime.ParseExact(strSavedKerbinDateTime, strDateTimeFormat, CultureInfo.InvariantCulture);
            }
            strConfigInitialDateTime = dtKerbinInitial.ToString (strDateTimeFormat);

            isMinimalisticView = Config.GetValue<bool> ("MinimalisticView", false);
            isKerbinTimeScale = Config.GetValue<bool> ("UseKerbinTimeScale", false);
            doOverrideMETDisplay = Config.GetValue<bool> ("OverrideMETDisplay", false);
            doShowSystemTime = Config.GetValue<bool> ("ShowSystemTime", false);
            doShowCalendarWindow = Config.GetValue ("ShowCalendar", false);
        }

        /// <summary>
        /// Loads Time Warp button textures
        /// </summary>
        public void loadTextures() {
            /* WIP
            LoadImage(out timeModeBtnMET_0, "MET_0.png");
            LoadImage(out timeModeBtnMET_1, "MET_1.png");

            LoadImage(out timeModeBtnUT_0, "UT_0.png");
            LoadImage(out timeModeBtnUT_1, "UT_1.png");

            LoadImage(out timeModeBtnKT_0, "KT_0.png");
            LoadImage(out timeModeBtnKT_1, "KT_1.png");
            */
        }

        /// <summary>
        /// Saves the config settings.
        /// </summary>
        public void saveConfig(bool doDestroy = false)
        {
            KSP.IO.PluginConfiguration Config = KSP.IO.PluginConfiguration.CreateForType<Calendar> ();

            Config.SetValue ("PluginVersion", dblPluginVersion);
            Config.SetValue ("CalendarWindowPosition", posCalendarWindow);
            Config.SetValue ("MiniCalendarWindowPosition", posMiniCalendarWindow);
            Config.SetValue ("InitialKerbinDateTime", dtKerbinInitial.ToString (strDateTimeFormat));
            Config.SetValue ("DateTimeFormat", strDateTimeFormat);
            Config.SetValue ("OverrideMETDisplay", doOverrideMETDisplay);
            Config.SetValue ("MinimalisticView", isMinimalisticView);
            Config.SetValue ("UseKerbinTimeScale", isKerbinTimeScale);
            Config.SetValue ("ShowSystemTime", doShowSystemTime);
            Config.SetValue ("ShowCalendar", doShowCalendarWindow);

            Config.save ();
        }

        /// <summary>
        /// Determines whether the current view is a valid scene for us.
        /// </summary>
        private bool isValidScene()
        {
            if (!HighLogic.LoadedScene.Equals(GameScenes.LOADING) && (HighLogic.LoadedSceneIsFlight || HighLogic.LoadedScene.Equals(GameScenes.SPACECENTER) || HighLogic.LoadedScene.Equals(GameScenes.TRACKSTATION))) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the current kerbin date time.
        /// It is basically just InitialKerbinDateTime + UniversalTime passed in seconds since the game was created.
        /// </summary>
        private void getCurrentKerbinDateTime()
        {
            double dblUniversalTime = Planetarium.GetUniversalTime ();

            if (isKerbinTimeScale) {
                dtKerbinInitial = dtKerbinInitial.Date;
                dblUniversalTime = (dblUniversalTime + 11700.0 - ((21600.0 / 9203544.6) * dblUniversalTime)) * 4;
            }

            dtKerbinCurrent = dtKerbinInitial.AddSeconds (dblUniversalTime);
        }

        /// <summary>
        /// Loads an image.
        /// </summary>
        private void LoadImage(out Texture2D texture, String fileName)
        {
            fileName = fileName.Split('.')[0];
            String path = "KSPCalendar/Textures/" + fileName;
            texture = GameDatabase.Instance.GetTexture (path, false);
            if (texture == null) {
                print ("LoadImage ERROR: " + path);
                texture = new Texture2D (32, 32);
                texture.Apply ();
            } else {
                print ("LoadImage OK: " + path);
            }
        }

    }
}


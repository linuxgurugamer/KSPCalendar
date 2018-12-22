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
using System.IO;
using System.Globalization;
using KSP.IO;
using UnityEngine;

namespace KSPCalendar
{
    public partial class Calendar
    {
        private const string CONFIGPATH = "GameData/KSPCalendar/PluginData/";
        private const string CONFIGFILE = "KSPCalendar.cfg";


        static public string GetConfigFileName
        {
            get
            {
                string s = Path.Combine(KSPUtil.ApplicationRootPath, CONFIGPATH).Replace('\\', '/') + "/" + CONFIGFILE;
                return s;
            }
        }
   
        Rect StringToRect(string s)
        {
            char[] delimiterChars = { ' ', ',' };

            
            string[] sar = s.Split(delimiterChars);
            Rect rect = default(Rect);
            float x;
            if (float.TryParse(sar[0], out x))
            rect.x = x;
            if (float.TryParse(sar[1], out x))
            rect.y = x;
            if (float.TryParse(sar[2], out x))
            rect.width = x;
            if (float.TryParse(sar[3], out x))
            rect.height = x;
            return rect;
        }
        string RectToString(Rect r)
        {
            return r.x.ToString() + "," + r.y.ToString() + "," + r.width.ToString() + "," + r.height.ToString();
        }
        /// <summary>
        /// Loads the saved config settings.
        /// </summary>
        public void loadConfig()
        {
            if (System.IO.File.Exists(GetConfigFileName))
            {
                ConfigNode file = ConfigNode.Load(GetConfigFileName);
                if (file != null)
                {
                    ConfigNode node = file.GetNode("KSPCalendar");
                    string s = "";
                    node.TryGetValue("CalendarWindowPosition", ref s);
                    posCalendarWindow = StringToRect(s);
                    node.TryGetValue("MiniCalendarWindowPosition", ref s);
                    posMiniCalendarWindow = StringToRect(s);
                    s = "";
                    node.TryGetValue("InitialKerbinDateTime", ref s);
                    dtKerbinInitial = DateTime.ParseExact(s, strDateTimeFormat, CultureInfo.InvariantCulture);
                    node.TryGetValue("DateTimeFormat", ref strDateTimeFormat);
                    node.TryGetValue("ShowCalendar", ref doShowCalendarWindow);
                }
            }
        }

        /// <summary>
        /// Saves the config settings.
        /// </summary>
        public void saveConfig(bool doDestroy = false)
        {

            ConfigNode file = new ConfigNode();
            ConfigNode node = new ConfigNode();
            
            node.AddValue("CalendarWindowPosition", RectToString(posCalendarWindow));
            node.AddValue("MiniCalendarWindowPosition", RectToString(posMiniCalendarWindow));
            node.AddValue("InitialKerbinDateTime", dtKerbinInitial.ToString(strDateTimeFormat));
            node.AddValue("DateTimeFormat", strDateTimeFormat);
            node.AddValue("ShowCalendar", doShowCalendarWindow);
            file.AddNode("KSPCalendar", node);
            file.Save(GetConfigFileName );
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

            if (HighLogic.CurrentGame.Parameters.CustomParams<KSPCalSettings>().isKerbinTimeScale) {
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


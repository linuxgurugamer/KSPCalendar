/// <summary>
/// 
/// Calendar.cs
/// 
/// Kerbal Space Program Calendar Plugin :: Copyright (C) 2014 nuclearping
/// 
/// Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com
/// This project or the author is in no way associated with nor endorsed by Squad.
/// 
/// This Source Code and Plugin are released under the Creative Commons Public Domain Mark 1.0 license.
/// See http://creativecommons.org/publicdomain/mark/1.0/ for further details.
/// 
/// ---- Additional Information ----
/// 
/// You are of course free to change, contribute or modify this plugin.
/// 
/// However if you plan to improve its functionality and/or features you are invited to send me a PM on
/// Kerbal Space Program forums http://forum.kerbalspaceprogram.com/members/94977-nuclearping so that we
/// can maintain a consistent branch.
/// 
/// ---- Version Log ----
/// 
/// Version 1.5-14/08/25 - nuclearping
///     * Removed included Toolbar from Mod package
///     * Removed config button from calendar window
///     * Integrated Config button into Toolbar
///     * Removed obsolete Calendar window from Flight Scene when "Override MET display" is enabled
///     * Calendar window will now only be visible in Flight Scence when "Show System date" and "Override MET display" are enabled
///       to display the System date / time.
/// 
/// Version 1.4-14/08/24 - nuclearping
///     * Recompile for 0.24.2
///     * Added Option to override flight mode MET display with KSPCalendar Date
/// 
/// Version 1.3-14/02/13 - nuclearping
///     * Fixed the "Kerbin time scale" problem from 1.2
///     * The Calendar window will now stay visible when switching scenes
///     * Removed unnecessary config window entries introduced with 1.2
/// 
///     * Thanks to diomedea who helped me figuring out the problem from 1.2
///     * Also thanks to Christian who helped us aswell figuring out the problems with the getCurrentKerbinDateTime()-formula for Kerbin time scale
/// 
/// Version 1.2-14/02/08 - nuclearping
///     * Found an issue in the "Kerbin time scale" calculation.
///         In the long run the date & time go awfully inaccurate.
///            Like it is late at night at KSC but Calendar shows ... 14:37
///         So my tests have shown that either Kerbin doesn't rotate exactly
///         4 times faster or there is some sort of floating point inaccuracy 
///         occuring in the long run.
///         However I found out that a fixed Time Scale factor of 3.991 does a
///         pretty good job instead.
///         So with an Initial Time of ... 13:00 and a Kerbin factor of 3.991
///         you should get an accurate result.
/// 
/// Version 1.1-14/02/04 - nuclearping
///     * Toolbar implementation
///     * Added "Kerbin time scale" feature
///     * Added "System time" feature
///     * Cleaned up GUI
///     * Cleaned up code
/// 
/// Version 1.0-14/02/02 - nuclearping
///     * Initial release
/// 
/// </summary>

using System;
using UnityEngine;
//using KSPCalendar.Toolbar;

namespace KSPCalendar
{
    [KSPAddon(KSPAddon.Startup.AllGameScenes, true)]
    public partial class Calendar : MonoBehaviour
    {
        internal static Calendar Instance;

        /// <summary>
        /// Called when the Plugin is loaded.
        /// </summary>
        public void Awake()
        {
            Instance = this;
            loadConfig();
            initToolbarButtons ();
            DontDestroyOnLoad(this);
            //loadTextures ();
        }

        KSPDateTimeFormatter kspDateTimeFormatter;

        /// <summary>
        /// Called once everything in the scene (including other plugins) have loaded,
        /// just before the first execution tick.
        /// </summary>
        public void Start()
        {
            print ("KSPCalendar Plugin Started [Version " + dblPluginVersion.ToString() + "]");
            kspDateTimeFormatter = new KSPDateTimeFormatter();
        }

        /// <summary>
        /// Called every frame by the Unity Rendering Manager.
        /// </summary>
        public void OnGUI()
        {
            if (!isValidScene ())
                return;

            initStyles ();
            drawWindows ();
        }

        /// <summary>
        /// Called on APOCALYPSE! \o/
        /// </summary>
        public void OnDestroy()
        {
            saveConfig (true);
            removeLauncherButtons();
        }
    }
}


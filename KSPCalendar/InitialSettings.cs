/// <summary>
/// 
/// InitialSettings.cs
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

namespace KSPCalendar
{
    public partial class Calendar
    {
        private double dblPluginVersion = 1.5;

        private DateTime dtKerbinInitial = new DateTime(1980, 1, 1, 0, 0, 0),
            dtKerbinCurrent = new DateTime(1980, 1, 1, 0, 0, 0);

        private String strDateTimeFormat = "yyyy/MM/dd HH:mm:ss", strConfigInitialDateTime = "", 
            strConfigDateTimeFormat = "";

        /* WIP
        private Texture2D timeModeBtnMET_0, timeModeBtnMET_1, timeModeBtnUT_0, timeModeBtnUT_1, timeModeBtnKT_0, timeModeBtnKT_1;
        */
    }
}

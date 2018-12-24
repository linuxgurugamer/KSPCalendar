using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPCalendar
{
    public partial class Calendar
    {
             
        protected void drawHelpWindow(int windowID)
        {
            GUILayout.BeginVertical();

            GUILayout.Label("Allowable Date Format Specifiers");
            GUILayout.Space(10);
            GUILayout.Label("\"d\"      The day of the month, from 1 through 31.", styleLabel);
            GUILayout.Label("\"dd\"     The day of the month, from 01 through 31.styleLabel", styleLabel);
            GUILayout.Label("\"ddd\"    The abbreviated name of the day of the week.", styleLabel);
            GUILayout.Label("\"dddd\"   The full name of the day of the week.", styleLabel);
            GUILayout.Space(10);
            GUILayout.Label("\"M\"      The month, from 1 through 12.", styleLabel);
            GUILayout.Label("\"MM\"     The month, from 01 through 12.", styleLabel);
            GUILayout.Label("\"MMM\"    The abbreviated name of the month.", styleLabel);
            GUILayout.Label("\"MMMM\"   The full name of the month.", styleLabel);
            GUILayout.Space(10);
            GUILayout.Label("\"y\"      The year, from 0 to 99.", styleLabel);
            GUILayout.Label("\"yy\"     The year, from 00 to 99.", styleLabel);
            GUILayout.Label("\"yyy\"    The year, with a minimum of three digits.", styleLabel);
            GUILayout.Label("\"yyyy\"   The year as a four-digit number.", styleLabel);
            GUILayout.Space(10);
            GUILayout.Label("\"h\"      The hour, using a 12-hour clock from 1 to 12.", styleLabel);
            GUILayout.Label("\"hh\"	    The hour, using a 12-hour clock from 01 to 12.", styleLabel);
            GUILayout.Label("\"H\"      The hour, using a 24-hour clock from 0 to 23.", styleLabel);
            GUILayout.Label("\"m\"      The minute, from 0 through 59.", styleLabel);
            GUILayout.Label("\"mm\"	    The minute, from 00 through 59.", styleLabel);
            GUILayout.Space(10);
            GUILayout.Label("\"s\"      The second, from 0 through 59.", styleLabel);
            GUILayout.Label("\"ss\"     The second, from 00 through 59.", styleLabel);
            GUILayout.Label("\"t\"      The first character of the AM/PM designator.", styleLabel);
            GUILayout.Label("\"tt\"     The AM/PM designator.", styleLabel);

            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Close", GUILayout.Width(60)))
            {
                doShowHelpWindow = false;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUI.DragWindow();
        }
    }
}

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;



namespace KSPCalendar
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings

    public class KSPCalendarSettings : GameParameters.CustomParameterNode
    {
        public override string Title { get { return ""; } } // Column header
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "KSP Calendar"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }


        [GameParameters.CustomParameterUI("Override MET timer in flight mode")]
        public bool doOverrideMETDisplay = true;

        [GameParameters.CustomParameterUI("Minimalistic view")]
        public bool isMinimalisticView = false;

        [GameParameters.CustomParameterUI("Kerbin time scale (6 hours day)")]
        public bool isKerbinTimeScale = true;

        [GameParameters.CustomParameterUI("Show system time")]
        public bool doShowSystemTime = false;

        [GameParameters.CustomParameterUI("Use Blizzy toolbar (if available)")]
        public bool useBlizzyToolbar = true;


#if false
        public override void SetDifficultyPreset(GameParameters.Preset preset)
        {
            switch (preset)
            {
                case GameParameters.Preset.Easy:
                    toolbarEnabled = true;
                    toolbarPopupsEnabled = true;
                    editorMenuPopupEnabled = true;
                    hoverTimeout = 0.5f;
                    break;

                case GameParameters.Preset.Normal:
                    toolbarEnabled = true;
                    toolbarPopupsEnabled = true;
                    editorMenuPopupEnabled = true;
                    hoverTimeout = 0.5f;
                    break;

                case GameParameters.Preset.Moderate:
                    toolbarEnabled = true;
                    toolbarPopupsEnabled = true;
                    editorMenuPopupEnabled = true;
                    hoverTimeout = 0.5f;
                    break;

                case GameParameters.Preset.Hard:
                    toolbarEnabled = true;
                    toolbarPopupsEnabled = true;
                    editorMenuPopupEnabled = true;
                    hoverTimeout = 0.5f;
                    break;
            }
        }
#endif

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            //if (member.Name == "enabled")
            //    return true;

            return true; //otherwise return true
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {

            return true;
            //            return true; //otherwise return true
        }

        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }

    }
}

using System.Collections;
using System.Reflection;

namespace KSPCalendar
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings


    public class KSPCalSettings : GameParameters.CustomParameterNode
    {

        public override string Title { get { return ""; } } // Column header
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "KSP Calendar"; } }
        public override string DisplaySection { get { return "KSP Calendar"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }


        [GameParameters.CustomParameterUI("Override MET timer in flight mode",
            toolTip = "When enabled, the MET timer in the upper left corner will be overwritten by this")]
        public bool doOverrideMETDisplay = true;

        [GameParameters.CustomParameterUI("Minimalistic view",
            toolTip = "Just the date/time, nothing else")]
        public bool isMinimalisticView = false;

        [GameParameters.CustomParameterUI("Kerbin time scale (6 hours day)")]
        public bool isKerbinTimeScale = true;

        [GameParameters.CustomParameterUI("Show system time")]
        public bool doShowSystemTime = false;

        [GameParameters.CustomIntParameterUI("Font size", minValue = 9, maxValue = 21)]
        public int fontSize = 11;

        [GameParameters.CustomParameterUI("Bold font")]
        public bool bold = false;

        [GameParameters.CustomParameterUI("Use KSP Skin")]
        public bool useKSPskin = true;



        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            return true; //otherwise return true
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            return true;
        }

        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }

    }
}

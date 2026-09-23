// ============================================================================
//  RyzomTimeConverter.cs — Atys time strings from a server tick.
//  Format: "21:47 - Holeth, Harvestor 12, 3rd AC 2640"
//
//  "3rd AC" = 3rd cycle of the Jena Year (API field `cycle`+1, "Cycle d'Anno",
//  English "3rd AC" = "3e CA"). Calendar: 12 months of 30 days per Jena Year.
//  Field-for-field verified against api.ryzom.com/time.php?format=xml
//  (constants match ryzom_api functions_time.php, minus the legacy 61-day
//  tick offset — the current epoch is JY 2626 at tick 0).
//
//  Usage:
//      long tick = ...;  // server tick (10 per second)
//      RyzomTimeConverter.GetTimeString(tick);
// ============================================================================

namespace API.Helper
{
    public static class RyzomTimeConverter
    {
        public const uint TicksPerHour = 1800;         // 3 real minutes
        public const uint TicksPerDay = 43200;        // 24 h
        public const uint DaysPerWeek = 6;
        public const uint DaysPerMonth = 30;
        public const uint MonthsPerYear = 12;
        public const uint DaysPerYear = 360;         // 12 * 30
        public const uint SeasonDays = 90;          // 3 months
        public const long StartJY = 2626;        // JY at tick 0

        static readonly string[] Months =
        {
            "Winderly", "Germinally", "Folially",      // Spring
            "Floris",   "Medis",      "Thermis",       // Summer
            "Harvestor","Frutor",     "Fallenor",      // Autumn
            "Pluvia",   "Mystia",     "Nivia",         // Winter
        };

        static readonly string[] Days = { "Prima", "Dua", "Tria", "Quarta", "Quinteth", "Holeth" };
        static readonly string[] Seasons = { "Spring", "Summer", "Autumn", "Winter" };

        /// <summary>"21:47 - Holeth, Harvestor 12, 3rd AC 2640" (hh = game hour as in-game HUD, no leading zero).</summary>
        public static string GetTimeString(long tick)
        {
            long day = tick / TicksPerDay;
            if (day < 0) day = DaysPerYear - (-day % DaysPerYear);   // API behaviour
            uint hour = (uint)(tick % TicksPerDay / TicksPerHour);
            uint minute = (uint)(tick % TicksPerHour / 10 / 60);

            string dow = Days[(int)(day % DaysPerWeek + DaysPerWeek) % (int)DaysPerWeek];
            long dojy = day % DaysPerYear;
            string month = Months[(int)(dojy / DaysPerMonth) % MonthsPerYear];
            uint dom = (uint)(day % DaysPerMonth) + 1;           // 1..30
            uint cycle = (uint)(dojy / 360) + 1;                   // 1..4
            long year = StartJY + day / DaysPerYear;

            return string.Format("{0}:{1:D2} - {2}, {3} {4}, {5} AC {6}",
                hour, minute, dow, month, dom, CycleSuffix(cycle), year);
        }

        static string CycleSuffix(uint cycle) =>
            cycle == 1 ? "1st" : cycle == 2 ? "2nd" : cycle == 3 ? "3rd" : "4th";

        /// <summary>0 = Spring, 1 = Summer, 2 = Autumn, 3 = Winter.</summary>
        internal static uint SeasonIndex(long tick)
        {
            long day = tick / TicksPerDay;
            return (uint)((day / SeasonDays % 4 + 4) % 4);
        }

        internal static string SeasonName(long tick) => Seasons[SeasonIndex(tick)];
    }
}
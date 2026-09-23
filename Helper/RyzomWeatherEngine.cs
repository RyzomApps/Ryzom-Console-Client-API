// ============================================================================
//  RyzomWeatherEngine.cs — deterministic Atys weather from a server tick.
//  One weather setup per 3-game-hour cycle (5400 ticks), picked by
//  wang_hash64(cycle) % totalWeight over the per-continent, per-season
//  weighted table — exactly like ryzomcore's CPredictWeather.
//  Setup tables are the live game data (client weather sheet, season.blend).
//
//  Usage:
//      RyzomWeatherEngine.GetWeatherString(tick, RyzomWeatherTables.Fyros);
//      // -> "It's Autumn and the weather is Fair, 39% Humidity"
//  ============================================================================

namespace API.Helper
{
    /// <summary>Weighted weather setup list for one continent+season.</summary>
    public class WeatherTable
    {
        public struct Entry(uint weight, string setup, string localized, float lighting)
        {
            public uint Weight = weight;
            public string SetupName = setup;
            public string LocalizedName = localized;
            public float Lighting = lighting;
        }

        readonly Entry[] _entries;
        public readonly uint TotalWeight;
        public int Count => _entries.Length;

        public WeatherTable(Entry[] entries)
        {
            _entries = entries;
            foreach (var e in _entries) TotalWeight += e.Weight;
        }

        public Entry Peek(int k) => _entries[k];

        public Entry Pick(uint v)
        {
            uint acc = 0;
            for (int k = 0; k < _entries.Length; k++)
            {
                acc += _entries[k].Weight;
                if (v < acc) return _entries[k];
            }
            return _entries[_entries.Length - 1];
        }
    }

    public static class RyzomWeatherEngine
    {
        public const uint TicksPerWeatherCycle = 5400;      // 3 game hours

        /// <summary>wang_hash64 (nel/misc/wang_hash.h).</summary>
        public static ulong WangHash64(ulong key)
        {
            unchecked
            {
                key = ~key + (key << 21);
                key ^= key >> 24;
                key *= 265;
                key ^= key >> 14;
                key *= 21;
                key ^= key >> 28;
                key += key << 31;
                return key;
            }
        }

        /// <summary>Pick the setup for a continent at a tick (weather value 0..1).</summary>
        public static WeatherTable.Entry GetEntry(long tick, WeatherTable[] seasonTables, out float weatherValue)
        {
            var table = seasonTables[RyzomTimeConverter.SeasonIndex(tick)];
            long cycle = tick / TicksPerWeatherCycle;
            uint v = (uint)(WangHash64((ulong)cycle) % table.TotalWeight);

            uint curr = 0;
            for (int k = 0; k < table.Count; k++)
            {
                var e = table.Peek(k);
                if (v < curr + e.Weight)
                {
                    weatherValue = (v - curr) / (float)e.Weight + k;
                    weatherValue /= table.Count;       // scaled / numWS
                    return e;
                }
                curr += e.Weight;
            }
            weatherValue = 1f;
            return table.Peek(table.Count - 1);
        }

        /// <summary>"It's Autumn and the weather is Fair, 39% Humidity"</summary>
        public static string GetWeatherString(long tick, WeatherTable[] seasonTables)
        {
            var e = GetEntry(tick, seasonTables, out float w);
            string season = RyzomTimeConverter.SeasonName(tick);
            return string.Format("It's {0} and the weather is {1}, {2}% Humidity",
                season, e.LocalizedName, (int)(w * 100));
        }
    }

    public static class RyzomWeatherTables
    {
        public static WeatherTable[] RouteGouffre =
        [
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
        ];

        public static WeatherTable[] Bagne =
        [
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
        ];

        public static WeatherTable[] Lecarrefour =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] Fyros =
        [
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(3, "clouds2", "Rainy", 0.8f),
                new(7, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(50, "Fair1", "Fair", 1f),
                new(30, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(3, "clouds1", "Rainy", 0.9f),
                new(3, "clouds2", "Rainy", 0.8f),
                new(4, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(9, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(15, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(15, "storm", "Thundery", 0.6f),
            ]),
        ];

        public static WeatherTable[] Lepaysmalade =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] Tryker =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] Lesfalaises =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] Lesilesvivantes =
        [
            new(
            [
                new(10, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(30, "clouds1", "Rainy", 0.9f),
                new(20, "clouds2", "Rainy", 0.8f),
                new(1, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(5, "Fair1", "Fair", 1f),
                new(5, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(20, "clouds2", "Rainy", 0.8f),
                new(20, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(10, "Fair1", "Fair", 1f),
                new(5, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(20, "clouds2", "Rainy", 0.8f),
                new(30, "storm", "Thundery", 0.6f),
            ]),
        ];

        public static WeatherTable[] Sources =
        [
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
        ];

        public static WeatherTable[] Terre =
        [
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
        ];

        public static WeatherTable[] Testroom =
        [
            new(
            [
                new(1, "Fair1", "Fair", 1f),
                new(1, "Fair2", "Fair", 1f),
                new(1, "Fair3", "Fair", 1f),
                new(1, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(1, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(1, "Fair1", "Fair", 1f),
                new(1, "Fair2", "Fair", 1f),
                new(1, "Fair3", "Fair", 1f),
                new(1, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(1, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(1, "Fair1", "Fair", 1f),
                new(1, "Fair2", "Fair", 1f),
                new(1, "Fair3", "Fair", 1f),
                new(1, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(1, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(1, "Fair1", "Fair", 1f),
                new(1, "Fair2", "Fair", 1f),
                new(1, "Fair3", "Fair", 1f),
                new(1, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(1, "storm", "Thundery", 0.6f),
            ]),
        ];

        public static WeatherTable[] FyrosIsland =
        [
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(3, "clouds2", "Rainy", 0.8f),
                new(7, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(50, "Fair1", "Fair", 1f),
                new(30, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(3, "clouds1", "Rainy", 0.9f),
                new(3, "clouds2", "Rainy", 0.8f),
                new(4, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(9, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(15, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(15, "storm", "Thundery", 0.6f),
            ]),
        ];

        public static WeatherTable[] FyrosNewbie =
        [
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(3, "clouds2", "Rainy", 0.8f),
                new(7, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(50, "Fair1", "Fair", 1f),
                new(30, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(3, "clouds1", "Rainy", 0.9f),
                new(3, "clouds2", "Rainy", 0.8f),
                new(4, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(9, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(15, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(15, "storm", "Thundery", 0.6f),
            ]),
        ];

        public static WeatherTable[] MatisIsland =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] TrykerIsland =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] TrykerNewbie =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] ZoraiIsland =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] MatisNewbie =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] ZoraiNewbie =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] R2Desert =
        [
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(3, "clouds2", "Rainy", 0.8f),
                new(7, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(50, "Fair1", "Fair", 1f),
                new(30, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(3, "clouds1", "Rainy", 0.9f),
                new(3, "clouds2", "Rainy", 0.8f),
                new(4, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(9, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(15, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(15, "storm", "Thundery", 0.6f),
            ]),
        ];

        public static WeatherTable[] R2Forest =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] R2Jungle =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] R2Lakes =
        [
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(5, "clouds2", "Rainy", 0.8f),
                new(5, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(30, "Fair3", "Fair", 1f),
                new(10, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(10, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(30, "Fair1", "Fair", 1f),
                new(10, "Fair2", "Fair", 1f),
                new(10, "Fair3", "Fair", 1f),
                new(20, "clouds1", "Rainy", 0.9f),
                new(10, "clouds2", "Rainy", 0.8f),
                new(20, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] R2Roots =
        [
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
        ];

        public static WeatherTable[] Newbieland =
        [
            new(
            [
                new(40, "Fair1", "Fair", 1f),
                new(27, "Fair2", "Fair", 1f),
                new(27, "Fair3", "Fair", 1f),
                new(3, "clouds1", "Rainy", 0.9f),
                new(2, "clouds2", "Rainy", 0.8f),
                new(1, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(48, "Fair1", "Fair", 1f),
                new(24, "Fair2", "Fair", 1f),
                new(24, "Fair3", "Fair", 1f),
                new(2, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(1, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(27, "Fair1", "Fair", 1f),
                new(27, "Fair2", "Fair", 1f),
                new(40, "Fair3", "Fair", 1f),
                new(2, "clouds1", "Rainy", 0.9f),
                new(2, "clouds2", "Rainy", 0.8f),
                new(2, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(21, "Fair1", "Fair", 1f),
                new(21, "Fair2", "Fair", 1f),
                new(50, "Fair3", "Fair", 1f),
                new(2, "clouds1", "Rainy", 0.9f),
                new(1, "clouds2", "Rainy", 0.8f),
                new(5, "snow", "Snowy", 0.6f),
            ]),
        ];

        public static WeatherTable[] CorruptedMoor =
        [
            new(
            [
                new(50, "fair1", "Rainy", 0.8f),
                new(20, "fair1", "Rainy", 0.8f),
                new(30, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(50, "fair1", "Rainy", 0.8f),
                new(20, "fair1", "Rainy", 0.8f),
                new(30, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(50, "fair1", "Rainy", 0.8f),
                new(20, "fair1", "Rainy", 0.8f),
                new(30, "storm", "Thundery", 0.6f),
            ]),
            new(
            [
                new(50, "fair1", "Rainy", 0.8f),
                new(20, "fair1", "Rainy", 0.8f),
                new(30, "storm", "Thundery", 0.6f),
            ]),
        ];

        public static WeatherTable[] Kitiniere =
        [
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
            new(
            [
                new(20, "Fair1", "Fair", 1f),
                new(20, "Fair2", "Fair", 1f),
                new(20, "wind1", "Fair", 1f),
                new(15, "humidity1", "Fair", 1f),
                new(15, "humidity2", "Fair", 1f),
                new(10, "Thunderseve", "Sap Thundery", 1f),
            ]),
        ];
    }
}

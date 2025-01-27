﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheCheapsLib
{
    public static class Settings
    {
        public const int maxPlayers = 4;
        /// <summary>
        /// Fall speed when throwing an item, in pixel per second (converted from frames)
        /// </summary>
        internal const float fall_speed = (1.2f)*60f;
        /// <summary>
        /// Time in second before an item on the floor disappears
        /// </summary>
        public const float TimeOnTheFloor = 5f;
        /// <summary>
        /// Score received when selling an item that is not in the current recipe
        /// </summary>
        public const int scoreSellItem = 5;
        /// <summary>
        /// Number of seconds bethween dash moves
        /// </summary>
        public const int InventoryMaxSize = 3;
        /// <summary>
        /// Time before the start of a match
        /// </summary>
        public static float StartGameCountDownMax = 5;
        /// <summary>
        /// Total level size in pixels
        /// </summary>
        public static int LevelWidth = 1920;
        /// <summary>
        /// Total level size in pixels
        /// </summary>
        public static int LevelHeight = 1080;
        /// <summary>
        /// Distance in pixels that the dash move allow to traverse in a single use
        /// </summary>
        internal static float DashDistance = 48;
        /// <summary>
        /// Pixels/Second traversed when moving
        /// </summary>
        public const float MoveSpeed = 6*60f;
        /// <summary>
        /// Exit Speed in pixel per second of a thrown item
        /// </summary>
        public static float ThrowSpeed = 6*60f;
        /// <summary>
        /// Distance to pickup an item
        /// </summary>
        internal static int DistanceForPickup = 24;
        /// <summary>
        /// Distance for interacting with a base
        /// </summary>
        internal static int DistanceForMiningTrash = 64;
        /// <summary>
        /// Number of recipes that you can complete
        /// </summary>
        internal static int RecipesPerPlayer = 1;
        /// <summary>
        /// Number of refeshes per second of the game server
        /// </summary>
        public static float ServerTicksPerSecond { get { return 90; } }
        /// <summary>
        /// Total game duration in seconds, is shown during the "breaking news gui event"
        /// </summary>
        public static int TotalGameTimeS = 60 * 4;
        /// <summary>
        /// Total duration in seconds to show broadcasting news
        /// </summary>
        internal static int BroadCastDuration = 10;
        /// <summary>
        /// Speed of player animation
        /// </summary>
        internal static float PlayerFrameRate = 12;
        /// <summary>
        /// Fix for glitches in animation when walking and server data is intermittent
        /// </summary>
        internal static float ContinueWalkingAnimeFor = 0.125f;
        /// <summary>
        /// Stun duration in seconds
        /// </summary>
        internal static float StunDuration = 3;
        /// <summary>
        /// Height at wich an item get shot when throw is pressed
        /// </summary>
        internal static float ThrowHeight = 42;
        /// <summary>
        /// Number of hits required to mine a resource from a pile
        /// </summary>
        public const int ClicksRequiredToMineResource = 3;
        /// <summary>
        /// Number of seconds after which the clicked counter is resed due to inactivity
        /// </summary>
        public const float MaxTimeBetwheenClicksToMineResource = 30/60f;
        /// <summary>
        /// Number of seconds bethween dash moves
        /// </summary>
        public const float DashRecoilS = 40/60f;


        public const int DurationShakeMsec = 500;
        public const int SpeedShake = 30;
        public const int AmplitudeShake = 1;

    }
}

using BalsaCore;
using HarmonyLib;
using IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CloverTech
{
    class HarmonyContainer
    {
        public static Harmony harmony = new Harmony("Balsa.CloverTech.PlayerAircraftOpsFix");

        public static void DoPatches()
        {
#if DEBUG
            Harmony.DEBUG = true;
#endif
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(Scenarios.Modules.PlayerAircraftOps), "OnVehicleLogEvent")]
        class PlayerAircraftOpsFixPatch
        {
            static bool Prefix()
            {
                Vehicle localPlayerVehicle = GameLogic.LocalPlayerVehicle;
                if ( localPlayerVehicle == null )
                {
#if DEBUG
                    FileLog.Log("NullRef in PlayerAircraftOps");
#endif
                    return false;
                }
                return true;
            }
        }

    }
}
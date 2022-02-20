using UnityEngine;
using BalsaCore;
using IO;
using UI;
using UI.MMX.Data;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.IO;

namespace CloverTech
{
    public class PlayerAircraftOpsFix : MonoBehaviour
    {
        public void Start()
        {
            DontDestroyOnLoad(this);
            LogW("Test");
#if DEBUG
            Debug.LogError("Init");
#endif
            HarmonyContainer.DoPatches();
        }

        public void LogI(string message)
        {
            Debug.Log($"[PlayerAircraftOpsFix] {message}");
        }
        public void LogW(string message)
        {
            Debug.LogWarning($"[PlayerAircraftOpsFix] {message}");
        }
        public void LogE(string message)
        {
            Debug.LogError($"[PlayerAircraftOpsFix] {message}");
        }
    }
    

    [BalsaAddon]
    public class Loader
    {
        private static GameObject go = null;
        private static bool initialised = false;

        [BalsaAddonInit]
        public static void BalsaInit()
        {
            if (!initialised)
            {
                Debug.Log("[PlayerAircraftOpsFix] Creating GameObject");
                go = new GameObject("CloverTech::PlayerAircraftOpsFix");
                go.AddComponent<PlayerAircraftOpsFix>();
                initialised = true;
            }
        }

        [BalsaAddonInit(invokeTime = AddonInvokeTime.Flight)]
        public static void BalsaInitFlight()
        {

        }

        [BalsaAddonFinalize(invokeTime = AddonInvokeTime.Flight)]
        public static void BalsaFinalizeFlight()
        {

        }
        //Game exit
        [BalsaAddonFinalize]
        public static void BalsaFinalize()
        {
            go.DestroyGameObject();
        }

    }
}

using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utils
{
    public static class Utilities
    {
        //CUSTOM EDITOR METHODS
        #region Editor
#if UNITY_EDITOR
        public static void DestroyAllChildren(Transform parent)
        {
            if (parent.childCount <= 0)
                return;

            var tempArray = new GameObject[parent.childCount];
            for (int i = 0; i < tempArray.Length; i++)
                tempArray[i] = parent.GetChild(i).gameObject;

            foreach (var child in tempArray)
                GameObject.DestroyImmediate(child);
        }

        /// <summary>
        /// Indicates if the given scene exists and is set up in the build settings
        /// </summary>
        /// <param name="name">Scene name to check</param>
        public static bool ExistingScene(string name)
        {
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (name != AssetDatabase.LoadAssetAtPath(scene.path, typeof(SceneAsset)).name)
                    continue;
                else
                    return true;
            }

            return false;
        }
#endif
        #endregion

        //CUSTOM METHODS ABOUT CAMERA METHODS
        #region Camera Class
        public abstract class CameraClass
        {
            /// <summary>
            /// Return the current dimension of the orthographic camera
            /// </summary>
            public static Vector2 GetCameraDimensions(Camera camera)
            {
                float height = camera.orthographicSize * 2f;
                float width = height * camera.aspect;
                return new Vector2(width, height);
            }
            public static Vector2 GetCameraDimensions()
            {
                Camera camera = Camera.main;
                float height = camera.orthographicSize * 2f;
                float width = height * camera.aspect;
                return new Vector2(width, height);
            }
        }
        #endregion

        //CUSTOM METHODS ABOUT MATH METHODS
        #region Math Class
        public abstract class Math
        {
            /// <summary>
            /// Compares two floating point values and returns true if they are approximately similar
            /// </summary>
            /// <param name="a"> Variable to compare </param>
            /// <param name="b"> Compared number </param>
            /// <param name="tolerance"> Tolerance between them </param>
            public static bool ApproximationRange(float a, float b, float tolerance)
            {
                return (Mathf.Abs(a - b) < tolerance);
            }

            /// <summary>
            /// Return a random value between a mininmum and a maximum
            /// </summary>
            public static float RandomValue(float min, float max)
            {
                return Random.Range(min, max);
            }

            /// <summary>
            /// Convert a value in decibels
            /// </summary>
            public static float ValueToVolume(float value)
            {
                return Mathf.Log10(value) * 20;
            }
        }
        #endregion

        //CUSTOM METHODS ABOUT STRING METHODS
        #region String Class
        public abstract class String
        {
            public enum RoundingType { F0, F1, F2, F3 }

            /// <summary>
            /// Round an folat number and get a strign with a specified number of decimals
            /// </summary>
            /// <param name="value">Float value to round</param>
            /// <param name="rounding">Rounding type</param>
            public string RoundValueInString(float value, RoundingType rounding)
            {
                switch (rounding)
                {
                    case RoundingType.F1:
                        return value.ToString("F1");
                    case RoundingType.F2:
                        return value.ToString("F2");
                    case RoundingType.F3:
                        return value.ToString("F3");
                    default:
                        return value.ToString("F0");
                }
            }

            /// <summary>
            /// Return a bold string
            /// </summary>
            public static string BoldString(string myString) => $"<b>{myString}</b>";
        }
        #endregion

        //CUSTOM METHODS ABOUT OBJECT LAYERS
        #region Layers Class
        public abstract class Layers
        {
            /// <summary>
            /// Indicates if the layer is in the the given layerMask
            /// </summary>
            /// <param name="mask"> LayerMask to check into </param>
            /// <param name="layer"> Layer to look for </param>
            public static bool LayerMaskContains(LayerMask mask, int layer)
            {
                return mask == (mask | (1 << layer));
            }

            /// <summary>
            /// Enable a layer mask in the culling mask of the main camera
            /// </summary>
            /// <param name="layer"> Layer name </param>
            public static int ShowLayer(string layer)
            {
                return Camera.main.cullingMask |= 1 << LayerMask.NameToLayer(layer);
            }

            /// <summary>
            /// Disable a layer mask in the culling mask of the main camera
            /// </summary>
            /// <param name="layer"> Layer name </param>
            public static int HideLayer(string layer)
            {
                return Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer(layer));
            }

            /// <summary>
            /// Toggeling a layer into the culling mask of the main camera
            /// </summary>
            /// <param name="layer"> Layer name </param>
            public static int ToggleLayer(string layer)
            {
                return Camera.main.cullingMask ^= 1 << LayerMask.NameToLayer(layer);
            }
        }
        #endregion

        //CUSTOM METHODS ABOUT TIME AND TIME CONVERSION
        #region Time Class
        public abstract class Time
        {
            public enum TimeUnit { Seconds, Minuts, Hours }

            /// <summary>
            /// Converting a duration in a selected time unit into an other
            /// </summary>
            /// <param name="time"></param>
            /// <param name="baseUnit"></param>
            /// <param name="targetUnit"></param>
            public static float ConvertTime(float time, TimeUnit baseUnit, TimeUnit targetUnit)
            {
                return GetConvertedTime(GetTimeInSeconds(time, baseUnit), targetUnit);
            }

            /// <summary>
            /// Converting a duration value into seconds
            /// </summary>
            /// <param name="time"> Duration value </param>
            /// <param name="unit"> Duration's time unit </param>
            public static float GetTimeInSeconds(float time, TimeUnit unit)
            {
                switch (unit)
                {
                    case TimeUnit.Minuts:
                        return time * 60f;

                    case TimeUnit.Hours:
                        return time * 3600f;

                    default:
                        return time;
                }
            }

            /// <summary>
            /// Converting a duration in seconds in a target time unit
            /// </summary>
            /// <param name="seconds"> Duration to convert (in seconds) </param>
            /// <param name="unit"> Unit to convert into </param>
            public static float GetConvertedTime(float seconds, TimeUnit unit)
            {
                switch (unit)
                {
                    case TimeUnit.Minuts:
                        return seconds / 60f;

                    case TimeUnit.Hours:
                        return seconds / 3600f;

                    default:
                        return seconds;
                }
            }
        }
        #endregion

        //CUSTOM METHODS ABOUT COLORS
        #region Colors Class
        public abstract class ColorProperties
        {
            public static Color RandomColor(float min, float max)
            {
                float r = Math.RandomValue(min, max);
                float g = Math.RandomValue(min, max);
                float b = Math.RandomValue(min, max);
                return new Color(r, g, b, 1);
            }
        }
        #endregion

        //CUSTOM METHODS ABOUT INPUTS (NEW INPUT SYSTEM)
        #region Inputs Class
        public abstract class Inputs
        {
            /// <summary>
            /// Enable or disable a given input action
            /// </summary>
            /// <param name="inputActions"> Actions to enable/disable </param>
            /// <param name="enable"> Enable or disable </param>
            public static void EnableInputMap(InputAction inputActions, bool enable)
            {
                if (enable)
                    inputActions.Enable();
                else
                    inputActions.Disable();
            }

            /// <summary>
            /// Enable or disable a given input action map
            /// </summary>
            /// <param name="inputMap"> Input map to enable/disable </param>
            /// <param name="enable"> Enable or disable </param>
            public static void EnableInputMap(InputActionMap inputMap, bool enable)
            {
                if (enable)
                    inputMap.Enable();
                else
                    inputMap.Disable();
            }

            /// <summary>
            /// Return the current input binding for a given input action
            /// </summary>
            /// <param name="playerInput">Player input component</param>
            /// <param name="action">InputAction to get binding from</param>
            public static string GetCurrentInputForAction(PlayerInput playerInput, string action)
            {
                InputActionAsset actions = playerInput.actions;
                int binding = actions[action].GetBindingIndex(playerInput.currentControlScheme);
                return actions[action].GetBindingDisplayString(binding);
            }
        }
        #endregion
    }

}

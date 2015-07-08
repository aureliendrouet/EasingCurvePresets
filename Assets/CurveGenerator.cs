using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using LookOrFeel.Animation;

public class CurveGenerator : MonoBehaviour
{
    public AnimationCurve curve;    // for preview

    delegate double EasingFunction(double time,double min,double max,double duration);

    static AnimationCurve GenerateCurve(EasingFunction easingFunction, int resolution)
    {
        var curve = new AnimationCurve();
        for (var i = 0; i < resolution; ++i)
        {
            var time = i / (resolution - 1f);
            var value = (float)easingFunction(time, 0.0, 1.0, 1.0);
            var key = new Keyframe(time, value);
            curve.AddKey(key);
        }
        for (var i = 0; i < resolution; ++i)
        {
            curve.SmoothTangents(i, 0f);
        }
        return curve;
    }

    [MenuItem("Assets/Create/EasingCurves")]
    static void CreateAsset()
    {
        var curvePresetLibraryType = Type.GetType("UnityEditor.CurvePresetLibrary, UnityEditor");
        var library = ScriptableObject.CreateInstance(curvePresetLibraryType);

        addCurve(library, PennerDoubleAnimation.Linear, 2, "Linear");

        addCurve(library, PennerDoubleAnimation.QuadEaseOut, 15, "QuadEaseOut");
        addCurve(library, PennerDoubleAnimation.QuadEaseIn, 15, "QuadEaseIn");
        addCurve(library, PennerDoubleAnimation.QuadEaseInOut, 15, "QuadEaseInOut");
        addCurve(library, PennerDoubleAnimation.QuadEaseOutIn, 15, "QuadEaseOutIn");

        addCurve(library, PennerDoubleAnimation.ExpoEaseOut, 15, "ExpoEaseOut");
        addCurve(library, PennerDoubleAnimation.ExpoEaseIn, 15, "ExpoEaseIn");
        addCurve(library, PennerDoubleAnimation.ExpoEaseInOut, 15, "ExpoEaseInOut");
        addCurve(library, PennerDoubleAnimation.ExpoEaseOutIn, 15, "ExpoEaseOutIn");

        addCurve(library, PennerDoubleAnimation.CubicEaseOut, 15, "CubicEaseOut");
        addCurve(library, PennerDoubleAnimation.CubicEaseIn, 15, "CubicEaseIn");
        addCurve(library, PennerDoubleAnimation.CubicEaseInOut, 15, "CubicEaseInOut");
        addCurve(library, PennerDoubleAnimation.CubicEaseOutIn, 15, "CubicEaseOutIn");

        addCurve(library, PennerDoubleAnimation.QuartEaseOut, 15, "QuartEaseOut");
        addCurve(library, PennerDoubleAnimation.QuartEaseIn, 15, "QuartEaseIn");
        addCurve(library, PennerDoubleAnimation.QuartEaseInOut, 15, "QuartEaseInOut");
        addCurve(library, PennerDoubleAnimation.QuartEaseOutIn, 15, "QuartEaseOutIn");

        addCurve(library, PennerDoubleAnimation.QuintEaseOut, 15, "QuintEaseOut");
        addCurve(library, PennerDoubleAnimation.QuintEaseIn, 15, "QuintEaseIn");
        addCurve(library, PennerDoubleAnimation.QuintEaseInOut, 15, "QuintEaseInOut");
        addCurve(library, PennerDoubleAnimation.QuintEaseOutIn, 15, "QuintEaseOutIn");

        addCurve(library, PennerDoubleAnimation.CircEaseOut, 15, "CircEaseOut");
        addCurve(library, PennerDoubleAnimation.CircEaseIn, 15, "CircEaseIn");
        addCurve(library, PennerDoubleAnimation.CircEaseInOut, 15, "CircEaseInOut");
        addCurve(library, PennerDoubleAnimation.CircEaseOutIn, 15, "CircEaseOutIn");

        addCurve(library, PennerDoubleAnimation.SineEaseOut, 15, "SineEaseOut");
        addCurve(library, PennerDoubleAnimation.SineEaseIn, 15, "SineEaseIn");
        addCurve(library, PennerDoubleAnimation.SineEaseInOut, 15, "SineEaseInOut");
        addCurve(library, PennerDoubleAnimation.SineEaseOutIn, 15, "SineEaseOutIn");

        addCurve(library, PennerDoubleAnimation.ElasticEaseOut, 30, "ElasticEaseOut");
        addCurve(library, PennerDoubleAnimation.ElasticEaseIn, 30, "ElasticEaseIn");
        addCurve(library, PennerDoubleAnimation.ElasticEaseInOut, 30, "ElasticEaseInOut");
        addCurve(library, PennerDoubleAnimation.ElasticEaseOutIn, 30, "SineEaseOutIn");

        addCurve(library, PennerDoubleAnimation.BounceEaseOut, 30, "BounceEaseOut");
        addCurve(library, PennerDoubleAnimation.BounceEaseIn, 30, "BounceEaseIn");
        addCurve(library, PennerDoubleAnimation.BounceEaseInOut, 30, "BounceEaseInOut");
        addCurve(library, PennerDoubleAnimation.BounceEaseOutIn, 30, "SineEaseOutIn");

        addCurve(library, PennerDoubleAnimation.BackEaseOut, 30, "BackEaseOut");
        addCurve(library, PennerDoubleAnimation.BackEaseIn, 30, "BackEaseIn");
        addCurve(library, PennerDoubleAnimation.BackEaseInOut, 30, "BackEaseInOut");
        addCurve(library, PennerDoubleAnimation.BackEaseOutIn, 30, "SineEaseOutIn");

        AssetDatabase.CreateAsset(library, "Assets/Editor/EasingCurves.curves");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void addCurve(object library, EasingFunction easingFunction, int resolution, string name)
    {
        var curvePresetLibraryType = Type.GetType("UnityEditor.CurvePresetLibrary, UnityEditor");
        var addMehtod = curvePresetLibraryType.GetMethod("Add");
        addMehtod.Invoke(library, new object[]
        {
            GenerateCurve(easingFunction, resolution),
            name
        });
    }
}

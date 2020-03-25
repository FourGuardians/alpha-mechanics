#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
 
public class HideIfDisabledDrawer : MaterialPropertyDrawer
{
    protected string[] argValue;
    bool bElementHidden;
 
    public HideIfDisabledDrawer(string name1)
    {
        argValue = new string[] { name1 };
    }
 
    public HideIfDisabledDrawer(string name1, string name2)
    {
        argValue = new string[] { name1, name2 };
    }
 
    public HideIfDisabledDrawer(string name1, string name2, string name3)
    {
        argValue = new string[] { name1, name2, name3 };
    }
 
    public HideIfDisabledDrawer(string name1, string name2, string name3, string name4)
    {
        argValue = new string[] { name1, name2, name3, name4 };
    }
 
    //-------------------------------------------------------------------------------------------------
 
    public override void OnGUI (Rect position, MaterialProperty prop, string label, MaterialEditor editor)
    {
        bElementHidden = false;

        for (int i = 0; i < editor.targets.Length; i++)
        {
            Material mat = editor.targets[i] as Material;

            if(mat != null)
                for (int j = 0; j < argValue.Length; j++)
                    bElementHidden |= !mat.IsKeywordEnabled(argValue[j]);
        }
 
        if (!bElementHidden)
            editor.DefaultShaderProperty(prop, label);
    }

    public override float GetPropertyHeight (MaterialProperty prop, string label, MaterialEditor editor) => 0;
}

#endif
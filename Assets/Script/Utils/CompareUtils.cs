using System;
using UnityEngine;

namespace Utils
{
    public class CompareUtils
    {
        public static bool IsEquals(GameObject go1, GameObject go2)
        {
            bool isNamesEquals = go1.name.Equals(go2.name);
            string go1CloneName = string.Format("{0}(Clone)", go1.name);
            string go2CloneName = string.Format("{0}(Clone)", go2.name);
            bool isCloneNamesEquals = go1CloneName.Equals(go2.name) || go2CloneName.Equals(go1.name);

            return isNamesEquals || isCloneNamesEquals; 
        }
    }
}

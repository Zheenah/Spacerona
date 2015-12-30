using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Source.Utils
{
    public static class Vector3Extensions
    {
     /// <summary>
	/// Returns angle between two vectors depending on the reference vector in 0, 360 range.
	/// </summary>
	/// <param name="vectorOne">Ex: 1,0,0 (Right)</param>
	/// <param name="vectorTwo">Ex: 0,0,1 (Forward)</param>
	/// <param name="normal">Ex: 0,1,0 (Up)</param>
	/// <returns></returns>
	public static float GetAngleBetween360(this Vector3 vectorOne, Vector3 vectorTwo, Vector3 normal)
    {
        // angle in [0,180]
        float angle = Vector3.Angle(vectorOne, vectorTwo);
        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorOne, vectorTwo)));

        // angle in [-179,180]
        float signed_angle = angle * sign;

        // angle in [0,360]
        float angle360 = (signed_angle + 180) % 360;

        return angle360;
    }
}
}

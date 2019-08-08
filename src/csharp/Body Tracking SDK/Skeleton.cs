//-----------------------------------------------------------------------
// <copyright file="Body Tracking SDK\Skeleton.cs" company="Bencin Studios">
//     Author:  James Carroll
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Azure.Kinect.Sensor.BodyTracking
{
    [StructLayout(LayoutKind.Sequential)]
    [Native.NativeReference("k4abt_skeleton_t")]
    public struct Skeleton
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = (int)JointId.Count)]
        public Joint[] Joints;
    }
}

//-----------------------------------------------------------------------
// <copyright file="Body Tracking SDK\Joint.cs" company="Bencin Studios">
//     Author:  James Carroll
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Azure.Kinect.Sensor.BodyTracking
{
    [StructLayout(LayoutKind.Sequential)]
    [Native.NativeReference("k4abt_joint_t")]
    public struct Joint
    {
        public Vector3 Position;
        public Quaternion Orientation;
    }
}

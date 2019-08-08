//-----------------------------------------------------------------------
// <copyright file="Body Tracking SDK\JointId.cs" company="Bencin Studios">
//     Author:  James Carroll
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Microsoft.Azure.Kinect.Sensor.BodyTracking
{
    [Native.NativeReference("k4abt_joint_id_t")]
    public enum JointId
    {
        Pelvis = 0,
        SpineNaval,
        SpineChest,
        Neck,
        ClavicleLeft,
        ShoulderLeft,
        ElbowLeft,
        WristLeft,
        ClavicleRight,
        ShoulderRight,
        ElbowRight,
        WristRight,
        HipLeft,
        KneeLeft,
        AnkleLeft,
        FootLeft,
        HipRight,
        KneeRight,
        AnkleRight,
        FootRight,
        Head,
        Nose,
        EyeLeft,
        EarLeft,
        EyeRight,
        EarRight,
        Count
    }
}
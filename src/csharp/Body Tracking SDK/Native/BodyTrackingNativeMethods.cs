// Copyright (c) Bencin Studios

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Azure.Kinect.Sensor.Native;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Azure.Kinect.Sensor.BodyTracking
{
#pragma warning disable IDE1006 // Naming Styles
    internal static class BodyTrackingNativeMethods
    {
        private const CallingConvention k4aCallingConvention = CallingConvention.Cdecl;

        // These types are used internally by the interop dll for marshaling purposes and are not exposed
        // over the public surface of the managed dll.

        #region Handle Types
        
        public sealed class k4abt_tracker_t : SafeHandleZeroOrMinusOneIsInvalid
        {
            private k4abt_tracker_t() : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                BodyTrackingNativeMethods.k4abt_tracker_destroy(this.handle);
                return true;
            }
        }
        
        public sealed class k4abt_frame_t: SafeHandleZeroOrMinusOneIsInvalid
        {
            private k4abt_frame_t() : base(true)
            {
            }

            protected override bool ReleaseHandle()
            {
                BodyTrackingNativeMethods.k4abt_frame_release(this.handle);
                return true;
            }
        }

        #endregion

        #region Enumerations

        [NativeReference]
        public enum k4abt_joint_id_t
        {
            K4ABT_JOINT_PELVIS = 0,
            K4ABT_JOINT_SPINE_NAVAL,
            K4ABT_JOINT_SPINE_CHEST,
            K4ABT_JOINT_NECK,
            K4ABT_JOINT_CLAVICLE_LEFT,
            K4ABT_JOINT_SHOULDER_LEFT,
            K4ABT_JOINT_ELBOW_LEFT,
            K4ABT_JOINT_WRIST_LEFT,
            K4ABT_JOINT_CLAVICLE_RIGHT,
            K4ABT_JOINT_SHOULDER_RIGHT,
            K4ABT_JOINT_ELBOW_RIGHT,
            K4ABT_JOINT_WRIST_RIGHT,
            K4ABT_JOINT_HIP_LEFT,
            K4ABT_JOINT_KNEE_LEFT,
            K4ABT_JOINT_ANKLE_LEFT,
            K4ABT_JOINT_FOOT_LEFT,
            K4ABT_JOINT_HIP_RIGHT,
            K4ABT_JOINT_KNEE_RIGHT,
            K4ABT_JOINT_ANKLE_RIGHT,
            K4ABT_JOINT_FOOT_RIGHT,
            K4ABT_JOINT_HEAD,
            K4ABT_JOINT_NOSE,
            K4ABT_JOINT_EYE_LEFT,
            K4ABT_JOINT_EAR_LEFT,
            K4ABT_JOINT_EYE_RIGHT,
            K4ABT_JOINT_EAR_RIGHT,
            K4ABT_JOINT_COUNT
        }

        #endregion

        #region Structs
        
        #endregion

        #region Functions

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern NativeMethods.k4a_result_t k4abt_tracker_create(
            Calibration sensorCalibration,
            out k4abt_tracker_t tracker_handle);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern void k4abt_tracker_destroy(IntPtr tracker_handle);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern NativeMethods.k4a_wait_result_t k4abt_tracker_enqueue_capture(
            k4abt_tracker_t tracker_handle,
            NativeMethods.k4a_capture_t sensor_capture_handle,
            int timeout_in_ms);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern NativeMethods.k4a_wait_result_t k4abt_tracker_pop_result(
            k4abt_tracker_t tracker_handle,
            out k4abt_frame_t body_frame_handle,
            int timeout_in_ms);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern void k4abt_tracker_shutdown(k4abt_tracker_t tracker_handle);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern void k4abt_frame_release(IntPtr body_frame_handle);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern void k4abt_frame_reference(IntPtr body_frame_handle);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern uint k4abt_frame_get_num_bodies(k4abt_frame_t body_frame_handle);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern NativeMethods.k4a_result_t k4abt_frame_get_body_skeleton(
            k4abt_frame_t body_frame_handle,
            uint index,
            out Skeleton skeleton);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern uint k4abt_frame_get_body_id(k4abt_frame_t body_frame_handle, uint index);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern uint k4abt_frame_get_timestamp_usec(k4abt_frame_t body_frame_handle);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern NativeMethods.k4a_image_t k4abt_frame_get_body_index_map(k4abt_frame_t body_frame_handle);

        [DllImport("k4abt", CallingConvention = k4aCallingConvention)]
        [NativeReference]
        public static extern NativeMethods.k4a_capture_t k4abt_frame_get_capture(k4abt_frame_t body_frame_handle);

        #endregion
    }
#pragma warning restore IDE1006 // Naming Styles
}

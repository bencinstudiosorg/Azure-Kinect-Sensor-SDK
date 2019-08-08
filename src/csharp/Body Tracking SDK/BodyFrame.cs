using System;

namespace Microsoft.Azure.Kinect.Sensor.BodyTracking
{
    public class BodyFrame : IDisposable
    {
        /// <summary>
        /// The pixel value that indicates the pixel belong to the background in the body id map.
        /// </summary>
        public const byte K4ABT_BODY_INDEX_MAP_BACKGROUND = 255;

        /// <summary>
        /// The invalid body id value
        /// </summary>
        public const uint K4ABT_INVALID_BODY_ID = 0xFFFFFFFF;

        private BodyTrackingNativeMethods.k4abt_frame_t _handle;

        /// <summary>
        /// Gets the number of people in this frame.
        /// </summary>
        public uint NumBodies
        {
            get
            {
                lock (this)
                {
                    if (this.disposedValue)
                    {
                        throw new ObjectDisposedException(nameof(BodyFrame));
                    }

                    return BodyTrackingNativeMethods.k4abt_frame_get_num_bodies(this._handle);
                }
            }
        }

        /// <summary>
        /// Get the body frame timestamp in microseconds
        /// </summary>
        public ulong Timestamp
        {
            get
            {
                lock (this)
                {
                    if (this.disposedValue)
                    {
                        throw new ObjectDisposedException(nameof(BodyFrame));
                    }

                    return BodyTrackingNativeMethods.k4abt_frame_get_timestamp_usec(this._handle);
                }
            }
        }

        internal BodyFrame(BodyTrackingNativeMethods.k4abt_frame_t handle)
        {
            // Hook the native allocator and register this object.
            // .Dispose() will be called on this object when the allocator is shut down.
            Allocator.Singleton.RegisterForDisposal(this);

            this._handle = handle;
        }

        /// <summary>
        /// Get the joint information for a particular person index from this frame
        /// </summary>
        /// <param name="index">The index of the body of which the joint information is queried.</param>
        /// <returns>Returns skeleton data for that person.</returns>
        public Skeleton GetSkeleton(uint index)
        {
            lock (this)
            {
                if (this.disposedValue)
                {
                    throw new ObjectDisposedException(nameof(BodyFrame));
                }

                Skeleton skeleton = default;

                AzureKinectException.ThrowIfNotSuccess(() => BodyTrackingNativeMethods.k4abt_frame_get_body_skeleton(
                    this._handle, index, out skeleton));

                return skeleton;
            }
        }

        /// <summary>
        /// Get the body id for a particular person index from this frame.
        /// </summary>
        /// <param name="index">The index of the body of which the body id information is queried.</param>
        /// <returns>Returns the body id.</returns>
        public uint GetBodyId(uint index)
        {
            lock (this)
            {
                if (this.disposedValue)
                {
                    throw new ObjectDisposedException(nameof(BodyFrame));
                }

                uint id = BodyTrackingNativeMethods.k4abt_frame_get_body_id(_handle, index);

                if (id == K4ABT_INVALID_BODY_ID)
                {
                    throw new AzureKinectException($"No body id found for person index: {index}");
                }

                return id;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    Allocator.Singleton.UnregisterForDisposal(this);

                    this._handle.Close();
                    this._handle = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BodyFrame()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

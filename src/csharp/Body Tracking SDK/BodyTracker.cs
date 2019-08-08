using System;

namespace Microsoft.Azure.Kinect.Sensor.BodyTracking
{
    public class BodyTracker : IDisposable
    {
        private BodyTrackingNativeMethods.k4abt_tracker_t _handle;

        private BodyTracker(BodyTrackingNativeMethods.k4abt_tracker_t handle)
        {
            // Hook the native allocator and register this object.
            // .Dispose() will be called on this object when the allocator is shut down.
            Allocator.Singleton.RegisterForDisposal(this);

            this._handle = handle;
        }

        public static BodyTracker Create(Calibration sensorCalibration)
        {
            BodyTrackingNativeMethods.k4abt_tracker_t handle = default;

            AzureKinectException.ThrowIfNotSuccess(() =>
                BodyTrackingNativeMethods.k4abt_tracker_create(sensorCalibration, out handle));

            return new BodyTracker(handle);
        }

        /// <summary>
        /// Add a k4a sensor capture to the tracker input queue to generate its body tracking result asynchronously.
        /// </summary>
        /// <param name="capture">Capture obtained by the device. It should contain the depth data for
        /// this function to work. Otherwise the function will return failure.</param>
        /// <param name="timeoutInMs">Specifies the time in milliseconds the function should block waiting to add the sensor capture to the tracker
        /// process queue. 0 is a check of the status without blocking. Passing a value of -1 will block
        /// indefinitely until the capture is added to the process queue.</param>
        public void EnqueueCapture(Capture capture, int timeoutInMs = -1)
        {
            lock (this)
            {
                if (this.disposedValue)
                {
                    throw new ObjectDisposedException(nameof(BodyTracker));
                }

                NativeMethods.k4a_wait_result_t result = BodyTrackingNativeMethods.k4abt_tracker_enqueue_capture(
                    this._handle, capture.DangerousGetHandle(), timeoutInMs);
                
                if (result == NativeMethods.k4a_wait_result_t.K4A_WAIT_RESULT_TIMEOUT)
                {
                    throw new TimeoutException("Timed out before adding the capture to the processing queue.");
                }

                AzureKinectException.ThrowIfNotSuccess(() => result);
            }
        }
        
        public BodyFrame PopResult(int timeoutInMS = -1)
        {
            lock (this)
            {
                if (this.disposedValue)
                {
                    throw new ObjectDisposedException(nameof(BodyTracker));
                }

                NativeMethods.k4a_wait_result_t result = BodyTrackingNativeMethods.k4abt_tracker_pop_result(
                    this._handle, out BodyTrackingNativeMethods.k4abt_frame_t frameHandle, timeoutInMS);

                if (result == NativeMethods.k4a_wait_result_t.K4A_WAIT_RESULT_TIMEOUT)
                {
                    throw new TimeoutException("Timed out before removing the body frame from the processing queue.");
                }

                AzureKinectException.ThrowIfNotSuccess(() => result);

                return new BodyFrame(frameHandle);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
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

                this.disposedValue = true;
            }
        }
        
        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //~BodyTracker()
        //{
        //    // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //    this.Dispose(false);
        //}

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

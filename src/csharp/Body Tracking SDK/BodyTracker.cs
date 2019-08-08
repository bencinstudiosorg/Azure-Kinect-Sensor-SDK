using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Kinect.Sensor;

namespace Microsoft.Azure.Kinect.Sensor.BodyTracking
{
    public class BodyTracker : IDisposable
    {
        private BodyTrackingNativeMethods.k4abt_tracker_t _handle;

        private BodyTracker(BodyTrackingNativeMethods.k4abt_tracker_t handle)
        {
            //Allocator
            this._handle = handle;
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
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        ~BodyTracker()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

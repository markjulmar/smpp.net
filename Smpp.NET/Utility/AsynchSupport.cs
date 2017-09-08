using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;

namespace JulMar.Smpp.Utility {
	/// <remarks>
	/// This class is used to change a synchronous method into an asynchronous method.
	/// It wraps the synchronous method into a worker thread and returns an IAsyncResult to
	/// query the state of the call.
	/// </remarks>
	internal class AsynchCall : ISynchronizeInvoke {
		private object state_;
		private AsyncCallback asynchCallback_;

		/// <summary>
		/// Constructor for the asynch call
		/// </summary>
		/// <param name="callBack">Callback method</param>
		/// <param name="asyncState">Object returned for state</param>
		public AsynchCall(AsyncCallback callBack, object asyncState) {
			asynchCallback_ = callBack;
			state_ = asyncState;
		}

		/// <summary>
		/// Gets a value indicating whether the caller must call Invoke when 
		/// calling an object that implements this interface.
		/// </summary>
		/// <value>True/False</value>
		public bool InvokeRequired {
			get { return false; }
		}

		/// <summary>
		/// Takes the delegate to the synchronous method, and queues it up for execution on a 
		/// thread pool. The thread pool calls DoInvoke to execute.
		/// </summary>
		/// <param name="method">Method to call</param>
		/// <param name="args">Arguments for method</param>
		/// <returns>IAsyncResult interface</returns>
		public IAsyncResult BeginInvoke(Delegate method, object[] args) {
			AsynchCallResult result = new AsynchCallResult(method, args, asynchCallback_, state_, this);
			ThreadPool.QueueUserWorkItem(new WaitCallback(result.DoInvoke));
			return (IAsyncResult)result;
		}

		/// <summary>
		/// Gets the value from the synchronous method call by inspecting the return value.
		/// </summary>
		/// <param name="result">Original IAsyncResult interface from BeginInvoke</param>
		/// <returns>Returning object</returns>
		public object EndInvoke(IAsyncResult result) {
			AsynchCallResult asynchResult = (AsynchCallResult)result;
			asynchResult.AsyncWaitHandle.WaitOne();
			return asynchResult.ReturnValue;
		}

		/// <summary>
		/// Invokes the delegate to the synchronous method synchronously by calling Delegate.DynamicInvoke
		/// </summary>
		/// <param name="method">Method to call</param>
		/// <param name="args">Arguments for method</param>
		/// <returns>Returning object</returns>
		public object Invoke(Delegate method, object[] args) {
			return method.DynamicInvoke(args);
		}

		/// <summary>
		/// This returns the result of the asynchronous method.
		/// </summary>
		/// <param name="ar">Returned IAsyncResult from the Invoke</param>
		/// <returns>System.Object result</returns>
		static public object ProcessEndInvoke(IAsyncResult ar) {
			AsynchCallResult acr = (AsynchCallResult)ar;
			return acr.AsynchCall.EndInvoke(ar);
		}

		/// <remarks>
		/// This class is used to implement the IAsyncResult interface returned by the
		/// AsynchCall class.  This provides the status of the asynchronous operation.
		/// </remarks>
		private class AsynchCallResult : IAsyncResult {
			// Class data
			private object[] args_;
			private object state_;
			private bool hasCompleted_ = false;
			private Delegate method_;
			private ManualResetEvent completedEvent_ = new ManualResetEvent(false);
			private object result_;
			private AsyncCallback asynchCallback_;
			private ISynchronizeInvoke asynchCall_ = null;

			/// <summary>
			/// Constructor for the AsynchCallResult object
			/// </summary>
			/// <param name="method">Method being invoked</param>
			/// <param name="args">Arguments for method</param>
			/// <param name="callBack">Callback method for result</param>
			/// <param name="asyncState">Object representing state for the method call</param>
			/// <param name="syncInv">The caller</param>
			internal AsynchCallResult(Delegate method, object[] args, AsyncCallback callBack, object asyncState, ISynchronizeInvoke syncInv) {
				state_ = asyncState;
				method_ = method;
				args_ = args;
				asynchCallback_ = callBack;
				asynchCall_ = syncInv;
			}

			/// <summary>
			/// Gets a user-defined object that qualifies or contains information about an asynchronous operation.
			/// </summary>
			/// <value>object value</value>
			public object AsyncState {
				get { return state_; }
			}

			/// <summary>
			/// Gets a WaitHandle that is used to wait for an asynchronous operation to complete.
			/// </summary>
			/// <value>WaitHandle</value>
			public WaitHandle AsyncWaitHandle {
				get { return completedEvent_; }
			}

			/// <summary>
			/// Gets an indication of whether the asynchronous operation completed synchronously.
			/// </summary>
			/// <value>Always returns false</value>
			public bool CompletedSynchronously {
				get { return false; }
			}

			/// <summary>
			/// Gets an indication whether the asynchronous operation has completed.
			/// </summary>
			/// <value>True/False</value>
			public bool IsCompleted {
				get { return hasCompleted_; }
			}

			/// <summary>
			/// This returns the final result of the call.
			/// </summary>
			/// <value>Object</value>
			public object ReturnValue {
				get { return result_; }
			}

			/// <summary>
			/// This returns the original asynch caller
			/// </summary>
			/// <value>Interface</value>
			public ISynchronizeInvoke AsynchCall {
				get { return asynchCall_; }
			}

			/// <summary>
			/// This is called by the thread pool worker thread and invokes the 
			/// delegate.
			/// </summary>
			/// <param name="state">State passed; not used</param>
			public void DoInvoke(object state) {
				// Get the result
				result_ = method_.DynamicInvoke(args_);

				// Mark us as completed
				hasCompleted_ = true;
				completedEvent_.Set();

				// Callback to the caller
				if (asynchCallback_ != null)
					asynchCallback_(this);

			}
		}
	}
}
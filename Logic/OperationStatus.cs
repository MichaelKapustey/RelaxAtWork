using System;

namespace RelaxAtWork
{
	public class OperationStatus
	{
		public bool Success { get; private set;}

		public Exception Exception { get; private set;}

		public OperationStatus (bool success):this(success,null)
		{			
		}

		public OperationStatus(Exception exception):this(exception==null,exception)
		{
		}

		public OperationStatus(bool success, Exception exception)
		{
			Success = success;
			Exception = exception;
		}

	}

	public class OperationResult<T>:OperationStatus
	{
		public T Result{ get; private set;}

		public OperationResult(T result):base(false)
		{
			Result=result;
		}

		public OperationResult(Exception ex):base(ex)
		{
		}
	}
}


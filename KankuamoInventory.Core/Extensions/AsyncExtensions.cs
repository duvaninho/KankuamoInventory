namespace KankuamoInventory.Core.Extensions
{
	public static class AsyncExtensions
	{
		public static T ToSync<T>(this Task<T> task)
		{
			var result = task.ConfigureAwait(false).GetAwaiter().GetResult();
			return result;
		}

		public static void ToSync(this Task task)
		{
			task.ConfigureAwait(false).GetAwaiter().GetResult();
		}
	}
}

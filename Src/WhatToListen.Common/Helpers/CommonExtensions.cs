namespace WhatToListen.Common.Helpers
{
	public static class CommonExtensions
	{
		public static string Align(this object obj, int left = 0, int right = -5)
		{
			return string.Format($"{{{left.ToString()},{right.ToString()}}}", obj);
		}
	}
}

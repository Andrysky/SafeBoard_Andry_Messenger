using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace WpfApplication1.Interop
{
	public class ManagerDll
	{
		[DllImport(TodoManagerLib)]
		public static extern bool Connect();

		[DllImport(TodoManagerLib)]
		public static extern void Close();

		[DllImport(TodoManagerLib)]
		public static extern int get();

		[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern int plus(int a);

		//[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		//public static extern void CreateItem(ref TodoItem todoItem);

		//[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		//public static extern int GetItems([MarshalAs(UnmanagedType.LPArray)][Out] TodoItem[] todoItems, long itemsLength);

		private const string TodoManagerLib = "../../../Debug/interop_dll.dll";
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace WpfApplication1.Interop
{
	class ConstCharPtrMarshaler : ICustomMarshaler
	{
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return (string)Marshal.PtrToStringAnsi(pNativeData);
			//return Marshal.PtrToStringAnsi(pNativeData);
		}

		public IntPtr MarshalManagedToNative(object ManagedObj)
		{
			return IntPtr.Zero;
		}

		public void CleanUpNativeData(IntPtr pNativeData)
		{
		}

		public void CleanUpManagedData(object ManagedObj)
		{
		}

		public int GetNativeDataSize()
		{
			return IntPtr.Size;
		}

		static readonly ConstCharPtrMarshaler instance = new ConstCharPtrMarshaler();

		public static ICustomMarshaler GetInstance(string cookie)
		{
			return instance;
		}
	}

	public class ManagerDll
	{
		[DllImport(TodoManagerLib)]
		public static extern void Init();

		[DllImport(TodoManagerLib)]
		public static extern bool Connect();

		[DllImport(TodoManagerLib)]
		public static extern void Close();

		[DllImport(TodoManagerLib)]
		public static extern int get();

		[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern int plus(int a);

		[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		//public static extern void Login1([MarshalAs(UnmanagedType.LPStr)] string url, ushort port, [In] string Login, [In, MarshalAs(UnmanagedType.LPStr)] string pass);
		public static extern void Login1([MarshalAs(UnmanagedType.LPStr)] string url, ushort port, [MarshalAs(UnmanagedType.LPStr)] string Login, [MarshalAs(UnmanagedType.LPStr)] string pass);

		[DllImport(TodoManagerLib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		//[return: MarshalAs(UnmanagedType.LPStr)]
		//public static extern string ReceiveMessage1();
		public static extern IntPtr ReceiveMessage();

		[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern void SendMessage([MarshalAs(UnmanagedType.LPStr)] string Name, [MarshalAs(UnmanagedType.LPStr)] string mes);

		[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		public static extern int GetActiveUsers([MarshalAs(UnmanagedType.LPArray)][Out] IntPtr[] array);

		//CharSet = CharSet.Ansi,
		[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		//[return: MarshalAs(UnmanagedType.LPStr)]
		//[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ConstCharPtrMarshaler))]
		//public static extern string ReceiveUserL();
		public static extern IntPtr ReceiveUserL_get();

		[DllImport(TodoManagerLib)]
		public static extern bool ReceiveUserL_all();


		[DllImport(TodoManagerLib)]
		public static extern void ReceiveUserL_start();

		//[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		//public static extern void CreateItem(ref TodoItem todoItem);

		//[DllImport(TodoManagerLib, CallingConvention = CallingConvention.Cdecl)]
		//public static extern int GetItems([MarshalAs(UnmanagedType.LPArray)][Out] TodoItem[] todoItems, long itemsLength);

		private const string TodoManagerLib = "../../../Debug/interop_dll.dll";
	}
}

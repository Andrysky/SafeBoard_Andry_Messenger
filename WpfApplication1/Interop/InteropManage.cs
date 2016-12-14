using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WpfApplication1.Interop;
using System.Runtime.InteropServices;

namespace WpfApplication1.Interop
{
	class InteropManage
	{
		public bool isInitialized;
		public InteropManage()
		{
			isInitialized = true;
			_isDisposed = false;

			ManagerDll.Init();
		}

		public bool Conect()
		{
			return ManagerDll.Connect();
		}

		public void Disconect()
		{
			ManagerDll.Close();
		}

		public void set_server(string addr, int port, string user, string pasword)
		{
			ManagerDll.Login1(addr,(ushort)port,user,pasword);
		}

		public void set_user(string user,string pasword) {

		}

		public List<string> list_user() {
			//ArrayList 
			List<string> users = new List<string>();
			ManagerDll.ReceiveUserL_start();

			while (!ManagerDll.ReceiveUserL_all())
			{
				string temp = Marshal.PtrToStringAnsi(ManagerDll.ReceiveUserL_get());
				users.Add(temp);
			}
			

			return users;
		}

		public string ReceiveMessage() {
			return Marshal.PtrToStringAnsi(ManagerDll.ReceiveMessage());
		}

		public void SendMessage(string name, string mes) {
			ManagerDll.SendMessage(name, mes);
		}

		public void Dispose()
		{
			if (_isDisposed)
				return;

			_isDisposed = true;
			ManagerDll.Close();
		}
		private bool _isDisposed;

	}
}

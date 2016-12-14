using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WpfApplication1.Interop;

namespace WpfApplication1
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			/*

			ManagerDll.Connect();
			ManagerDll.plus(5);
			int a = ManagerDll.get();
			ManagerDll.Close();


			ManagerDll.Login1("127.0.0.1", 666, "Andry", "Andry6");
			string test = Marshal.PtrToStringAnsi(ManagerDll.ReceiveMessage1());

			string test2 = Marshal.PtrToStringAnsi(ManagerDll.ReceiveUserL_get());
			*/
			/*
			var array = new IntPtr[100];
			//array[0] = new IntPtr(23);
			array[1] = new IntPtr(45);
			array[2] = new IntPtr(55);
			var length = array.Length;
			int len = ManagerDll.GetActiveUsers(array);
			string test1 = Marshal.PtrToStringAnsi(array[0]);
			string test2 = Marshal.PtrToStringAnsi(array[1]);
			*/
			InteropManage Client = new InteropManage();
			Client.set_server("127.0.0.1", 0, "Andry", "");
			Client.Conect();


		}
		private ManagerDll _nativeManager;
	}
}

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
		InteropManage Client;
		private void button_Click(object sender, RoutedEventArgs e)
		{
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
			
			Client = new InteropManage();
			//Client.set_server("127.0.0.1", 0, "Andry", "Andry00");
			Client.set_server("192.168.43.83", 0, "Andry", "Andry00");
			Client.Conect();

			//List<string> list = Client.list_user();
			//Client.SendMessage("test", "hi it i");
			//string text = Client.ReceiveMessage();
			
			//string text1 = Client.ReceiveMessage();

		}
		private ManagerDll _nativeManager;

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			List<string> list = Client.list_user();
			listView.ItemsSource = list;
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			string name = (string)listView.SelectedValue;
			string mesege = textBox.Text;
			Client.SendMessage(name, mesege);
		}

		private void button3_Click(object sender, RoutedEventArgs e)
		{
			string text = Client.ReceiveMessage();
			UTF8Encoding utf8;
			byte[] utext = Encoding.ASCII.GetBytes(text);
			string textu = Encoding.Unicode.GetString(utext);
			//utf8.GetString();
			// =  = text;
			textBox1.Text = textu + '\n' + text;

		}

		private void button4_Click(object sender, RoutedEventArgs e)
		{
			string ip = textip.Text;
			string name = textName.Text;
			string pass = passwordBox.Password;
			Client = new InteropManage();

			Client.set_server(ip, 0, name, pass);
			Client.Conect();

		}
	}
}

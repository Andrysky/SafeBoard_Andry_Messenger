#include <string>
using namespace std;

#include <messenger/messenger.h>
#include "client.h"

extern "C"
{
	int temp_test;
	Client *client;
	messenger::UserList list;

	void __declspec(dllexport) Init() {
		client = new Client;
	}

	bool __declspec(dllexport) Connect()
	{
		client->connect();
		temp_test = 0;
		return true;// g_todoManager.Connect();
	}

	void __declspec(dllexport) Close()
	{
		delete client;
		temp_test = 0;
		//g_todoManager.Close();
	}

	void __declspec(dllexport) inc()
	{
		temp_test++;
	}

	void __declspec(dllexport) __cdecl plus(int a)
	{
		temp_test+=a;
	}

	int __declspec(dllexport) get()
	{
		return temp_test;
	}


	void  __declspec(dllexport) Login1(const char* Adress, unsigned short int port, const char* Login, const char* password) {
		/*
		string adr = Adress;
		string log = Login;
		string pass = password;
		string all = adr + log + pass;
		*/
		client->set_server(Adress, port);
		client->set_user(Login, password);
	}

	void  __declspec(dllexport) Disconnect1() {
		int b;
	}

	void  __declspec(dllexport) SendMessage(const char* recpt, const char* text) {
		client->SendMessage(recpt, text);
	}

	__declspec(dllexport) char** GetActiveUsers_old() {

		char** temp_array = new char*[4];
		temp_array[0] = new char[7];
		temp_array[1] = new char[7];
		temp_array[0] = "hi ";
		temp_array[1] = "i am";
		return temp_array;
	}

	__declspec(dllexport) int GetActiveUsers(char** array) {

		char** temp_array = new char*[4];
		temp_array[0] = new char[7];
		temp_array[1] = new char[7];
		temp_array[0] = "hi ";
		temp_array[1] = "i am";
		//array = temp_array;
		array[0] = new char[7];
		array[0] = "hi ";
		return 2;
	}
		 
	__declspec(dllexport) char* ReceiveUserL_get() {
		string user = list.back().identifier;
		list.pop_back();

		char * ret_str = new char[user.size() + 1];
		copy(user.begin(), user.end(), ret_str);
		ret_str[user.size()] = '\0';
		return ret_str;
	}

	__declspec(dllexport) bool ReceiveUserL_all() {
		return (list.size() == 0);
	}

	__declspec(dllexport) void ReceiveUserL_start() {
		list = client->GetActiveUsers();
	}


	__declspec(dllexport) char* ReceiveMessage() {
		string mes = client->ReceiveMessage();
		
		char * ret_str = new char[mes.size() + 1];
		copy(mes.begin(), mes.end(), ret_str);
		ret_str[mes.size()] = '\0';
		return ret_str;

		//string test("hi inter");
		//return test.data();

		char szSampleString[] = "Hello World";
		char* pszReturn = new char[12];

		pszReturn = "Hello Worl";
	
		return pszReturn;
	}

	/*
	void __declspec(dllexport) __cdecl CreateItem(todo_sample::TodoItem* item)
	{
		*item = g_todoManager.CreateItem();
	}

	int __declspec(dllexport) __cdecl GetItems(todo_sample::TodoItem* items, int maxItemsLength)
	{
		auto itemsVector = g_todoManager.GetItems();

		if (itemsVector.size() <= static_cast<size_t>(maxItemsLength))
		{
			memset(items, 0, sizeof(todo_sample::TodoItem) * maxItemsLength);
			memcpy(items, &itemsVector[0], sizeof(todo_sample::TodoItem) * itemsVector.size());
		}

		return itemsVector.size();
	}
	*/
}
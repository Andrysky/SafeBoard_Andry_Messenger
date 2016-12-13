// dllmain.cpp : Defines the entry point for the DLL application.

#include <messenger/messenger.h>

extern "C"
{
	int temp_test;
	bool __declspec(dllexport) Connect()
	{
		temp_test = 0;
		return true;// g_todoManager.Connect();
	}

	void __declspec(dllexport) Close()
	{
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
#pragma once

#include <condition_variable>
#include <mutex>
#include <sstream>
#include <future>
#include <cassert>
#include <iterator>

using namespace std;

#include <messenger/messenger.h>

class Client : public messenger::ILoginCallback, messenger::IMessagesObserver, messenger::IRequestUsersCallback
{
public:
	messenger::MessengerSettings settings;
	string Login;
	string Password;


	//Client(const std::string& jid)
	Client()
		: m_ready(false)
	{
		/*
		messenger::MessengerSettings settings;
		settings.serverUrl = "127.0.0.1";
		m_messenger = messenger::GetMessengerInstance(settings);

		messenger::SecurityPolicy securityPolicy;
		m_messenger->Login((jid + "@localhost").c_str(), "", securityPolicy, this);

		std::unique_lock<std::mutex> lock(m_mutex);
		while (!m_ready)
		{
			m_cv.wait(lock);
		}

		m_messenger->RegisterObserver(this);
		*/
	}

	~Client()
	{
		m_messenger->Disconnect();
	}

	void set_server(string addres, unsigned short port = 0) {
		settings.serverUrl = addres;
		settings.serverPort = port;
	}

	void set_user(string Logi, string pas) {
		Login = Logi+ "@localhost";
		Password = pas;
	}

	void connect() {
		m_messenger = messenger::GetMessengerInstance(settings);
		messenger::SecurityPolicy securityPolicy;
		//m_messenger->Login((jid + "@localhost").c_str(), "", securityPolicy, this);
		m_messenger->Login(Login.c_str(), Password, securityPolicy, this);


		std::unique_lock<std::mutex> lock(m_mutex);
		while (!m_ready)
		{
			m_cv.wait(lock);
		}

		m_messenger->RegisterObserver(this);
	}

	void SendMessage(const std::string& recpt, const std::string& text)
	{
		messenger::MessageContent msg;
		msg.type = messenger::message_content_type::Text;
		std::copy(text.begin(), text.end(), std::back_inserter(msg.data));
		m_messenger->SendMessage(recpt + "@localhost", msg);
	}

	std::string ReceiveMessage()
	{
		std::unique_lock<std::mutex> lock(m_mutex);
		while (m_receivedMsg.empty())
		{
			m_cv.wait(lock);
		}

		std::string res = m_receivedMsg;
		m_receivedMsg.clear();
		return res;
	}

	messenger::UserList GetActiveUsers()
	{
		m_messenger->RequestActiveUsers(this);
		return m_userList.get_future().get();
	}

	void OnOperationResult(messenger::operation_result::Type result) override
	{
		std::unique_lock<std::mutex> lock(m_mutex);
		m_ready = true;
		m_cv.notify_all();
	}

	void OnMessageStatusChanged(const messenger::MessageId& msgId, messenger::message_status::Type status) override
	{
		std::unique_lock<std::mutex> lock(m_mutex);
		m_receivedMsg = "<error>";
		m_cv.notify_all();
	}

	void OnMessageReceived(const messenger::UserId& senderId, const messenger::Message& msg) override
	{
		std::unique_lock<std::mutex> lock(m_mutex);
		m_receivedMsg.assign(reinterpret_cast<const char*>(&msg.content.data[0]), msg.content.data.size());
		m_cv.notify_all();
	}

	void OnOperationResult(messenger::operation_result::Type result, const messenger::UserList& users) override
	{
		m_userList.set_value(users);
	}

private:
	std::shared_ptr<messenger::IMessenger> m_messenger;
	std::mutex m_mutex;
	std::condition_variable m_cv;
	bool m_ready;
	std::string m_receivedMsg;

	std::promise<messenger::UserList> m_userList;
};

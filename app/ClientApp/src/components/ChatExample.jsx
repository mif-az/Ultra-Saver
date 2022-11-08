/* eslint-disable jsx-a11y/no-static-element-interactions */
/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable no-console */
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { React, useContext, useEffect, useState } from 'react';
import { authApi, UserContext } from '../contexts/UserProvider';
import URL from '../appUrl';

export default function ChatExample() {
  const [user] = useContext(UserContext);
  const [connection, setConnection] = useState(null);
  const [input, setInput] = useState(null);
  const [msgStatus, setMsgStatus] = useState({ sending: false });
  const [messages, setMessages] = useState([]);

  useEffect(() => {
    const fn = async () => {
      const msgs = await authApi(user).get(`${URL}/chat/messages`);
      setMessages(await msgs.json());
    };
    const connectToChat = async () => {
      if (connection !== null) return;
      try {
        const conn = new HubConnectionBuilder()
          .withUrl(`${URL}/chat/send`, { accessTokenFactory: () => user.jwt })
          .configureLogging(LogLevel.Information)
          .build();

        conn.on('msg', (msg) => {
          setMessages((current) => [...current, msg]);
        });

        conn.on('msg_removed', (id) => {
          setMessages((current) => current.filter((el) => el.id !== id));
        });

        await conn.start();
        setConnection(conn);
      } catch (error) {
        // eslint-disable-next-line no-console
        console.error(error);
      }
    };
    fn();
    connectToChat();
  }, []);

  const sendMessage = async (msg) => {
    setMsgStatus({ ...msgStatus, sending: true, sent: false });
    await connection.invoke('sendMessage', msg);
    setMsgStatus({ ...msgStatus, sending: false, sent: true });
  };

  const removeMessage = async (id) => {
    await connection.invoke('removeMessage', id);
  };

  return (
    <>
      <div>
        <textarea
          type="text"
          className="box-shadow shadow-lg rounded-2 border-1 p-2 mx-3"
          onChange={(e) => setInput(e.target.value)}
        />
        <button type="button" className="btn btn-primary" onClick={() => sendMessage(input)}>
          Send
        </button>
      </div>
      <div className="h-50 w-50 overflow-auto p-5 position-absolute">
        {messages.map((message) => (
          <div className="bg-dark rounded-1 p-1 my-2 position-relative" key={message.id}>
            <p className="text-info"> {message.user} </p>
            <p className="text-white"> {message.message} </p>
            <span
              className="bg-danger rounded-circle position-absolute top-0 end-0 m-2 text-center"
              style={{
                width: '1.5rem',
                height: '1.5rem',
                cursor: 'pointer',
                display: message.email === user.email ? 'block' : 'none'
              }}
              onClick={() => removeMessage(message.id)}>
              x
            </span>
          </div>
        ))}
      </div>
    </>
  );
}

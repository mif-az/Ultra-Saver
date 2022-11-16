/* eslint-disable no-param-reassign */
/* eslint-disable jsx-a11y/no-static-element-interactions */
/* eslint-disable jsx-a11y/click-events-have-key-events */
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
  // const [time, setTime] = useState();
  // const date = new Date();

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

  const handleSendMessage = (e) => {
    if (input !== '') {
      sendMessage(input);
      e.target.value = '';
    }
  };

  const handleSendKeypress = (e) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      handleSendMessage(e);
    }
  };

  // const handleSendTime = () => {
  //   setTime(date);
  // };

  const removeMessage = async (id) => {
    await connection.invoke('removeMessage', id);
  };

  return (
    <>
      <div className="containter">
        <div className="row fixed-bottom justify-content-center p-3 bg-white">
          <textarea
            type="text"
            className="col-4 rounded-2 border-1 mx-2 p-2"
            onChange={(e) => setInput(e.target.value)}
            onKeyPress={handleSendKeypress}
          />
          <button
            type="button"
            className="btn btn-primary col-1"
            onClick={(e) => handleSendMessage(e)}>
            Send
          </button>
        </div>
      </div>
      <div className="containter">
        <div
          className="w-50 position-absolute top-50 start-50 translate-middle overflow-auto"
          style={{ height: '75%' }}>
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
      </div>
    </>
  );
}

/* eslint-disable no-alert */
import React, { useState } from 'react';
import { Input } from 'reactstrap';
import MesageProps from './MessageProps';
import './MessageUi.css';

export default function MessageUi() {
  const [message, setMessage] = useState('');
  const [recieveMessage, setRecieveMessage] = useState('');

  const handleSendMessage = (e) => {
    if (message != null) {
      setRecieveMessage(message);
      e.target.value = '';
    }
  };
  const handleKeypress = (e) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      handleSendMessage(e);
    }
  };

  return (
    <div className="MessageUi">
      <div>
        <Input
          type="text"
          placeHolder="text"
          onChange={(e) => setMessage(e.target.value)}
          onKeyPress={handleKeypress}
          autoFocus
        />
        <button type="button" onClick={handleSendMessage}>
          Send
        </button>
      </div>
      <MesageProps text={recieveMessage} />
    </div>
  );
}

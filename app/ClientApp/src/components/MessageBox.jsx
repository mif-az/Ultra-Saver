import React from 'react';

document.getElementById('SendButton').onclick = function () {
  let MessageText = document.getElementById('MessageText').value;
  console.log(MessageText);
};

export default function MessageBox() {
  return (
    <div>
      <input type="text" id="MessageText" />
      <button type="button" id="SendButton">
        Sent
      </button>
    </div>
  );
}

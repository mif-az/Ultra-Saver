/* eslint-disable react/prop-types */
import React, { useContext } from 'react';
import { UserContext } from '../contexts/UserProvider';

export default function MesageProps(props) {
  const [user] = useContext(UserContext);
  const { text } = props;
  const date = new Date();
  return (
    <div>
      <h3 id="NAME"> {user.name} </h3>
      <p id="DATE">
        Date: {date.getDate()}-{date.getMonth()}
        <br />
        Time: {date.getHours()}:{date.getMinutes()}
      </p>
      <p> {text} </p>
    </div>
  );
}

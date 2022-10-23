import React, { useContext } from 'react';
import { UserContext } from '../contexts/UserProvider';

export default function Welcome() {
  const [user] = useContext(UserContext);

  return Object.keys(user).length < 1 ? (
    ''
  ) : (
    <p className=" align-baseline my-auto text-primary">
      Welcome, <b>{user.name}</b>
    </p>
  );
}

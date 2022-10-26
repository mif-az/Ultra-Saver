import React, { useContext } from 'react';
import LoggedOutHome from './LoggedOutHome';
import LoggedInHome from './LoggedInHome';
import { UserContext } from '../../contexts/UserProvider';

export default function Home() {
  const [user] = useContext(UserContext);

  return Object.keys(user).length < 1 ? <LoggedOutHome /> : <LoggedInHome />;
}

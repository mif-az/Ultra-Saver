import React, { useContext } from 'react';
import { LanguageContext } from '../contexts/LanguageProvider';
import { UserContext } from '../contexts/UserProvider';
import all from './Texts/all';

export default function Welcome() {
  const [user] = useContext(UserContext);
  const [lang] = useContext(LanguageContext);

  return Object.keys(user).length < 1 ? (
    ''
  ) : (
    <p className=" align-baseline my-auto text-primary">
      {all.all_navbar_welcome[lang]}
      <b>{user.name}</b>
    </p>
  );
}

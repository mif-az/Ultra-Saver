import React, { useContext } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { UserContext } from '../contexts/UserProvider';
import { LanguageContext } from '../contexts/LanguageProvider';
import all from './Texts/all';

export default function Chat() {
  const [user] = useContext(UserContext);
  const [lang] = useContext(LanguageContext);
  return Object.keys(user).length < 1 ? (
    ''
  ) : (
    <NavItem>
      <NavLink tag={Link} className="text-dark" to="/communitychat">
        {all.all_navbar_chat[lang]}
      </NavLink>
    </NavItem>
  );
}

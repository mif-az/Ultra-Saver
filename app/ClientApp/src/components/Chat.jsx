import React, { useContext } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { UserContext } from '../contexts/UserProvider';

export default function Chat() {
  const [user] = useContext(UserContext);
  return Object.keys(user).length < 1 ? (
    ''
  ) : (
    <NavItem>
      <NavLink tag={Link} className="text-dark" to="/messageui">
        Chat
      </NavLink>
    </NavItem>
  );
}

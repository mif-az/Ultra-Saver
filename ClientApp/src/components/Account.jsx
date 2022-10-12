import React, { useContext } from 'react';
import {
  Button,
  NavItem,
  NavLink,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { UserContext, RemoveJwtToken } from '../contexts/UserProvider';

export default function Welcome() {
  const [user, setUser] = useContext(UserContext);

  const HandleLogOut = () => {
    RemoveJwtToken(setUser);
  };

  return Object.keys(user).length < 1 ? (
    <NavItem>
      <NavLink tag={Link} className="text-dark" to="/login">
        Login
      </NavLink>
    </NavItem>
  ) : (
    <UncontrolledDropdown nav inNavbar>
      <DropdownToggle nav caret className="text-dark">
        Account
      </DropdownToggle>
      <DropdownMenu end>
        <DropdownItem tag={Link} to="/accountsettings" className="text-dark">
          Account Settings
        </DropdownItem>
        <Button outline tag={Link} to="/" onClick={HandleLogOut}>
          Sign Out
        </Button>
      </DropdownMenu>
    </UncontrolledDropdown>
  );
}

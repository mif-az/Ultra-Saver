import React, { useContext } from 'react';
import {
  NavItem,
  NavLink,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  UncontrolledDropdown
} from 'reactstrap';
import { Link } from 'react-router-dom';
import { UserContext, RemoveJwtToken } from '../contexts/UserProvider';
import all from './Texts/all';
import { LanguageContext } from '../contexts/LanguageProvider';

export default function Welcome() {
  const [user, setUser] = useContext(UserContext);
  const [lang] = useContext(LanguageContext);

  const HandleLogOut = () => {
    RemoveJwtToken(setUser);
  };

  const imgStyle = {
    maxHeight: 32,
    maxWidth: 32
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
        <img
          src={user.picture}
          className="rounded-circle img-fluid"
          alt="Responsive"
          style={imgStyle}
        />
      </DropdownToggle>
      <DropdownMenu end>
        <DropdownItem tag={Link} to="/accountsettings" className="text-dark">
          {all.all_navbar_acc_settings[lang]}
        </DropdownItem>
        <div className="d-flex justify-content-center">
          <DropdownItem tag={Link} to="/" onClick={HandleLogOut}>
            {all.all_navbar_signout[lang]}
          </DropdownItem>
        </div>
      </DropdownMenu>
    </UncontrolledDropdown>
  );
}

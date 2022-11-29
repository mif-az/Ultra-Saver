import React, { useContext } from 'react';
import { DropdownToggle, DropdownMenu, DropdownItem, UncontrolledDropdown } from 'reactstrap';
import { Link } from 'react-router-dom';
import { UserContext } from '../contexts/UserProvider';
import { LanguageContext } from '../contexts/LanguageProvider';
import all from './Texts/all';

export default function Welcome() {
  const [user] = useContext(UserContext);
  const [lang] = useContext(LanguageContext);

  return Object.keys(user).length < 1 ? (
    ''
  ) : (
    <UncontrolledDropdown nav inNavbar>
      <DropdownToggle nav caret className="text-dark">
        {all.all_navbar_recipes[lang]}
      </DropdownToggle>
      <DropdownMenu end>
        <DropdownItem tag={Link} to="/sharerecipe" className="text-dark">
          {all.all_navbar_recipes_share[lang]}
        </DropdownItem>
        <DropdownItem tag={Link} to="/searchrecipe" className="text-dark">
          {all.all_navbar_recipes_search[lang]}
        </DropdownItem>
      </DropdownMenu>
    </UncontrolledDropdown>
  );
}

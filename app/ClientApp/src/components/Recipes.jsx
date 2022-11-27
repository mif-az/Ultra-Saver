import React, { useContext } from 'react';
import { DropdownToggle, DropdownMenu, DropdownItem, UncontrolledDropdown } from 'reactstrap';
import { Link } from 'react-router-dom';
import { UserContext } from '../contexts/UserProvider';

export default function Welcome() {
  const [user] = useContext(UserContext);

  return Object.keys(user).length < 1 ? (
    ''
  ) : (
    <UncontrolledDropdown nav inNavbar>
      <DropdownToggle nav caret className="text-dark">
        Recipes
      </DropdownToggle>
      <DropdownMenu end>
        <DropdownItem tag={Link} to="/sharerecipe" className="text-dark">
          Share Recipe
        </DropdownItem>
        <DropdownItem tag={Link} to="/searchrecipe" className="text-dark">
          Search Recipes
        </DropdownItem>
        <DropdownItem tag={Link} to="/likedrecipes" className="text-dark">
          Liked Recipes
        </DropdownItem>
      </DropdownMenu>
    </UncontrolledDropdown>
  );
}

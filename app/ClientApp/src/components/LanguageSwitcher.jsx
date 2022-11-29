import React, { useContext } from 'react';
import { DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown } from 'reactstrap';
import { LanguageContext } from '../contexts/LanguageProvider';

export default function LanguageSwitcher() {
  const [lang, setLang] = useContext(LanguageContext);
  return (
    <UncontrolledDropdown nav inNavbar>
      <DropdownToggle nav caret className="text-dark">
        {lang.toUpperCase()}
      </DropdownToggle>
      <DropdownMenu end>
        <DropdownItem onClick={() => setLang('en')}>EN</DropdownItem>
        <DropdownItem onClick={() => setLang('lt')}>LT</DropdownItem>
      </DropdownMenu>
    </UncontrolledDropdown>
  );
}

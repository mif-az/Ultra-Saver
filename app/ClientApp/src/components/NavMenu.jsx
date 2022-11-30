/* eslint-disable react/destructuring-assignment */
import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import Welcome from './Welcome';
import Account from './Account';
import Recipes from './Recipes';
import Chat from './Chat';
import LanguageSwitcher from './LanguageSwitcher';
import { LanguageContext } from '../contexts/LanguageProvider';
import all from './Texts/all';

export default class NavMenu extends Component {
  // eslint-disable-next-line react/static-property-placement
  static displayName = NavMenu.name;

  // eslint-disable-next-line react/static-property-placement
  static contextType = LanguageContext;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    // this.toggleDropdown = this.toggleDropdown.bind(this);
    this.state = {
      collapsed: true
      // dropCollapsed: true
    };
  }

  toggleNavbar() {
    this.setState({
      // eslint-disable-next-line react/no-access-state-in-setstate
      collapsed: !this.state.collapsed
    });
  }

  render() {
    const [lang] = this.context;
    return (
      <header>
        <Navbar
          className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
          container
          light>
          <NavbarBrand className="fw-bold" tag={Link} to="/">
            Ultra_Saver
          </NavbarBrand>
          <Welcome />
          <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
          <Collapse
            className="d-sm-inline-flex flex-sm-row-reverse"
            isOpen={!this.state.collapsed}
            navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/user/properties">
                  {all.all_navbar_properties[lang]}
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/energycalculation">
                  {all.all_navbar_energy_cost[lang]}
                </NavLink>
              </NavItem>
              <Chat />
              <Recipes />
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/countdowntimer">
                  Timer
                </NavLink>
              </NavItem>
              <Account />
              <LanguageSwitcher />
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}

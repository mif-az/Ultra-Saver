/* eslint-disable react/destructuring-assignment */
import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import Welcome from './Welcome';
import Account from './Account';
import Recipes from './Recipes';
import Chat from './Chat';

export default class NavMenu extends Component {
  // eslint-disable-next-line react/static-property-placement
  static displayName = NavMenu.name;

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
                  Properties
                </NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="text-dark" to="/energycalculation">
                  Energy Cost
                </NavLink>
              </NavItem>
              <Chat />
              <Recipes />
              <Account />
            </ul>
          </Collapse>
        </Navbar>
      </header>
    );
  }
}

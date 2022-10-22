/* eslint-disable react/prop-types */
/* eslint-disable react/destructuring-assignment */
/* eslint-disable import/prefer-default-export */
import React, { Component } from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';

// eslint-disable-next-line react/prefer-stateless-function
export class Layout extends Component {
  // eslint-disable-next-line react/static-property-placement
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <Container>{this.props.children}</Container>
      </div>
    );
  }
}

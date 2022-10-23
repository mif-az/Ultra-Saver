/* eslint-disable react/prefer-stateless-function */
import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import { UserProvider } from './contexts/UserProvider';
import './custom.css';

export default class App extends Component {
  // eslint-disable-next-line react/static-property-placement
  static displayName = App.name;

  render() {
    return (
      // We wrap everything with UserProvider because UserProvider is what provides the global user state
      <UserProvider>
        <Layout>
          <Routes>
            {AppRoutes.map((route, index) => {
              const { element, ...rest } = route;
              // eslint-disable-next-line react/no-array-index-key, react/jsx-props-no-spreading
              return <Route key={index} {...rest} element={element} />;
            })}
          </Routes>
        </Layout>
      </UserProvider>
    );
  }
}

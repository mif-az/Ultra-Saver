/* eslint-disable react/jsx-no-constructed-context-values */
/* eslint-disable no-shadow */
/* eslint-disable react/prop-types */
import React from 'react';
import Cookies from 'universal-cookie';
// eslint-disable-next-line camelcase
import jwt_decode from 'jwt-decode';

const cookies = new Cookies(); // We save login data in cookies for it to be persistent in browser

let token = cookies.get('jwt');

const staticStateAdditions = (obj) => {
  // If the user is logged in - we always want these to be available
  if (!obj || Object.keys(obj).length < 1) return obj;
  return { ...obj, header: `Bearer ${token}`, jwt: token }; // header - Authorization header for restricted access API; jwt - token given by Google
};

const initialState = staticStateAdditions(token ? jwt_decode(token) : {});

export const UpdateJwtToken = (jwt, _setUser) => {
  // This gets called when we log in. The function saves the JWT token in cookies and sets global user state
  token = jwt;
  const decoded = jwt_decode(jwt);
  cookies.set('jwt', jwt, { path: '/', expires: new Date(decoded.exp * 1000) });
  _setUser(decoded);
};

export const RemoveJwtToken = (_setUser) => {
  // Remove JWT from cookies and reset global user state
  cookies.remove('jwt');
  token = undefined;
  _setUser({});
};

export const UserContext = React.createContext({
  // Context allows for global state
  state: initialState,
  dispath: () => null
});

export function UserProvider({ children }) {
  // useReducer is similar to useState but works a bit differently. It is only needed for the global state
  const [state, dispath] = React.useReducer(
    (state, action) => staticStateAdditions(action),
    initialState
  );
  // All components in {children} will be able to access UserContext and therefore the global user state
  return <UserContext.Provider value={[state, dispath]}>{children}</UserContext.Provider>;
}

// Helpers for doing API calls

export const authApi = (user) => ({
  get: (url) =>
    fetch(url, {
      method: 'GET',
      headers: {
        // eslint-disable-next-line no-underscore-dangle
        Authorization: user.header
      }
    }),
  post: (url, body, method = 'POST') =>
    fetch(url, {
      method,
      headers: {
        // eslint-disable-next-line no-underscore-dangle
        Authorization: user.header,
        'Content-type': 'application/json'
      },
      body
    })
});

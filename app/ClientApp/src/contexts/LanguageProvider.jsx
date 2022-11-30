/* eslint-disable react/prop-types */
/* eslint-disable react/jsx-no-constructed-context-values */
import React from 'react';
import Cookies from 'universal-cookie';

const cookies = new Cookies();

export function LanguageProvider({ children }) {
  const [state, dispath] = React.useReducer((_, action) => {
    cookies.set('lang', action);
    return action;
  }, cookies.get('lang') || 'en');

  return <LanguageContext.Provider value={[state, dispath]}>{children}</LanguageContext.Provider>;
}

export const LanguageContext = React.createContext({
  // Context allows for global state
  state: cookies.get('lang') || 'en',
  dispath: () => null
});

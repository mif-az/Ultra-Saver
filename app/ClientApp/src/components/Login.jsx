import React, { useContext, useEffect } from 'react';

// eslint-disable-next-line no-unused-vars
import { RemoveJwtToken, UpdateJwtToken, UserContext, authApi } from '../contexts/UserProvider';

function Login() {
  const [user, setUser] = useContext(UserContext); // This is the global user state

  function HandleCallback(response) {
    // function called when user successfully logs in
    UpdateJwtToken(response.credential, setUser); // Adds token to cookies and sets user login data
  }

  useEffect(() => {
    /* global google */
    google.accounts.id.initialize({
      client_id: '929531042172-l5h1hbegcb3qm6nkpg1r7m4aa6seb98n.apps.googleusercontent.com',
      callback: HandleCallback
    });

    google.accounts.id.renderButton(document.getElementById('signInDiv'), {
      theme: 'outline',
      size: 'Large'
    });
  }, []);

  function HandleLogOut() {
    RemoveJwtToken(setUser); // deletes user data
  }

  return (
    <div>
      <h1 id="tabelLabel">Login</h1>
      <p>This component demonstrates the log in through google functionality.</p>
      <div id="signInDiv" style={{ display: Object.keys(user).length > 0 ? 'none' : 'block' }} />
      {Object.keys(user ?? {}).length !== 0 && ( // This div only shows up when user data is registered
        <div>
          <p>Signed in as: {user.name}</p>
          <p>Email: {user.email}</p>
          <button type="button" onClick={(e) => HandleLogOut(e)}>
            Sign Out
          </button>
        </div>
      )}
    </div>
  );
}

export default Login;

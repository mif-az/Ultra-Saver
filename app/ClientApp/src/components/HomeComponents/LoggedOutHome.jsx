import React, { useContext, useEffect, useState } from 'react';
import URL from '../../appUrl';
import { UpdateJwtToken, UserContext } from '../../contexts/UserProvider';

export default function LoggedOutHome() {
  // eslint-disable-next-line no-unused-vars
  const [user, setUser] = useContext(UserContext); // This is the global user state
  const [info, setInfo] = useState([]);

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
      size: 'large',
      theme: 'outline',
      shape: 'pill',
      text: 'continue_with'
    });
    const getInfo = async () => {
      const response = await fetch(`${URL}/recipe/stats`);
      const data = await response.json();
      setInfo(data);
    };
    getInfo();
  }, []);

  return (
    <div className="position-absolute top-50 start-50 translate-middle">
      <div className="d-grid gap-3">
        <div className="fw-bold fs-1 d-flex justify-content-center">ULTRA-SAVER</div>
        <div className="fs-5 text-center">Your gateway to nutritious and inexpensive dining</div>
        <p className="text-center" style={info.length < 1 ? { display: 'none' } : undefined}>
          Over {info.count} recipes that on average take ~{info.minutes} minutes to make and use ~
          {info.wattage?.toFixed(3)} Watts are waiting for you!
        </p>
        <div className="d-flex justify-content-center">
          <div id="signInDiv" style={{ display: 'block' }} />
        </div>
      </div>
    </div>
  );
}

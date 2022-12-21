/* eslint-disable react/prop-types */
import React, { useContext, useEffect, useState } from 'react';
import { authApi, UserContext } from '../contexts/UserProvider';

export default function UserProps() {
  const [user] = useContext(UserContext);
  const [props, setProps] = useState({});
  const [tableState, setTableState] = useState({ loading: true, error: false });

  async function populateData() {
    const data = await authApi(user).get(`/userinfo`);

    if (data.status !== 200) {
      setTableState({ ...tableState, error: true });
    } else {
      const json = await data.json();
      setProps({ ...json, original: true });
      setTableState({ ...tableState, loading: false });
    }
  }

  useEffect(() => {
    populateData();
  }, []);

  if (tableState.error) return <h3 style={{ color: 'red' }}>An error has occured...</h3>;

  return tableState.loading ? (
    <p>Loading...</p>
  ) : (
    <>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>email</th>
            <th>Dark Mode</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>{props.email}</td>
            <td>
              <input
                checked={props.darkMode}
                type="checkbox"
                className="form-check w-25"
                onChange={() => {}}
                onClick={() => {
                  setProps({ ...props, darkMode: !props.darkMode, original: false });
                }}
              />
            </td>
          </tr>
        </tbody>
      </table>
      <button
        type="button"
        className="btn btn-primary"
        hidden={props.original}
        onClick={async () => {
          await authApi(user).post(`/userinfo`, JSON.stringify(props));
          setProps({ ...props, original: true });
        }}>
        Save changes
      </button>
    </>
  );
}

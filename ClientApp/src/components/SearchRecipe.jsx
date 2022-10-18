import React, { useContext, useState } from 'react';
import * as reactstrap from 'reactstrap';
import { authApi, UserContext } from '../contexts/UserProvider';

export default function SearchRecipe() {
  const [recipes, setRecipes] = useState([]);
  const [user] = useContext(UserContext);

  async function fetchData(q) {
    const response = await authApi(user).get(`recipe?filter=${q}`);
    const data = await response.json();
    console.log('data :>> ', data);
    setRecipes(data);
  }

  return (
    <>
      <reactstrap.Input
        type="text"
        onChange={(e) => {
          fetchData(e.target.value);
        }}
      />
      <div className="form-check border-1 border-danger">
        {recipes.map((el) => (
          <div className="border-2 mt-2 bg-dark text-white rounded-1 fw-bold p-3" key={el.id}>
            {el.name}
          </div>
        ))}
      </div>
    </>
  );
}

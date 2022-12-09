import React, { useEffect, useContext, useState } from 'react';
import { useParams } from 'react-router-dom';
import { authApi, UserContext } from '../contexts/UserProvider';
import URL from '../appUrl';

export default function Recipe() {
  const { recipeId } = useParams();
  const [user] = useContext(UserContext);
  const [recipe, setRecipe] = useState('');

  useEffect(() => {
    const initialFetchData = async () => {
      const response = await authApi(user).get(`${URL}/recipe/${recipeId}`);
      const data = await response.json();
      console.log(data);
      setRecipe(data);
    };
    initialFetchData();
  }, []);

  return (
    <div className="container">
      <div className="row">
        <div className="col-8">
          <div className="fw-bold fs-1 text-capitalize">{recipe.name}</div>
          <div className="fontweight-light fs-3">Will cost about ~</div>
          <div className="fontweight-light fs-3">
            Will take ~{recipe.fullPrepTime} minutes to make
          </div>
          <div className="fontweight-light fs-3">Consists of {recipe.calorieCount} calories</div>
        </div>
        <div className="col-4">
          <img
            className="img-thumbnail"
            style={{ height: 200 }}
            src={`data:image/jpeg;base64,${recipe.imageData}`}
            alt=""
          />
        </div>
      </div>
      <div className="row mt-5">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut
        labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
        laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in
        voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat
        cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
      </div>
    </div>
  );
}

import React, { useEffect, useContext, useState } from 'react';
import { useParams } from 'react-router-dom';
import { authApi, UserContext } from '../contexts/UserProvider';
import URL from '../appUrl';

export default function Recipe() {
  const { recipeId } = useParams();
  const [user] = useContext(UserContext);
  const [recipe, setRecipe] = useState('');

  function Instructions() {
    const text = recipe.instruction;
    return text.split('\n').map((str) => <p className="fs-5">{str}</p>);
  }

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
      <Instructions />
    </div>
  );
}

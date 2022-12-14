import React, { useEffect, useContext, useState } from 'react';
import { useParams } from 'react-router-dom';
import { authApi, UserContext } from '../contexts/UserProvider';
import URL from '../appUrl';

export default function Recipe() {
  const { recipeId } = useParams();
  const [user] = useContext(UserContext);
  const [recipe, setRecipe] = useState('');
  const [recipeIngredients, setRecipeIngredients] = useState([]);

  function Instructions() {
    if (recipe.instruction !== undefined) {
      const text = recipe.instruction;
      return text.split('\n').map((str) => <p className="fs-5">{str}</p>);
    }
    return '';
  }

  useEffect(() => {
    const initialFetchData = async () => {
      let response = await authApi(user).get(`${URL}/recipe/${recipeId}`);
      let data = await response.json();
      console.log(data);
      setRecipe(data);

      response = await authApi(user).get(`${URL}/recipeingredient/${recipeId}`);
      data = await response.json();
      console.log(data);
      setRecipeIngredients(data);
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
      <ul>
        {recipeIngredients.map((el) => (
          <li>
            <div className="row fs-4" key={el.id}>
              <p>
                {el.ingredientAmount} of {el.ingredientName}
              </p>
            </div>
          </li>
        ))}
      </ul>
      <Instructions />
    </div>
  );
}

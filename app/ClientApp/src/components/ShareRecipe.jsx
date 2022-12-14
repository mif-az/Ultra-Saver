import React, { useState, useContext, useEffect } from 'react';
import {
  Button,
  Form,
  FormGroup,
  FormFeedback,
  Input,
  Label,
  Row,
  Col,
  FormText
} from 'reactstrap';
import { LanguageContext } from '../contexts/LanguageProvider';

import { authApi, UserContext } from '../contexts/UserProvider';
import all from './Texts/all';

export default function ShareRecipe() {
  const [recipeTitle, setRecipeTitle] = useState('');
  const [description, setDescription] = useState('');
  const [ingredients, setIngredients] = useState([
    {
      ingredientName: '',
      ingredientAmount: '',
      ingredientPreparationMethod: 'None of the above',
      ingredientPreparation: ''
    }
  ]);
  const [instructions, setInstructions] = useState('');
  const [inputValidity, setInputValidity] = useState(true);
  const [imageData, setImageData] = useState();
  const [user] = useContext(UserContext);
  const [lang] = useContext(LanguageContext);

  const isNumber = (input) => !Number.isNaN(+input); // isNaN returns true if the input is NOT a number, so we have to negate
  const isEmptyString = (str) => str.length === 0;

  const isInputValid = () => {
    if (isEmptyString(recipeTitle)) {
      // check if it isn't already false to prevent infinite re-rendering
      if (inputValidity !== false) setInputValidity(false);
      return false;
    }

    if (isEmptyString(description)) {
      // check if it isn't already false to prevent infinite re-rendering
      if (inputValidity !== false) setInputValidity(false);
      return false;
    }

    for (let i = 0; i < ingredients.length; i += 1) {
      const ingredient = ingredients[i];
      if (
        !isNumber(ingredient.ingredientAmount) ||
        isEmptyString(ingredient.ingredientName) ||
        isEmptyString(ingredient.ingredientPreparation)
      ) {
        if (inputValidity !== false) setInputValidity(false);
        return false;
      }
    }

    if (inputValidity !== true) setInputValidity(true);
    return true;
  };

  const handleSubmit = async (event) => {
    event.preventDefault(); // prevents page refresh

    const recipeModel = {
      owner: user.email,
      instruction: instructions,
      name: recipeTitle,
      calorieCount: 1000, // Calories and full preptime will later be calculated from all the ingredients
      fullPrepTime: 1000,
      imageData,
      recipeIngredient: [],
      userlikedrecipe: []
    };
    console.log(JSON.stringify(recipeModel));
    await authApi(user).post(`/recipe`, JSON.stringify(recipeModel));
  };

  const handleFormChange = (event, element) => {
    const index = ingredients.indexOf(element);
    const tempIngredients = [...ingredients];
    tempIngredients[index][event.target.name] = event.target.value;
    setIngredients(tempIngredients);
  };

  const handleReaderLoaded = (e) => {
    const binaryString = e.target.result;
    setImageData(btoa(binaryString));
  };

  const handlePictureUpload = (event) => {
    const file = event.target.files[0];

    if (file) {
      const reader = new FileReader();
      reader.onload = handleReaderLoaded;
      reader.readAsBinaryString(file);
    }
  };

  const addIngredient = () => {
    // setIngredients(...ingredients, { ingredientName: '', ingredientAmount: '' });
    const tempIngredients = [...ingredients];
    tempIngredients.push({
      ingredientName: '',
      ingredientAmount: '',
      ingredientPreparationMethod: 'None of the above',
      ingredientPreparation: ''
    });
    setIngredients(tempIngredients);
  };

  const removeIngredient = (index) => {
    // setIngredients(...ingredients, { ingredientName: '', ingredientAmount: '' });
    let tempIngredients = [...ingredients];
    tempIngredients = tempIngredients.filter((element) => ingredients.indexOf(element) !== index);
    setIngredients(tempIngredients);
  };

  useEffect(() => {
    isInputValid();
    console.log(ingredients);
  });

  return (
    <>
      <h1>Sharing recipe Demo</h1>
      <Form onSubmit={handleSubmit} className="form">
        <FormGroup>
          <Label> {all.share_recipe_label_name[lang]} </Label>
          <Input
            name="recipeName"
            id="recipeName"
            onChange={(event) => setRecipeTitle(event.target.value)}
            value={recipeTitle}
            invalid={isEmptyString(recipeTitle)}
          />
          <FormFeedback invalid> {all.share_recipe_validation_name[lang]} </FormFeedback>
        </FormGroup>
        <FormGroup>
          <Label sm={2}> {all.share_recipe_label_desc[lang]} </Label>
          <Input
            type="textarea"
            name="text"
            id="description"
            onChange={(event) => setDescription(event.target.value)}
            value={description}
            invalid={isEmptyString(description)}
          />
          <FormFeedback invalid> {all.share_recipe_validation_desc[lang]} </FormFeedback>
        </FormGroup>
        <Label> {all.share_recipe_label_ingredients[lang]} </Label>
        {ingredients.map((element, index) => (
          // change later from index to some sort of ID system
          // eslint-disable-next-line react/no-array-index-key
          <div className="form-row mb-4" key={index}>
            <Row>
              <Col>
                <Label sm={2}> {all.share_recipe_label_ingredient_name[lang]} </Label>
                <FormGroup>
                  <Input
                    type="textarea"
                    name="ingredientName"
                    id="exampleText"
                    onChange={(event) => handleFormChange(event, element)}
                    value={ingredients[index].ingredientName}
                    invalid={isEmptyString(ingredients[index].ingredientName)}
                  />
                  <FormFeedback invalid>
                    {' '}
                    {all.share_recipe_validation_ingredient_name[lang]}{' '}
                  </FormFeedback>
                </FormGroup>
              </Col>
              <Col>
                <Label sm={2}> {all.share_recipe_label_name_ingredient_amount[lang]} </Label>
                <FormGroup>
                  <Input
                    type="textarea"
                    name="ingredientAmount"
                    id="exampleText"
                    onChange={(event) => handleFormChange(event, element)}
                    value={ingredients[index].ingredientAmount}
                    invalid={!isNumber(ingredients[index].ingredientAmount)}
                  />
                  <FormFeedback invalid>Your input has to be a number!</FormFeedback>
                </FormGroup>
              </Col>
              <Col>
                <Label sm={2}> {all.share_recipe_label_name_ingredient_prep[lang]} </Label>
                <FormGroup>
                  <Input
                    type="select"
                    name="ingredientPreparationMethod"
                    onChange={(event) => handleFormChange(event, element)}
                    value={ingredients[index].ingredientPreparationMethod}>
                    <option>Boiled</option>
                    <option>Baked in oven</option>
                    <option>Heated</option>
                    <option>Steamed</option>
                    <option>None of the above</option>
                  </Input>
                </FormGroup>
              </Col>
            </Row>
            <Row>
              <Label sm={2}> {all.share_recipe_label_explanation[lang]} </Label>
              <FormGroup>
                <Input
                  type="textarea"
                  name="ingredientPreparation"
                  onChange={(event) => handleFormChange(event, element)}
                  value={ingredients[index].ingredientPreparation}
                  invalid={isEmptyString(ingredients[index].ingredientPreparation)}
                />
                <FormFeedback invalid>{all.share_recipe_validation_explanation[lang]}</FormFeedback>
              </FormGroup>
              {ingredients.length > 1 && (
                <div>
                  <Button
                    color="danger"
                    size="sm"
                    onClick={() => removeIngredient(ingredients.indexOf(element))}>
                    Remove
                  </Button>{' '}
                </div>
              )}
            </Row>
          </div>
        ))}
        <Button type="button" color="primary" onClick={addIngredient}>
          +
        </Button>{' '}
        <FormGroup>
          <Label sm={2}> {all.share_recipe_label_intr[lang]} </Label>
          <Input
            type="textarea"
            name="text"
            id="exampleText"
            onChange={(event) => setInstructions(event.target.value)}
            value={instructions}
            invalid={isEmptyString(instructions)}
          />
          <FormFeedback invalid> {all.share_recipe_validation_intr[lang]} </FormFeedback>
        </FormGroup>
        <FormGroup>
          <Label> {all.share_recipe_label_picture[lang]} </Label>
          <Input type="file" name="file" onChange={(event) => handlePictureUpload(event)} />
          <FormText color="muted"> {all.share_recipe_validation_picture[lang]} </FormText>
        </FormGroup>
        <Button disabled={!inputValidity}> {all.share_recipe_button_send[lang]} </Button>
      </Form>
    </>
  );
}

import React, { useState, useEffect } from 'react';
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

export default function ShareRecipe() {
  const [recipeTitle, setRecipeTitle] = useState('');
  const [description, setDescription] = useState('');
  const [ingredients, setIngredients] = useState([{ ingredientName: '', ingredientAmount: '' }]);
  const [instructions, setInstructions] = useState('');
  const [inputValidity, setInputValidity] = useState(true);

  const isNumber = (input) => !Number.isNaN(+input); // isNaN returns true if the input is NOT a number, so we have to negate
  const isEmptyString = (str) => str.length === 0;

  const isInputValid = () => {
    console.log(inputValidity);
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
      if (!isNumber(ingredient.ingredientAmount) || isEmptyString(ingredient.ingredientName)) {
        if (inputValidity !== false) setInputValidity(false);
        return false;
      }
    }

    if (inputValidity !== true) setInputValidity(true);
    return true;
  };

  const handleSubmit = (event) => {
    event.preventDefault(); // prevents page refresh
    console.log(recipeTitle);
    console.log(description);
    console.log(instructions);
    console.log(JSON.stringify(ingredients));
  };

  const handleFormChange = (event, element) => {
    const index = ingredients.indexOf(element);
    const tempIngredients = [...ingredients];
    tempIngredients[index][event.target.name] = event.target.value;
    setIngredients(tempIngredients);
  };

  const addIngredient = () => {
    // setIngredients(...ingredients, { ingredientName: '', ingredientAmount: '' });
    const tempIngredients = [...ingredients];
    tempIngredients.push({ ingredientName: '', ingredientAmount: '' });
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
  });

  return (
    <>
      <h1>Sharing recipe Demo</h1>
      <Form onSubmit={handleSubmit} className="form">
        <FormGroup>
          <Label>Recipe name</Label>
          <Input
            name="recipeName"
            id="recipeName"
            onChange={(event) => setRecipeTitle(event.target.value)}
            value={recipeTitle}
            invalid={isEmptyString(recipeTitle)}
          />
          <FormFeedback invalid>Your recipe must have a name!</FormFeedback>
        </FormGroup>
        <FormGroup>
          <Label sm={2}>Short Recipe Description</Label>
          <Input
            type="textarea"
            name="text"
            id="description"
            onChange={(event) => setDescription(event.target.value)}
            value={description}
            invalid={isEmptyString(description)}
          />
          <FormFeedback invalid>Your recipe must have a description!</FormFeedback>
        </FormGroup>
        <Label>Ingredients</Label>
        {ingredients.map((element, index) => (
          // change later from index to some sort of ID system
          // eslint-disable-next-line react/no-array-index-key
          <div className="form-row mb-4" key={index}>
            <Row>
              <Col>
                <Label sm={2}>Ingredient name</Label>
                <FormGroup>
                  <Input
                    type="textarea"
                    name="ingredientName"
                    id="exampleText"
                    onChange={(event) => handleFormChange(event, element)}
                    value={ingredients[index].ingredientName}
                    invalid={isEmptyString(ingredients[index].ingredientName)}
                  />
                  <FormFeedback invalid>Your ingredient must have a name!</FormFeedback>
                </FormGroup>
              </Col>
              <Col>
                <Label sm={2}>Ingredient Amount</Label>
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
          <Label sm={2}>Detailed instructions</Label>
          <Input
            type="textarea"
            name="text"
            id="exampleText"
            onChange={(event) => setInstructions(event.target.value)}
            value={instructions}
            invalid={isEmptyString(instructions)}
          />
          <FormFeedback invalid>Your recipe must have instructions!</FormFeedback>
        </FormGroup>
        <FormGroup>
          <Label for="exampleFile">File</Label>
          <Input
            type="file"
            name="file"
            id="exampleFile"
            onChange={(event) => console.log(event.target.files[0])}
          />
          <FormText color="muted">
            This is some placeholder block-level help text for the above input. Its a bit lighter
            and easily wraps to a new line.
          </FormText>
        </FormGroup>
        <Button disabled={!inputValidity}>Submit</Button>
      </Form>
    </>
  );
}

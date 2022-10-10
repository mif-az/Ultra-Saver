import React, { useState } from 'react';
import { Button, Form, FormGroup, FormFeedback, Input, Label, Row, Col } from 'reactstrap';

export default function ShareRecipe() {
  const [recipeTitle, setRecipeTitle] = useState('');
  const [description, setDescription] = useState('');
  const [ingredients, setIngredients] = useState([{ ingredientName: '', ingredientAmount: '' }]);
  const [instructions, setInstructions] = useState('');

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

  const isNumber = (input) => {
    console.log(typeof input);
    return !Number.isNaN(+input); // isNaN returns true if the input is NOT a number, so we have to negate
  };

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
          />
        </FormGroup>
        <FormGroup>
          <Label sm={2}>Short Recipe Description</Label>
          <Input
            type="textarea"
            name="text"
            id="description"
            onChange={(event) => setDescription(event.target.value)}
            value={description}
          />
        </FormGroup>
        <Label>Ingredients</Label>
        {ingredients.map((element, index) => (
          // change later from index to some sort of ID system
          // eslint-disable-next-line react/no-array-index-key
          <div className="form-row" key={index}>
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
                  />
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
                <Col lg={6} md={6} sm={12} xs={3}>
                  <Button
                    color="danger"
                    size="sm"
                    onClick={() => removeIngredient(ingredients.indexOf(element))}>
                    -
                  </Button>{' '}
                </Col>
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
          />
        </FormGroup>
        <Button>Submit</Button>
      </Form>
    </>
  );
}

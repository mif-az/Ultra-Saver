import React, { useState, useEffect } from 'react';
import { Button, Form, FormGroup, FormFeedback, Input, Label, Row, Col } from 'reactstrap';

export default function AccountSettings() {
  const [electricityPrice, setElectricityPrice] = useState('');
  const [appliances, setAppliances] = useState([{ applianceName: '', applianceWattage: '' }]);
  const [inputValidity, setInputValidity] = useState(true);
  const [preferences, setPreferences] = useState({
    vegetarian: false,
    vegan: false,
    dairy: false,
    eggs: false,
    fish: false,
    shellfish: false,
    nuts: false,
    wheat: false,
    soybean: false
  });

  const isNumber = (input) => !Number.isNaN(+input); // isNaN returns true if the input is NOT a number, so we have to negate
  const isEmptyString = (str) => str.length === 0;

  const isInputValid = () => {
    console.log(inputValidity);
    if (!isNumber(electricityPrice)) {
      // check if it isn't already false to prevent infinite re-rendering
      if (inputValidity !== false) setInputValidity(false);
      return false;
    }

    for (let i = 0; i < appliances.length; i += 1) {
      const appliance = appliances[i];
      if (!isNumber(appliance.applianceWattage) || isEmptyString(appliance.applianceName)) {
        if (inputValidity !== false) setInputValidity(false);
        return false;
      }
    }

    if (inputValidity !== true) setInputValidity(true);
    return true;
  };

  const handleSubmit = (event) => {
    event.preventDefault(); // prevents page refresh
    console.log(electricityPrice);
    console.log(JSON.stringify(appliances));
    console.log(JSON.stringify(preferences));
  };

  const handleFormChange = (event, element) => {
    const index = appliances.indexOf(element);
    const tempAppliances = [...appliances];
    tempAppliances[index][event.target.name] = event.target.value;
    setAppliances(tempAppliances);
  };

  const addAppliance = () => {
    const tempAppliances = [...appliances];
    tempAppliances.push({ applianceName: '', applianceWattage: '' });
    setAppliances(tempAppliances);
  };

  const removeAppliance = (index) => {
    let tempAppliances = [...appliances];
    tempAppliances = tempAppliances.filter((element) => appliances.indexOf(element) !== index);
    setAppliances(tempAppliances);
  };

  useEffect(() => {
    isInputValid();
  });

  return (
    <>
      <h1>Account settings</h1>
      <Form onSubmit={handleSubmit} className="form">
        <FormGroup>
          <Label>Electricity price (eur/kwh)</Label>
          <Input
            name="electricityPrice"
            id="electricityPrice"
            onChange={(event) => setElectricityPrice(event.target.value)}
            value={electricityPrice}
            invalid={!isNumber(electricityPrice)}
          />
          <FormFeedback invalid>Your input has to be a number!</FormFeedback>
        </FormGroup>
        <Label>Owned Appliances</Label>
        {appliances.map((element, index) => (
          // change later from index to some sort of ID system
          // eslint-disable-next-line react/no-array-index-key
          <div className="form-row" key={index}>
            <Row>
              <Col>
                <Label sm={2}>Appliance name</Label>
                <FormGroup>
                  <Input
                    type="textarea"
                    name="applianceName"
                    id="applianceName"
                    onChange={(event) => handleFormChange(event, element)}
                    value={appliances[index].applianceName}
                    invalid={isEmptyString(appliances[index].applianceName)}
                  />
                  <FormFeedback invalid>The appliance has to have a name!</FormFeedback>
                </FormGroup>
              </Col>
              <Col>
                <Label sm={2}>Appliance Wattage</Label>
                <FormGroup>
                  <Input
                    type="textarea"
                    name="applianceWattage"
                    id="applianceWattage"
                    onChange={(event) => handleFormChange(event, element)}
                    value={appliances[index].applianceWattage}
                    invalid={!isNumber(appliances[index].applianceWattage)}
                  />
                  <FormFeedback invalid>Your input has to be a number!</FormFeedback>
                </FormGroup>
              </Col>
              {appliances.length > 1 && (
                <Col lg={6} md={6} sm={12} xs={3}>
                  <Button
                    color="danger"
                    size="sm"
                    onClick={() => removeAppliance(appliances.indexOf(element))}>
                    -
                  </Button>
                </Col>
              )}
            </Row>
          </div>
        ))}
        <Button type="button" color="primary" onClick={addAppliance}>
          +
        </Button>
        <div className="row">
          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) =>
                  setPreferences({ ...preferences, vegetarian: event.target.checked })
                }
                value={preferences.vegetarian}
              />
              Vegetarian
            </Label>
          </FormGroup>
          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) =>
                  setPreferences({ ...preferences, vegan: event.target.checked })
                }
                value={preferences.vegan}
              />
              Vegan
            </Label>
          </FormGroup>
          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) =>
                  setPreferences({ ...preferences, dairy: event.target.checked })
                }
                value={preferences.dairy}
              />
              Dairy Allergy
            </Label>
          </FormGroup>

          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) => setPreferences({ ...preferences, eggs: event.target.checked })}
                value={preferences.eggs}
              />
              Eggs Allergy
            </Label>
          </FormGroup>
          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) => setPreferences({ ...preferences, fish: event.target.checked })}
                value={preferences.fish}
              />
              Fish Allergy
            </Label>
          </FormGroup>
          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) =>
                  setPreferences({ ...preferences, shellfish: event.target.checked })
                }
                value={preferences.shellfish}
              />
              Shellfish Allergy
            </Label>
          </FormGroup>
          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) => setPreferences({ ...preferences, nuts: event.target.checked })}
                value={preferences.nuts}
              />
              Nuts allergy
            </Label>
          </FormGroup>
          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) =>
                  setPreferences({ ...preferences, wheat: event.target.checked })
                }
                value={preferences.wheat}
              />
              Wheat allergy
            </Label>
          </FormGroup>
          <FormGroup check inline>
            <Label check>
              <Input
                type="checkbox"
                onChange={(event) =>
                  setPreferences({ ...preferences, soybean: event.target.checked })
                }
                value={preferences.soybean}
              />
              Soybean allergy
            </Label>
          </FormGroup>
        </div>
        <Button disabled={!inputValidity}>Submit</Button>
      </Form>
    </>
  );
}

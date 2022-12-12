import React, { useState, useEffect, useContext } from 'react';
import { Button, Form, FormGroup, FormFeedback, Input, Label, Row, Col } from 'reactstrap';
import { LanguageContext } from '../contexts/LanguageProvider';
import { authApi, UserContext } from '../contexts/UserProvider';
import URL from '../appUrl';
import all from './Texts/all';

export default function AccountSettings() {
  const [electricityPrice, setElectricityPrice] = useState('');
  const [lang] = useContext(LanguageContext);
  const [appliances, setAppliances] = useState([{ applianceName: '', applianceWattage: '' }]);
  const [inputValidity, setInputValidity] = useState(true);
  const [user] = useContext(UserContext);
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
  // const capitalizeFirstLetter = (str) => str.substring(0, 1).toUpperCase() + str.substring(1);

  const isInputValid = () => {
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

  const handleSubmit = async (event) => {
    event.preventDefault(); // prevents page refresh
    await authApi(user).post(`${URL}/userallergens`, preferences);
  };

  const handleFormChange = (event, element) => {
    const index = appliances.indexOf(element);
    const tempAppliances = [...appliances];
    tempAppliances[index][event.target.name] = event.target.value;
    setAppliances(tempAppliances);
  };

  const handlePreferencesChange = (event, preference) => {
    preferences[preference] = event.target.checked;
    setPreferences(preferences);
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

  const generatePreferenceCheckbox = (preference) => (
    <FormGroup check inline>
      <Label check>
        <Input
          type="checkbox"
          onChange={(event) => handlePreferencesChange(event, preference)}
          value={preferences[preference]}
        />
        {all[`account_settings_checkbox_${preference}`][lang]}
      </Label>
    </FormGroup>
  );

  useEffect(() => {
    isInputValid();
  });

  return (
    <>
      <h1> {all.all_navbar_acc_settings[lang]} </h1>
      <Form onSubmit={handleSubmit} className="form">
        <FormGroup>
          <Label>{all.account_settings_label_electricity_price[lang]} (eur/kwh)</Label>
          <Input
            name="electricityPrice"
            id="electricityPrice"
            onChange={(event) => setElectricityPrice(event.target.value)}
            value={electricityPrice}
            invalid={!isNumber(electricityPrice)}
          />
          <FormFeedback invalid>Your input has to be a number!</FormFeedback>
        </FormGroup>
        <Label>{all.account_settings_label_apliances[lang]}</Label>
        {appliances.map((element, index) => (
          // change later from index to some sort of ID system
          // eslint-disable-next-line react/no-array-index-key
          <div className="form-row" key={index}>
            <Row>
              <Col>
                <Label sm={2}> {all.account_settings_label_appliance_name[lang]} </Label>
                <FormGroup>
                  <Input
                    type="textarea"
                    name="applianceName"
                    id="applianceName"
                    onChange={(event) => handleFormChange(event, element)}
                    value={appliances[index].applianceName}
                    invalid={isEmptyString(appliances[index].applianceName)}
                  />
                  <FormFeedback invalid>
                    {all.account_settings_validation_appliance_name[lang]}
                  </FormFeedback>
                </FormGroup>
              </Col>
              <Col>
                <Label sm={2}>{all.account_settings_label_appliance_wattage[lang]}</Label>
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
          {Object.keys(preferences).map((preference) => generatePreferenceCheckbox(preference))}
        </div>
        <Button disabled={!inputValidity}>{all.all_button_save[lang]}</Button>
      </Form>
    </>
  );
}

import React, { useState, useEffect } from 'react';

export default function EnergyCalculation() {
  const [cost, setCost] = useState([]);

  const fetchData = async () => {
    try {
      const response = await fetch(`/energycost`);
      const number = await response.json();
      console.log(number);
      setCost(number);
    } catch (error) {
      console.log('error', error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <>
      <h1>Energy Cost Calculation Demo</h1>
      <p>(Hours / Wattage) * eur/kWh = {cost}</p>
    </>
  );
}

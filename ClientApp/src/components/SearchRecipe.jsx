import React, { useContext, useState, useEffect } from 'react';
import { Input, Label } from 'reactstrap';
import { authApi, UserContext } from '../contexts/UserProvider';

export default function SearchRecipe() {
  const [recipes, setRecipes] = useState([]);
  // const [priceFilter, setPriceFilter] = useState();
  const [user] = useContext(UserContext);

  const sortOptions = [
    {
      label: 'Alphabetically',
      value: 'name'
    },
    {
      label: 'Lowest time',
      value: 'minutes'
    },
    {
      label: 'Lowest price',
      value: 'wattage'
    }
  ];

  async function fetchData(query) {
    const response = await authApi(user).get(`recipe?filter=${query}`);
    const data = await response.json();
    console.log('data :>> ', data);
    return data;
  }

  async function handleSearchChange(query) {
    const data = await fetchData(query);
    setRecipes(data);
  }

  // sorts recipes by given property in descending order
  const sortData = (data, sortOption) => {
    console.log('sorting');
    const sortedData = [...data];
    sortedData.sort((a, b) => (a[sortOption] > b[sortOption] ? 1 : -1));
    return sortedData;
  };

  const filterData = (data, filterOption, filterValue) => {
    console.log(`filtering${filterValue}`);
    console.log(data);
    const filteredData = data.filter((recipe) => recipe[filterOption] < filterValue);
    return filteredData;
  };

  // async function handleFilterChange(query) {
  //   const data = await fetchData(query);
  //   filter;
  //   setRecipes(data);
  // }

  // Call fetch data on first render to not have an empty list on start
  useEffect(() => {
    const initialFetchData = async () => {
      let data = await fetchData('');
      data = sortData(data, 'name');
      setRecipes(data);
    };
    initialFetchData();
  }, []); // second argument makes useEffect call fetchData() only on first render

  return (
    <div className="container">
      <div className="row">
        <div className="col-6">
          <Label>Search bar</Label>
          <Input
            type="text"
            onChange={(e) => {
              handleSearchChange(e.target.value);
            }}
          />
        </div>
        <div className="col-3">
          <Label>Sort by</Label>
          <Input
            type="select"
            name="select"
            onChange={(e) => {
              setRecipes(sortData(recipes, e.target.value));
            }}>
            {sortOptions.map((option) => (
              <option value={option.value}>{option.label}</option>
            ))}
          </Input>
        </div>
        <div className="row">
          <div className="col">
            <Label>Price filter</Label>
            <Input
              type="text"
              onChange={(e) => {
                setRecipes(filterData(recipes, 'wattage', e.target.value));
              }}
            />
          </div>
        </div>
      </div>
      <div className="row form-check border-1 border-danger">
        {recipes.map((el) => (
          <div className="row border-2 mt-1 bg-dark text-white rounded-1 p-2" key={el.id}>
            <div className="col-5">
              <div className="fw-bold">{el.name}</div>
              <p>Takes {el.minutes} minutes to make</p>
              <p>Short description here (which we dont have)</p>
            </div>
            <div className="col-6">
              <p>Estimated price:</p>
              <p>{el.wattage}$</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

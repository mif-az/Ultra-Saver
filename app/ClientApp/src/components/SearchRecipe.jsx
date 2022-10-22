import React, { useContext, useState, useEffect } from 'react';
import { Input, Label } from 'reactstrap';
import { authApi, UserContext } from '../contexts/UserProvider';

export default function SearchRecipe() {
  const [recipes, setRecipes] = useState([]);
  const [query, setQuery] = useState();
  const [sortOption, setSortOption] = useState();
  const [filterOptions, setFilterOptions] = useState({
    wattage: '',
    minutes: ''
  });
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

  async function fetchData(q) {
    const response = await authApi(user).get(`recipe?filter=${q}`);
    const data = await response.json();
    return data;
  }

  // sorts recipes by given property in descending order
  const sortData = (data, option) => {
    const sortedData = [...data];
    sortedData.sort((a, b) => (a[option] > b[option] ? 1 : -1));
    return sortedData;
  };

  const filterData = (data, filterOption, filterValue) => {
    if (filterValue === '') return data;
    const filteredData = data.filter((recipe) => recipe[filterOption] < filterValue);
    return filteredData;
  };

  const updateAndSetRecipes = (d, sort, filters) => {
    let data = d;
    data = filterData(data, 'wattage', filters.wattage); // should implement more automated way of doing this later
    data = filterData(data, 'minutes', filters.minutes);
    data = sortData(data, sort);

    setRecipes(data);
  };

  async function handleSearchChange(q) {
    const data = await fetchData(q);
    setQuery(q);
    updateAndSetRecipes(data, sortOption, filterOptions);
  }

  async function handleSortChange(q) {
    await setSortOption(q);
    updateAndSetRecipes(recipes, q, filterOptions);
  }

  async function handleFilterChange(option, value) {
    const tempFilters = filterOptions;
    tempFilters[option] = value;
    setFilterOptions(tempFilters);
    const data = await fetchData(query);
    updateAndSetRecipes(data, sortOption, tempFilters);
  }
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
              handleSortChange(e.target.value);
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
                handleFilterChange('wattage', e.target.value);
              }}
            />
          </div>
          <div className="col">
            <Label>Time filter</Label>
            <Input
              type="text"
              onChange={(e) => {
                handleFilterChange('minutes', e.target.value);
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
              <p>~{el.minutes} minutes to make</p>
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
import React, { useContext, useState, useEffect } from 'react';
import { Button, Input, Label } from 'reactstrap';
import { Link } from 'react-router-dom';
import { authApi, UserContext } from '../contexts/UserProvider';
import URL from '../appUrl';
import all from './Texts/all';
import { LanguageContext } from '../contexts/LanguageProvider';

// eslint-disable-next-line react/prop-types
export default function SearchRecipe({ request, likedRecipes }) {
  const [lang] = useContext(LanguageContext);
  const [recipes, setRecipes] = useState([]);
  const [query, setQuery] = useState('');
  const [sortOption, setSortOption] = useState();
  const [energyPrice, setEnergyPrice] = useState(1);
  const [filterOptions, setFilterOptions] = useState({
    wattage: '',
    fullPrepTime: ''
  });
  const [user] = useContext(UserContext);

  const sortOptions = [
    {
      label: all.search_recipe_dropdown_sortby_alpha[lang],
      value: 'name'
    },
    {
      label: all.search_recipe_dropdown_sortby_time[lang],
      value: 'fullPrepTime'
    },
    {
      label: all.search_recipe_dropdown_sortby_price[lang],
      value: 'wattage'
    }
  ];

  async function fetchData(q) {
    const response = await authApi(user).get(`${URL}/${request}?filter=${q}`);
    const data = await response.json();
    console.log(data);
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
    data = filterData(data, 'fullPrepTime', filters.fullPrepTime);
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

  const handleLikeRecipe = async (recipe) => {
    const likedRecipeModel = {
      userEmail: user.email,
      recipeId: recipe.id
    };

    if (likedRecipes) {
      await authApi(user).delete(`${URL}/userLikedRecipe`, JSON.stringify(likedRecipeModel));
      await handleSearchChange(query);
    } else await authApi(user).post(`${URL}/userLikedRecipe`, JSON.stringify(likedRecipeModel));
  };

  const getEnergyPrice = async () => {
    const u = await (await authApi(user).get(`${URL}/userprice`)).json();
    setEnergyPrice(u.electricityPrice);
  };

  // Call fetch data on first render to not have an empty list on start
  useEffect(() => {
    const initialFetchData = async () => {
      let data = await fetchData('');
      data = sortData(data, 'name');
      setRecipes(data);
    };
    initialFetchData();
  }, [request, likedRecipes]);

  useEffect(() => {
    getEnergyPrice();
  }, []);

  const likeButton = (el) => {
    if (likedRecipes) {
      return (
        <Button color="danger" onClick={() => handleLikeRecipe(el)}>
          Unlike recipe
        </Button>
      );
    }
    return (
      <Button color="primary" onClick={() => handleLikeRecipe(el)}>
        Like recipe
      </Button>
    );
  };

  return (
    <div className="container">
      <div className="row">
        <div className="col-6">
          <Label> {all.search_recipe_label_search[lang]} </Label>
          <Input
            type="text"
            onChange={(e) => {
              handleSearchChange(e.target.value);
            }}
          />
        </div>
        <div className="col-3">
          <Label> {all.search_recipe_label_sortby[lang]} </Label>
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
            <Label> {all.search_recipe_label_pricefilter[lang]} </Label>
            <Input
              type="text"
              onChange={(e) => {
                handleFilterChange('wattage', e.target.value);
              }}
            />
          </div>
          <div className="col">
            <Label> {all.search_recipe_label_timefilter[lang]} </Label>
            <Input
              type="text"
              onChange={(e) => {
                handleFilterChange('fullPrepTime', e.target.value);
              }}
            />
          </div>
        </div>
      </div>
      <div className="row form-check border-1 border-danger">
        {recipes.map((el) => (
          <div>
            <div className="row border-2 mt-1 bg-dark text-white rounded-1 p-2" key={el.id}>
              <div className="col-6">
                <Link to={`/r/${el.id}`}>
                  <div className="fw-bold">{el.name}</div>
                </Link>
                <p>~{el.fullPrepTime} minutes to make</p>
                <p>Short description here (which we dont have)</p>
              </div>
              <div className="col">
                <p>Estimated price:</p>
                <p>{el.totalEnergy * energyPrice}$</p>
              </div>
              <div className="col">
                <img
                  style={{ height: 150 }}
                  src={`data:image/jpeg;base64,${el.imageData}`}
                  alt=""
                />
              </div>
            </div>
            <div className="row">{likeButton(el)}</div>
          </div>
        ))}
      </div>
    </div>
  );
}

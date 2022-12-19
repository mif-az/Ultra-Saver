import React, { useContext, useState, useEffect } from 'react';
import { Button, Input, Label } from 'reactstrap';
import { authApi, UserContext } from '../contexts/UserProvider';
import URL from '../appUrl';
import all from './Texts/all';
import { LanguageContext } from '../contexts/LanguageProvider';

// eslint-disable-next-line react/prop-types
export default function SearchRecipe({ request, likedRecipes }) {
  const [lang] = useContext(LanguageContext);
  const [recipes, setRecipes] = useState([]);
  const [query, setQuery] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [sortOption, setSortOption] = useState();
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

  async function fetchData(q, page = currentPage) {
    const response = await authApi(user).get(`${URL}/${request}?page=${page}&filter=${q}`);
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

  const handlePagination = async (increment) => {
    const data = await fetchData(query, currentPage + increment);
    setCurrentPage(currentPage + increment);
    updateAndSetRecipes(data, sortOption, filterOptions);
  };

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

  // Call fetch data on first render to not have an empty list on start
  useEffect(() => {
    const initialFetchData = async () => {
      let data = await fetchData('');
      data = sortData(data, 'name');
      setRecipes(data);
    };
    initialFetchData();
  }, [request, likedRecipes]); // second argument makes useEffect call fetchData() only on first render

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
                <div className="fw-bold">{el.name}</div>
                <p>~{el.fullPrepTime} minutes to make</p>
                <p>Short description here (which we dont have)</p>
              </div>
              <div className="col">
                <p>Estimated price:</p>
                <p>{el.wattage}$</p>
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
        <div className="d-flex justify-content-center">
          <div className="btn-group">
            <Button className="btn" onClick={() => handlePagination(-1)}>
              Previous
            </Button>
            <Button className="btn" onClick={() => handlePagination(+1)}>
              Next
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
}

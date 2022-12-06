/* eslint-disable react/react-in-jsx-scope */
import Home from './components/HomeComponents/Home';
import Login from './components/Login';
import UserProps from './components/UserProps';
import ShareRecipe from './components/ShareRecipe';
import SearchRecipe from './components/SearchRecipe';
import Recipe from './components/Recipe';
import EnergyCalculation from './components/EnergyCalculation';
import AccountSettings from './components/AccountSettings';
import MessageUi from './components/MessageUi';
import CommunityChat from './components/CommunityChat';

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/login',
    element: <Login />
  },
  {
    path: '/energycalculation',
    element: <EnergyCalculation />
  },
  {
    path: '/user/properties',
    element: <UserProps />
  },
  {
    path: '/sharerecipe',
    element: <ShareRecipe />
  },
  {
    path: '/searchrecipe',
    element: <SearchRecipe request="recipe" likedRecipes={false} />
  },
  {
    path: '/r/:recipeId',
    element: <Recipe />
  },
  {
    path: '/likedrecipes',
    element: <SearchRecipe request="userlikedrecipe" likedRecipes />
  },
  {
    path: '/accountsettings',
    element: <AccountSettings />
  },
  {
    path: '/messageui',
    element: <MessageUi />
  },
  {
    path: '/communitychat',
    element: <CommunityChat />
  }
];

export default AppRoutes;

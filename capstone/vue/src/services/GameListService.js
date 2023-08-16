import axios from 'axios';

export default {
   listGameListsByUserId(id){
      return axios.get(`gamelist/user/${id}`);
   },
   getGameList(id) {
      return axios.get(`gamelist/${id}`);
   },
   addGameList(gameList) {
      return axios.post(`gamelist`, gameList);
   },
   updateGameList(gameList) {
      return axios.put(`gamelist`, gameList);
   },
};
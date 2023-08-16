import axios from 'axios';

export default {
   getGameList(id) {
      return axios.get(`gamelist/${id}`);
       
   },
   ListGameListsByUserId(id){
      return axios.get(`gameList/user/${id}`);
   }
};
import axios from 'axios';

export default {
   getGameList(userId, gameListType ) {
      return axios.get(`gamelist/${userId}/${gameListType}`);
   },
   addGameToList(entry) {
      return axios.post(`gamelist`, entry);
   },
   deleteGameFromList(entry) {
      return axios.delete(`gamelist`, entry);
   },
};
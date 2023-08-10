import axios from 'axios';

export default {
  getRatingByGameId(id){
    return axios.get(`/rating/game/${id}`);
  },
  getRating(gameId,userId){
    return axios.get(`/rating/${gameId}/${userId}`)
  }
}
